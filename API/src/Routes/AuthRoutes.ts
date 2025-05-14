import { Router, Request, Response, NextFunction } from "express";
import { loginUser } from "../Services/AuthService";
import { LoginDTO }  from "../DTO/LoginDTO";

const AuthRoutes = Router();

AuthRoutes.post(
  "/login",
  async (req: Request, res: Response, next: NextFunction): Promise<void> => {
    try {
      const { email, license } = req.body as LoginDTO;

      if (!email || !license) {
        res.status(400).json({ error: "Missing email or license." });
        return;
      }

      const token = await loginUser({ email, license });
      if (!token) {
        res.status(401).json({ error: "Invalid credentials." });
        return;
      }

      res.json({ token });
    } catch (err) {
      next(err);  
    }
  }
);

export default AuthRoutes;
