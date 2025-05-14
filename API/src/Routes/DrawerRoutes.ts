import { Router, Response, NextFunction } from "express";
import { AuthRequest }   from "../Middleware/AuthRequest";
import { requireAuth }   from "../Middleware/RequireAuth";
import { validatePickup } from "../Services/BookingService";
import { redisPub }      from "../Redis/client";

const router = Router();

router.post(
  "/drawer/unlock",
  requireAuth,
  async (req: AuthRequest, res: Response, next: NextFunction) => {
    try {
      //Confirm that the user has an active booking
      const bookingId = await validatePickup(req.user!.userId);

      // Publish the "open-cabinet" command
      await redisPub.publish("cabinet-channel", "open-cabinet");

      res.json({ message: "Unlock command sent", bookingId });
    } catch (err: any) {
      next(err);
    }
  }
);

export default router;
