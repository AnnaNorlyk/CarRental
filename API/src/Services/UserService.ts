import { User } from "../Models/User";
import { FakeUsers } from "../Data/FakeUserStorage";
import { generateId } from "../Utils/IdGenerator";

export function createUser(data: Omit<User, "id" | "role">): User | null {
  const exists = FakeUsers.some(
    u => u.email === data.email || u.license === data.license
  );
  if (exists) return null;

  const newUser: User = {
    ...data,
    id: generateId(),
    role: "customer"
  };

  FakeUsers.push(newUser);
  return newUser;
}
