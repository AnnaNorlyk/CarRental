import jwt from "jsonwebtoken";
import { FakeUsers } from "../Data/FakeUserStorage"; //Replace with Redis later
import { LoginDTO } from "../DTO/LoginDTO";

// returns JWT on valid credentials, null otherwise
export function loginUser(data: LoginDTO): string | null {
  const user = FakeUsers.find(
    u => u.email === data.email && u.license === data.license
  );
  if (!user) return null;

  if (!process.env.JWT_SECRET) {
    throw new Error("JWT_SECRET missing");
  }


  const secret = process.env.JWT_SECRET as jwt.Secret;


  const token = jwt.sign(
    { userId: user.id, role: user.role },                 
    secret,                                             
    { expiresIn: process.env.JWT_EXPIRES_IN || "1h" } as jwt.SignOptions
  );

  return token;
}
