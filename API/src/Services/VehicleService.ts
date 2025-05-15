import { redisPub as redis } from "../Redis/client";
import { Vehicle }           from "../Models/Vehicle";

export async function getVehicleById(id: string): Promise<Vehicle | null> {
  const raw = await redis.get(`vehicle:${id}`);
  return raw ? JSON.parse(raw) as Vehicle : null;
}

export async function listAllVehicles(): Promise<Vehicle[]> {
  const keys = await redis.keys("vehicle:*");
  const raws = await Promise.all(keys.map(k => redis.get(k)));
  return raws.filter(Boolean).map(r => JSON.parse(r!) as Vehicle);
}
