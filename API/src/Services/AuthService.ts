import { sign, Secret, SignOptions } from "jsonwebtoken";
import { redisPub } from "../Redis/client";
import { LoginDTO } from "../DTO/LoginDTO";
import { UserHash } from "../Models/UserHash";

export async function loginUser(data: LoginDTO): Promise<string | null> {
  // Look up the user ID by license
  const userId = await redisPub.get(`user:license:${data.license}`);
  if (!userId) return null;

  // Retrieve the user hash and cast to the expected shape
  const user = (await redisPub.hGetAll(`user:${userId}`)) as unknown as UserHash;
  if (Object.keys(user).length === 0) return null;

  // Ensure the email and license match the stored values 
  if (user.email !== data.email || user.license !== data.license) return null;

  // Get the JWT secret from environment variables
  if (!process.env.JWT_SECRET) throw new Error("JWT_SECRET missing");
  const secret: Secret = process.env.JWT_SECRET;

  // Build the token options, using a default if none is provided
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
