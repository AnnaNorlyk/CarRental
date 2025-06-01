import { Request } from "express";

export interface AuthRequest extends Request {
  user?: {
    license: string;
    role:   string;
  };
}
