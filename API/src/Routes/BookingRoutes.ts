import { Router, Response, NextFunction } from "express";
import { AuthRequest }      from "../Middleware/AuthRequest";
import { requireAuth }      from "../Middleware/RequireAuth";
import { CreateBookingDTO } from "../DTO/CreateBookingDTO";
import { createBooking }    from "../Services/BookingService";

const router = Router();
console.log("[BookingRoutes] file loaded");  

// Helper function to validate date
function isISO8601(str: string): boolean {
  return !isNaN(Date.parse(str));
}

router.post(
  "/bookings",
  requireAuth,
  async (req: AuthRequest, res: Response, next: NextFunction): Promise<void> => {
    console.log("[BookingRoutes] received POST /bookings");
    try {
      const body = req.body as Partial<CreateBookingDTO>;

      // List of required fields in CreateBookingDTO
      const required: Array<keyof CreateBookingDTO> = [
        "vehicleId",
        "customerFirstName",
        "customerLastName",
        "customerEmail",
        "customerLicenseId",
        "customerMobile",
        "startDate",
        "endDate",
      ];

      // Check that each required field is a not empty
      for (const key of required) {
        const val = body[key];
        if (typeof val !== "string" || !val.trim()) {
          res
            .status(400)
            .json({ error: `${key} is required..` });
          return;
        }
      }

      // Validate email format
      if (!/^\S+@\S+\.\S+$/.test(body.customerEmail!)) {
        res.status(400).json({ error: "Must be a valid email." });
        return;
      }

      if (!/^\+?\d{7,15}$/.test(body.customerMobile!)) {
        res.status(400).json({ error: "Must be a valid phone number." });
        return;
      }

      // Validate date strings 
      if (!isISO8601(body.startDate!)) {
        res.status(400).json({ error: "Must be a valid date." });
        return;
      }
      if (!isISO8601(body.endDate!)) {
        res.status(400).json({ error: "Must be valid date." });
        return;
      }

      // Ensure startDate is before endDate
      const start = new Date(body.startDate!);
      const end = new Date(body.endDate!);
      if (start >= end) {
        res
          .status(400)
          .json({ error: "endDate must be after than startDate." });
        return;
      }

      // Call service to create the booking, passing user license from AuthRequest
      const booking = await createBooking(
        req.user!.license!,
        body as CreateBookingDTO
      );

      res.status(201).json(booking);
    } catch (err: any) {
      next(err);
    }
  }
);

export default router;

