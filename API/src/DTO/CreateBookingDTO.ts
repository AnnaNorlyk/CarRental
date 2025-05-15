// src/DTO/CreateBookingDTO.ts
export interface CreateBookingDTO {
  vehicleId: string;
  customerFirstName: string;
  customerLastName: string;
  customerEmail: string;
  customerLicenseId: string;
  customerMobile: string;
  startDate: string;  // ISO 8601
  endDate:   string;  // ISO 8601
}

