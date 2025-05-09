import { createClient } from "redis";

export const redisPub = createClient({
  url: process.env.REDIS_URL ?? "redis://127.0.0.1:6379"
});

redisPub.on("error", err => console.error("Redis error:", err));

(async () => {
  await redisPub.connect();
})();
