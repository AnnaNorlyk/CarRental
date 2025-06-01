import { sign, Secret, SignOptions } from "jsonwebtoken";
import { redisPub }                 from "../Redis/client";
import { LoginDTO }                 from "../DTO/LoginDTO";
import { UserHash }                 from "../Models/UserHash";

export async function loginUser(data: LoginDTO): Promise<string | null> {
  const userKey = `user:${data.license}`;
  const user = (await redisPub.hGetAll(userKey)) as unknown as UserHash;

  // If no user exists under that license
  if (Object.keys(user).length === 0) return null;

  // Ensure the email and license match the stored values 
  if (user.email !== data.email || user.license !== data.license) return null;

  // Get the JWT secret from .env
  if (!process.env.JWT_SECRET) throw new Error("JWT_SECRET missing");
  const secret: Secret = process.env.JWT_SECRET;

  // Token options, using a default if none is provided
  const options = {
    expiresIn: process.env.JWT_EXPIRES_IN ?? "1h",
  } as SignOptions;

  // Create and return the signed JWT
  return sign(
    { license: user.license, role: user.role },
    secret,
    options
  );
}
