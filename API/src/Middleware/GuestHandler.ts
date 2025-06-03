import { Request, Response, NextFunction } from "express";
import { BaseRoleHandler } from "./BaseRoleHandler";

export class GuestHandler extends BaseRoleHandler {
  public handle(req: Request, res: Response, next: NextFunction): void {
    res.status(403).json({ error: "Forbidden: insufficient role" });
  }
}
