import redis
import RPi.GPIO as GPIO
import time

# GPIO-opsætning
GPIO.setmode(GPIO.BCM)
GPIO.setwarnings(False)
RELAY_PIN = 18
GPIO.setup(RELAY_PIN, GPIO.OUT)
GPIO.output(RELAY_PIN, GPIO.LOW)  # starts with closed cabinet

print("[INFO] GPIO pin 18 klar.")

# connect to Redis
r = redis.Redis(host='10.176.69.106', port=6379, decode_responses=True)
pubsub = r.pubsub()
pubsub.subscribe('cabinet-channel')
print("[INFO] Tilsluttet Redis. Venter på beskeder...")

try:
    for message in pubsub.listen():
        if message['type'] != 'message':
            continue

        command = message['data']
        print(f"[INFO] Modtog besked: {command}")

        if command == 'open-cabinet':
            print("[INFO] Åbner skuffe...")
            GPIO.output(RELAY_PIN, GPIO.HIGH)
        elif command == 'close-cabinet':
            print("[INFO] Lukker skuffe...")
            GPIO.output(RELAY_PIN, GPIO.LOW)
        else:
            print(f"[WARN] Ukendt besked: {command}")

except KeyboardInterrupt:
    print("\n[INFO] Afslutter og rydder op...")
finally:
    GPIO.output(RELAY_PIN, GPIO.LOW)
    GPIO.cleanup()
