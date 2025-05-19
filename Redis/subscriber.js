const { createClient } = require('redis');
const os = require('os');
let Gpio, relay;

// GPIO setup – virker på Raspberry Pi (Linux)
if (os.platform() === 'linux') {
    Gpio = require('onoff').Gpio;
    relay = new Gpio(17, 'out');
    console.log('GPIO initialized (Linux/Raspberry Pi)');
} else {
    console.log('Running on non-Linux system (GPIO mocked)');
}

// Forbind til Redis-server (Windows IP)
const subscriber = createClient({
    socket: {
        host: '10.176.69.110', // ← din opdaterede IP!
        port: 6379
    }
});

subscriber.on('error', (err) => {
    console.error('Redis error:', err);
});

async function start() {
    console.log(`Forbinder til Redis på ${subscriber.options.socket.host}:${subscriber.options.socket.port}...`);
    await subscriber.connect();
    console.log('Connected to Redis');

    // Abonner på channel og håndter beskeder
    await subscriber.subscribe('cabinet-channel', (message) => {
        console.log(`Besked modtaget fra Redis: ${message}`);

        if (message === 'open-cabinet') {
            if (relay) {
                console.log('BESKED MATCHER: Åbner skuffe...');
                relay.writeSync(1);
            } else {
                console.log('Mock: Åbner skuffe (ingen GPIO)');
            }
        } else if (message === 'close-cabinet') {
            if (relay) {
                console.log('BESKED MATCHER: Lukker skuffe...');
                relay.writeSync(0);
            } else {
                console.log('Mock: Lukker skuffe (ingen GPIO)');
            }
        } else {
            console.log('Ukendt beskedtype!');
        }
    });

    console.log('Subscribed to cabinet-channel');
    console.log('Venter på beskeder...');
}

start();

// Ctrl+C cleanup
process.on('SIGINT', async () => {
    console.log('Stopper og rydder op...');
    if (relay) {
        relay.writeSync(0);
        relay.unexport();
    }
    await subscriber.quit();
    process.exit(0);
});

