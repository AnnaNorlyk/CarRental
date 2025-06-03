import { Request, Response, NextFunction } from "express";

export interface RoleHandler {
  setNext(handler: RoleHandler): RoleHandler;
  handle(req: Request, res: Response, next: NextFunction): void;
}
