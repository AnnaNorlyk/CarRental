import { Router, Response } from "express";
import { requireAuth } from "../Middleware/RequireAuth";
import { roleChain }  from "../Middleware/RoleChain";

const router = Router();

router.put(
  "/bookings/:id",
  requireAuth,   // JWT check
  roleChain,     // Chain of responsibility role check
  (req, res) => {
    // Placeholder editing logic goes here
    res.status(501).json({ message: "'Edit booking' not yet implemented" });
  }
);

export default router;
