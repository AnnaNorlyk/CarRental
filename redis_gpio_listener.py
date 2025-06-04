# === [IMPORT LIBRARIES] ===
import redis            # For Redis messaging (Pub/Sub)
import RPi.GPIO as GPIO # For Raspberry Pi GPIO control
import time             # For sleep/delay
import threading        # For running motion detection in the background

# === [GPIO PIN ASSIGNMENTS] ===
RELAY_PIN = 18          # Controls relay to open/close cabinet
BUTTON_PIN = 17         # Input from help button
LED_HELP_PIN = 27       # LED lights up when help is needed
PIR_PIN = 22            # PIR motion sensor input
LED_MOTION_PIN = 23     # LED lights up on motion
PWM_LED_PIN = 24        # PWM-controlled LED (fade effect on motion)

# === [GPIO SETUP] ===
GPIO.setmode(GPIO.BCM)             # Use Broadcom pin-numbering
GPIO.setwarnings(False)            # Disable warning messages

# Setup output pins (LEDs and relay)
GPIO.setup(RELAY_PIN, GPIO.OUT)
GPIO.output(RELAY_PIN, GPIO.LOW)   # Start with cabinet closed

GPIO.setup(LED_HELP_PIN, GPIO.OUT)
GPIO.output(LED_HELP_PIN, GPIO.LOW)

GPIO.setup(LED_MOTION_PIN, GPIO.OUT)
GPIO.output(LED_MOTION_PIN, GPIO.LOW)

# Setup input pins (button and motion sensor)
GPIO.setup(BUTTON_PIN, GPIO.IN, pull_up_down=GPIO.PUD_UP)  # Pull-up resistor
GPIO.setup(PIR_PIN, GPIO.IN)

# Setup PWM pin
GPIO.setup(PWM_LED_PIN, GPIO.OUT)
pwm = GPIO.PWM(PWM_LED_PIN, 1000)  # 1000 Hz = 1kHz frequency
pwm.start(0)  # Start PWM with 0% duty cycle (LED off)

print("[INFO] GPIO setup complete.")

# === [REDIS PUB/SUB SECTION] ===
# Setup Redis client and subscribe to cabinet control channel
r = redis.Redis(host='10.176.69.106', port=6379, decode_responses=True)
pubsub = r.pubsub()
pubsub.subscribe('cabinet-channel')
print("[INFO] Connected to Redis. Listening on 'cabinet-channel'...")

# === [BUTTON INTERRUPT SECTION] ===
# Define a callback function for help button
def help_button_pressed(channel):
    print("[INFO] Help button pressed! Lighting up help LED.")
    GPIO.output(LED_HELP_PIN, GPIO.HIGH)  # Turn on help LED
    time.sleep(5)                         # Keep it on for 5 seconds
    GPIO.output(LED_HELP_PIN, GPIO.LOW)   # Turn off

# Register the callback for falling edge (button press)
GPIO.add_event_detect(BUTTON_PIN, GPIO.FALLING, callback=help_button_pressed, bouncetime=300)

# === [MOTION SENSOR + PWM SECTION] ===
# Monitor motion detection in a background thread
def motion_sensor_monitor():
    print("[INFO] Motion sensor monitoring started.")
    while True:
        if GPIO.input(PIR_PIN):  # Motion detected
            print("[INFO] Motion detected!")
            GPIO.output(LED_MOTION_PIN, GPIO.HIGH)  # Turn on motion LED

            # Fade in PWM LED
            for duty in range(0, 101, 10):  # 0% to 100%
                pwm.ChangeDutyCycle(duty)
                time.sleep(0.05)

            time.sleep(5)  # Keep LED on

            # Fade out PWM LED
            for duty in range(100, -1, -10):  # 100% to 0%
                pwm.ChangeDutyCycle(duty)
                time.sleep(0.05)

            GPIO.output(LED_MOTION_PIN, GPIO.LOW)  # Turn off motion LED

        time.sleep(0.1)  # Check for motion every 100ms

# Start motion detection thread
motion_thread = threading.Thread(target=motion_sensor_monitor, daemon=True)
motion_thread.start()

# === [REDIS MESSAGE LISTENER LOOP] ===
try:
    for message in pubsub.listen():  # Block until new message
        if message['type'] != 'message':
            continue  # Ignore subscription confirmations, etc.

        command = message['data']
        print(f"[INFO] Received message: {command}")

        if command == 'open-cabinet':
            print("[INFO] Opening cabinet (relay ON).")
            GPIO.output(RELAY_PIN, GPIO.HIGH)
            print("[INFO] Cabinet will close automatically in 10 seconds...")
            time.sleep(10)
            GPIO.output(RELAY_PIN, GPIO.LOW)
            print("[INFO] Cabinet closed automatically after 10 seconds.")

        elif command == 'close-cabinet':
            print("[INFO] Closing cabinet (relay OFF).")
            GPIO.output(RELAY_PIN, GPIO.LOW)

        else:
            print(f"[WARN] Unknown command received: {command}")

# === [CLEANUP ON EXIT] ===
except KeyboardInterrupt:
    print("\n[INFO] Interrupted by user. Cleaning up...")

finally:
    GPIO.output(RELAY_PIN, GPIO.LOW)
    GPIO.output(LED_HELP_PIN, GPIO.LOW)
    GPIO.output(LED_MOTION_PIN, GPIO.LOW)
    pwm.stop()         # Stop PWM signal
    GPIO.cleanup()     # Reset all GPIO pins
    print("[INFO] GPIO cleanup complete. Exiting.")
