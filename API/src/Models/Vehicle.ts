export interface Vehicle {
  id:                 string;  // PK vehicleID
  model:              string;
  fabricant:          string;  // manufacturer
  seats:              number;
  transmissionType:   "manual" | "automatic";
  isAvailable:        boolean; 
}
