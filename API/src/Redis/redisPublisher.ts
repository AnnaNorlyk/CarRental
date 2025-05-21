import { createClient } from 'redis';
import dotenv from 'dotenv';

dotenv.config();

const redis = createClient({
  socket: {
    host: process.env.REDIS_HOST || 'localhost',
    port: Number(process.env.REDIS_PORT) || 6379
  }
});

redis.on('error', (err) => console.error('[Redis error]', err));

let connected = false;

export async function publishMessage(channel: string, message: string) {
  if (!connected) {
    await redis.connect();
    connected = true;
  }

  await redis.publish(channel, message);
  console.log(`[Redis] Published: ${message} on ${channel}`);
}
