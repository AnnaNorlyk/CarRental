import { createClient } from 'redis';
import dotenv from 'dotenv';

dotenv.config();

// Create a Redis client using socket configuration
const redis = createClient({
  socket: {
    host: process.env.REDIS_HOST || 'localhost',
    port: Number(process.env.REDIS_PORT) || 6379
  }
});

// Handle Redis connection errors
redis.on('error', (err) => console.error('[Redis error]', err));


// Track whether the Redis client is already connected
let connected = false;

// Export an async function to publish a message to a Redis channel
export async function publishMessage(channel: string, message: string) {
  if (!connected) {
    await redis.connect();
    connected = true; 
  }

   // Publish the message to the given channel
  await redis.publish(channel, message);
  console.log(`[Redis] Published: ${message} on ${channel}`);
}


