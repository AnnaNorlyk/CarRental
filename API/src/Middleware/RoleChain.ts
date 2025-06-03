import { Request, Response, NextFunction } from "express";
import { AdminHandler } from "./AdminHandler";
import { CustomerHandler } from "./CustomerHandler";
import { GuestHandler } from "./GuestHandler";

// Build the chain once and reuse it
const adminHandler = new AdminHandler();
const customerHandler = new CustomerHandler();
const guestHandler = new GuestHandler();

adminHandler.setNext(customerHandler).setNext(guestHandler);

export function roleChain(req: Request, res: Response, next: NextFunction) {
  adminHandler.handle(req, res, next);
}
