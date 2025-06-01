import { Router, Response, NextFunction } from "express";
import { AuthRequest }      from "../Middleware/AuthRequest";
import { requireAuth }      from "../Middleware/RequireAuth";
import { CreateBookingDTO } from "../DTO/CreateBookingDTO";
import { createBooking }    from "../Services/BookingService";

const router = Router();
console.log("[BookingRoutes] file loaded");   // ← This should print on server startup

router.post(
  "/bookings",
  requireAuth,
  async (req: AuthRequest, res: Response, next: NextFunction) => {
    console.log("[BookingRoutes] received POST /bookings"); // ← This should print per request
    // …the rest of your handler…
    try {
      const body = req.body as Partial<CreateBookingDTO>;
      // (validation omitted for brevity)
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
