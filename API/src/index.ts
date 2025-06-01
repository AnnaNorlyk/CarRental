import express from "express";
import dotenv from "dotenv";
import UserRoutes from "./Routes/UserRoutes"; 
import AuthRoutes from "./Routes/AuthRoutes"; 
import DrawerRoutes from "./Routes/DrawerRoutes";
import BookingRoutes from "./Routes/BookingRoutes";
import VehicleRoutes from "./Routes/VehicleRoutes";

dotenv.config();

const app = express();
app.use(express.json());


app.use("/api", AuthRoutes);
app.use("/api", UserRoutes);
app.use("/api", DrawerRoutes);
app.use("/api", BookingRoutes);
app.use("/api", VehicleRoutes);

const PORT = process.env.PORT || 3000;
app.listen(PORT, () => {
  console.log(`API running on port ${PORT}`);
});
