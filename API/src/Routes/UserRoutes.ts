import { Router, Request, Response } from "express";
import { CreateUserDTO} from "../DTO/CreateUserDTO";
import { createUser } from "../Services/UserService";

const UserRoutes = Router();

UserRoutes.post("/users", async (req: Request<{}, {}, CreateUserDTO>, res: Response) => {
  const { firstName, lastName, email, license } = req.body;

  if (!firstName || !lastName || !email || !license) {
    res.status(400).json({ error: "Missing required fields." });
    return;
  }

  const user = await createUser({ firstName, lastName, email, license });

  if (!user) {
    res.status(409).json({ error: "User already exists." });
    return;
  }

  res.status(201).json({ message: "User created", userId: user.id });
});


export default UserRoutes;