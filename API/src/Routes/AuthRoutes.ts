import { Router, Request, Response } from "express";
import { LoginDTO } from "../DTO/LoginDTO";
import { loginUser } from "../Services/AuthService";

const AuthRoutes = Router();

AuthRoutes.post(
  "/login",
  (req: Request<{}, {}, LoginDTO>, res: Response): void => {
    const { email, license } = req.body;

    if (!email || !license) {
      res.status(400).json({ error: "Missing email or license." });
      return;
    }

    const token = loginUser({ email, license });

    if (!token) {
      res.status(401).json({ error: "Invalid. Please try again." });
      return;
    }

    res.json({ token });
  }
);

export default AuthRoutes;
