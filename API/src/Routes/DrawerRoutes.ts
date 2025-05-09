
import { Router } from "express";
import { requireAuth } from "../Middleware/requireAuth";
import { redisPub } from "../Redis/client";

const router = Router();

router.post("/drawer/unlock", requireAuth, async (req, res) => {
  await redisPub.publish("cabinet-channel", "open-cabinet");   // 🔑
  res.json({ message: "Unlock command sent" });
});

router.post("/drawer/lock", requireAuth, async (req, res) => {
  await redisPub.publish("cabinet-channel", "close-cabinet");
  res.json({ message: "Lock command sent" });
});

export default router;
