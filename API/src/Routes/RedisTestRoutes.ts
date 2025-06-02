import { Router, Request, Response } from "express";
import { redisPub } from "../Redis/client";

const router = Router();

router.post("/test/cache", async (req: Request, res: Response) => {
  try {
    const { userId } = req.body;
    if (!userId) {
      res.status(400).json({ error: "Missing userId" });
      return;
    }

    // Try to get booking data from Redis cache
    const cached = await redisPub.get(`booking:${userId}`);
    if (cached) {
      console.log("[Redis] Hentet fra cache");
      res.json({ source: "cache", data: JSON.parse(cached) });
      return;
    }

    // Dummy eksample on bookingdata (normal from database)
    const bookingData = {
      userId,
      carId: "ABC123",
      startDate: "2025-06-10",
      endDate: "2025-06-12"
    };

    // Save in redis with expiering time on 60 seconds
    await redisPub.set(`booking:${userId}`, JSON.stringify(bookingData), { EX: 60 });
    console.log("[Redis] Gemmer i cache");

    res.json({ source: "fresh", data: bookingData });
  } catch (err) {
    res.status(500).json({ error: "Internal server error" });
  }
});

export default router;
