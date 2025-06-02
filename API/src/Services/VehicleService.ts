import { redisPub as redis } from "../Redis/client";
import { Vehicle } from "../Models/Vehicle";
import { SearchVehicleDTO } from "../DTO/SearchVehicleDTO";

// get a vehicle by ID
export async function getVehicleById(id: string): Promise<Vehicle | null> {
  const raw = await redis.get("vehicle:" + id);
  if (!raw) return null;
  return JSON.parse(raw) as Vehicle;
}

// list all vehicles
export async function listAllVehicles(): Promise<Vehicle[]> {
  const keys = await redis.keys("vehicle:*");
  const raws = await Promise.all(keys.map(k => redis.get(k)));
  const out: Vehicle[] = [];
  for (const r of raws) {
    if (r) out.push(JSON.parse(r) as Vehicle);
  }
  return out;
}

// find vehicles matching criteria and not booked in the given window
export async function searchVehicles(
  criteria: SearchVehicleDTO
): Promise<Vehicle[]> {
  const startMs = new Date(criteria.startDate).getTime();
  const endMs   = new Date(criteria.endDate).getTime();

  // filter by seats and transmission
  const candidates = (await listAllVehicles()).filter(v =>
    v.seats === criteria.seats &&
    v.transmissionType === criteria.transmissionType
  );
  if (candidates.length === 0) return [];

  // load booking keys
  const allKeys = await redis.keys("booking:*");
  const bookingKeys = allKeys.filter(k => !k.startsWith("booking:user:"));

  const bookingHashes = await Promise.all(
    bookingKeys.map(k => redis.hGetAll(k))
  );

  // remove any vehicle with an overlapping booking
  const available: Vehicle[] = [];
  for (const v of candidates) {
    let hasOverlap = false;
    for (const h of bookingHashes) {
      if (h["vehicleId"] === v.id) {
        const bStart = Number(h["startDate"]);
        const bEnd   = Number(h["endDate"]);
        // check overlap
        if (!(endMs <= bStart || startMs >= bEnd)) {
          hasOverlap = true;
          break;
        }
      }
    }
    if (!hasOverlap) available.push(v);
  }

  return available;
}
