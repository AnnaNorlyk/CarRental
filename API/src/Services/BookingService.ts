import { v4 as uuid } from "uuid";
import { redisPub as redis } from "../Redis/client";
import { CreateBookingDTO } from "../DTO/CreateBookingDTO";
import { Booking } from "../Models/Booking";
import { getVehicleById } from "./VehicleService";

class Forbidden extends Error { status = 403; }


//Create a new booking record in Redis.
export async function createBooking(userId: string, dto: CreateBookingDTO): Promise<Booking> {

  // Verify vehicle exists and is available
  const vehicle = await getVehicleById(dto.vehicleId);
  if (!vehicle || !vehicle.isAvailable) {
    const err: any = new Error("Vehicle not found or unavailable");
    err.status = 400;
    throw err;
}

  const dropoffCode    = Math.random().toString(36).slice(-6).toUpperCase();
  const collectionCode = Math.random().toString(36).slice(-6).toUpperCase();

  const booking: Booking = {
    id: uuid(),
    vehicleId: dto.vehicleId,
    customerFirstName: dto.customerFirstName,
    customerLastName:  dto.customerLastName,
    customerEmail:     dto.customerEmail,
    customerLicenseId: dto.customerLicenseId,
    customerMobile:    dto.customerMobile,
    startDate:  new Date(dto.startDate).getTime(),
    endDate:    new Date(dto.endDate).getTime(),
    keyStatus:  "Not Retrieved",
    dropoffCode,
    collectionCode,
  };

const key = `booking:${booking.id}`;
  // Store all booking fields in a hash
  await redis.hSet(key, {
    ...Object.fromEntries(
      Object.entries(booking).map(([k, v]) => [k, v.toString()])
    ),
    userId,
  });

  // Map user to booking ID
  const userKey = `booking:user:${userId}`;
  await redis.set(userKey, booking.id);

  return booking;
}

//Mark the booking as picked up (retrieved).
export async function pickupBooking(bookingId: string): Promise<void> {
  const key = `booking:${bookingId}`;
  await redis.hSet(key, "keyStatus", "Retrieved");
}

//Marks the booking as dropped off (returned).
export async function dropoffBooking(bookingId: string): Promise<void> {
  const key = `booking:${bookingId}`;
  await redis.hSet(key, "keyStatus", "Dropped Off");
}

/**
 * Validates that the user has an active booking
 * and that the key hasn’t yet been retrieved or returned.
 * Returns the bookingId if valid
 */
export async function validatePickup(license: string): Promise<string> {
  const bookingId = await redis.get(`booking:user:${license}`);

  if (!bookingId) throw new Forbidden("No active booking");

  const data = await redis.hGetAll(`booking:${bookingId}`);
  if (!Object.keys(data).length) throw new Forbidden("Booking not found");

  const now = Date.now();
  if (Number(data.startDate) > now || Number(data.endDate) < now) {
    throw new Forbidden("Not within pickup window");
  }
  if (data.keyStatus !== "Not Retrieved") {
    throw new Forbidden("Booking already picked up or returned");
  }

  return bookingId;
}
