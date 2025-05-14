import { redisPub as redis } from "../Redis/client";
import { CreateUserDTO }      from "../DTO/CreateUserDTO";
import { User }              from "../Models/User";

export async function createUser(
  data: CreateUserDTO
): Promise<User | null> {
  const key = `user:${data.license}`;
  if (await redis.exists(key)) return null;

  await redis.hSet(key, {
    firstName: data.firstName,
    lastName:  data.lastName,
    email:     data.email,
    license:   data.license,
    role:      "customer",
  });

  return {
    id:      data.license,
    role:    "customer",
    ...data
  };
}
