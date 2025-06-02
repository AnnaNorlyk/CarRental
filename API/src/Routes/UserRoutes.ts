import { Router, Request, Response } from "express";
import { CreateUserDTO } from "../DTO/CreateUserDTO";
import { createUser } from "../Services/UserService";

const UserRoutes = Router();

UserRoutes.post("/users", (req: Request<{}, {}, CreateUserDTO>, res: Response): void => {
  const dto = req.body;  

  const { firstName, lastName, email, license } = dto;
  if (!firstName || !lastName || !email || !license) {
    res.status(400).json({ error: "Missing required fields." });
    return;
  }

  createUser(dto)
    .then(user => {
      if (!user) {
        res.status(409).json({ error: "User already exists." });
      } else {
        res.status(201).json({ message: "User created", license: user.license });
      }
    })
    .catch(err => {
      console.error("Error in createUser:", err);
      res.status(500).json({ error: "Internal server error" });
    });
});

export default UserRoutes;
