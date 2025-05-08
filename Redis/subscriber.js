const { createClient } = require('redis');
const os = require('os'); // Import Node's 'os' module to detect which system we're running on (Linux or not)
let Gpio, relay;

if (os.platform() === 'linux') {
    Gpio = require('onoff').Gpio;
    relay = new Gpio(17, 'out');
    console.log(' GPIO initialized (Linux/Raspberry Pi)');
} else {
    console.log('Running on non-Linux system (GPIO mocked)');
}

// Set up Redis client
const subscriber = createClient();

subscriber.on('error', (err) => console.error('Redis error:', err));

// Connect and subscribe
async function start() {
    await subscriber.connect();
    console.log(' Connected to Redis');

    await subscriber.subscribe('cabinet-channel', (message) => {
        console.log(`Received message: ${message}`);

        if (message === 'open-cabinet') {
            if (os.platform() === 'linux') {
                console.log('Activating relay (opening cabinet)...');
                relay.writeSync(1);
            } else {
                console.log(' MOCK: Would open cabinet (no GPIO)');
            }
        } else if (message === 'close-cabinet') {
            if (os.platform() === 'linux') {
                console.log('Deactivating relay (closing cabinet)...');
                relay.writeSync(0);
            } else {
                console.log('MOCK: Would close cabinet (no GPIO)');
            }
        } else {
            console.log(`Unknown command: ${message}`);
        }
    });
}

start();

// Handle CTRL+C
process.on('SIGINT', async () => {
    console.log('Cleaning up...');
    if (os.platform() === 'linux' && relay) {
        relay.writeSync(0);
        relay.unexport();
    }
    await subscriber.quit();
    process.exit(0);
});

