import { Request, Response, NextFunction } from "express";
import { RoleHandler } from "./RoleHandler";

export abstract class BaseRoleHandler implements RoleHandler {
  protected nextHandler: RoleHandler | null = null;

  public setNext(handler: RoleHandler): RoleHandler {
    this.nextHandler = handler;
    return handler;
  }

  public handle(req: Request, res: Response, next: NextFunction): void {
    if (this.nextHandler) {
      this.nextHandler.handle(req, res, next);
    } else {
      next();
    }
  }
}
