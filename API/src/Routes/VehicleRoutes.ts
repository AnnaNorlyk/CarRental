import { Router, Request, Response } from "express";
import { requireAuth } from "../Middleware/RequireAuth";
import { SearchVehicleDTO } from "../DTO/SearchVehicleDTO";
import {
  getVehicleById,
  listAllVehicles,
  searchVehicles
} from "../Services/VehicleService";

const VehicleRoutes = Router();

// GET /api/vehicles
VehicleRoutes.get(
  "/vehicles",
  requireAuth,
  (req: Request, res: Response): void => {
    listAllVehicles()
      .then(vehicles => res.json(vehicles))
      .catch(err => {
        console.error("Error listing vehicles:", err);
        res.status(500).json({ error: "Internal server error" });
      });
  }
);

// GET /api/vehicles/:id
VehicleRoutes.get(
  "/vehicles/:id",
  requireAuth,
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

// POST /api/vehicles/search
VehicleRoutes.post(
  "/vehicles/search",
  requireAuth,
  (req: Request<{}, {}, SearchVehicleDTO>, res: Response): void => {
    const { startDate, endDate, seats, transmissionType } = req.body;

    if (
      !startDate ||
      !endDate ||
      typeof seats !== "number" ||
      !["manual", "automatic"].includes(transmissionType)
    ) {
      res.status(400).json({ error: "Invalid search criteria." });
      return;
    }

    searchVehicles(req.body)
      .then(results => res.json(results))
      .catch(err => {
        // ← log the real error
        console.error("Vehicle search failed:", err);
        res.status(500).json({ error: err.message || "Internal server error" });
      });
  }
);

export default VehicleRoutes;
