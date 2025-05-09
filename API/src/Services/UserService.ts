import { redisPub as redis } from "../Redis/client";
import { User } from "../Models/User";
import { generateId } from "../Utils/IdGenerator";

//Creates a user if email & license are unique. Returns null on conflict.
export async function createUser(
  data: Omit<User, "id" | "role">
): Promise<User | null> {
  const id         = generateId();
  const emailKey   = `user:email:${data.email}`;
  const licenseKey = `user:license:${data.license}`;
  const userKey    = `user:${id}`;

  // atomic transaction
  const tx = redis.multi()
    .setNX(emailKey,   id)   
    .setNX(licenseKey, id)    
    .hSet(userKey, {          
      id,
      firstName: data.firstName,
      lastName:  data.lastName,
      email:     data.email,
      license:   data.license,
      role:      "customer"
    });

    const res = await tx.exec() as [ [null, number], [null, number], unknown ];

    const emailOk   = res[0][1] === 1;
    const licenseOk = res[1][1] === 1;
    

  if (!emailOk || !licenseOk) {
    if (emailOk)   await redis.del(emailKey);   
    if (licenseOk) await redis.del(licenseKey);
    return null;
  }

  return { ...data, id, role: "customer" };
}
