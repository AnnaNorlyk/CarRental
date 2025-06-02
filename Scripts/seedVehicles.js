import { createClient } from "redis";
import { v4 as uuid }   from "uuid";

(async () => {
  const redis = createClient({ url: process.env.REDIS_URL });
  await redis.connect();

  const vehicles = [
    {
      id: uuid(),
      model: "Corolla",
      fabricant: "Toyota",
      seats: 5,
      transmissionType: "automatic",
      isAvailable: true,
    },
    {
      id: uuid(),
      model: "Civic",
      fabricant: "Honda",
      seats: 5,
      transmissionType: "manual",
      isAvailable: true,
    },
  ];

  for (const v of vehicles) {
    const key = `vehicle:${v.id}`;
    await redis.hSet(key, {
      id:               v.id,
      model:            v.model,
      fabricant:        v.fabricant,
      seats:            v.seats.toString(),
      transmissionType: v.transmissionType,
      isAvailable:      v.isAvailable.toString(),
    });
  }

  console.log("Seeded vehicles:", vehicles.map(v => v.id));
  await redis.disconnect();
})();
