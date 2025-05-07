import express from "express";
import dotenv from "dotenv";
import UserRoutes from "./Routes/UserRoutes"; 

dotenv.config();

const app = express();
app.use(express.json());

app.use("/api", UserRoutes);

const PORT = process.env.PORT || 3000;
app.listen(PORT, () => {
  console.log(`API running on port ${PORT}`);
});
