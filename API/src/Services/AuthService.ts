import jwt from "jsonwebtoken";
import { redisPub } from "../Redis/client";
import { LoginDTO } from "../DTO/LoginDTO";

export async function loginUser(data: LoginDTO): Promise<string | null> {
  // Load the user by license
  const key = `user:${data.license}`;  
  const obj = await redisPub.hGetAll(key);
  if (!obj || obj.license !== data.license || obj.email !== data.email) {
    // if theres no record, or mismatch on email/license
    return null;
  }

  // Sign the JWT
  if (!process.env.JWT_SECRET) {
    throw new Error("JWT_SECRET missing");
  }
  const secret = process.env.JWT_SECRET as jwt.Secret;
  const options = { expiresIn: process.env.JWT_EXPIRES_IN || "1h" } as jwt.SignOptions;

  const token = jwt.sign(
    { license: obj.license, role: obj.role },
    secret,
    options
  );

  return token;
}
