import { Request, Response, NextFunction } from "express";
import { BaseRoleHandler } from "./BaseRoleHandler";

export class AdminHandler extends BaseRoleHandler {
  public handle(req: Request, res: Response, next: NextFunction): void {
    const role = (req as any).user?.role;
    if (role === "admin") {
      return next();
    }
    super.handle(req, res, next);
  }
}
