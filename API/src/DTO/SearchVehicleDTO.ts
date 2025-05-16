export interface SearchVehicleDTO {
  startDate:         string;              
  endDate:           string;               
  seats:             number;
  transmissionType:  "manual" | "automatic";
}
