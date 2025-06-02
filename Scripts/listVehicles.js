const { createClient } = require("redis");

//Script to list all vehicles. 

(async () => {
  const redis = createClient({
    url: process.env.REDIS_URL ?? "redis://localhost:6379"
  });
  await redis.connect();

  // Find all keys 
  const keys = await redis.keys("vehicle:*"); 
  if (!keys.length) {
    console.log("No seeded vehicles found.");
  } else {
    console.log("Found vehicle keys:");
    for (const key of keys) {
      const id = key.split(":")[1];
      console.log("  Vehicle ID:", id);
    }
  }

  await redis.disconnect();
})();
