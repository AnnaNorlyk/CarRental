export interface Booking {
  id: string;                 
  vehicleId: string;          
  customerFirstName: string;
  customerLastName: string;
  customerEmail: string;
  customerLicenseId: string;
  customerMobile: string;
  startDate: number;          
  endDate: number;            
  keyStatus: "Not Retrieved" | "Retrieved" | "Dropped Off";
  dropoffCode: string;
  collectionCode: string;
}
