// src/Routes/VehicleRoutes.ts
import { Router, Request, Response } from "express";
import { requireAuth }               from "../Middleware/RequireAuth";
import { getVehicleById, listAllVehicles } from "../Services/VehicleService";

const VehicleRoutes = Router();

// GET /api/vehicles
VehicleRoutes.get(
  "/vehicles",
  (req: Request, res: Response): void => {
    listAllVehicles()
      .then(all => res.json(all))
      .catch(err => {
        console.error("Error listing vehicles:", err);
        res.status(500).json({ error: "Internal server error" });
      });
  }
);

// GET /api/vehicles/:id
VehicleRoutes.get(
  "/vehicles/:id",
  (req: Request<{ id: string }>, res: Response): void => {
    const { id } = req.params;
    getVehicleById(id)
      .then(vehicle => {
        if (!vehicle) {
          res.status(404).json({ error: "Vehicle not found" });
        } else {
          res.json(vehicle);
        }
      })
      .catch(err => {
        console.error("Error fetching vehicle:", err);
        res.status(500).json({ error: "Internal server error" });
      });
  }
);

export default VehicleRoutes;
