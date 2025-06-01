import { Request, Response, NextFunction } from "express";
import jwt from "jsonwebtoken";
import { AuthRequest } from "./AuthRequest";

export function requireAuth(
  req: AuthRequest,
  res: Response,
  next: NextFunction
): void {
  const authHeader = req.header("Authorization");
  if (!authHeader || !authHeader.startsWith("Bearer ")) {
    // no token provided
    res.status(401).json({ error: "Not authenticated" });
    return;
  }

  const token = authHeader.slice(7);
  try {
    // verify token
    const payload = jwt.verify(
      token,
      process.env.JWT_SECRET as jwt.Secret
    ) as { license?: string; role?: string };

    if (!payload.license || !payload.role) {
      res.status(401).json({ error: "Not authenticated" });
      return;
    }

    req.user = {
      license: payload.license,
      role:    payload.role,
    };

    next();
  } catch {
    res.status(401).json({ error: "Not authenticated" });
  }
}
