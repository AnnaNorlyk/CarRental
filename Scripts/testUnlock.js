;(async () => {
  const apiBase = "http://localhost:3000";
  const EMAIL   = "brian@test.com";
  const LICENSE = "LIC456";
  const VEHICLE = "vehicle123";

  // Ensure user exists
  console.log("→ POST /api/users");
  let res = await fetch(`${apiBase}/api/users`, {
    method:  "POST",
    headers: { "Content-Type": "application/json" },
    body:    JSON.stringify({
      firstName: "Brian",
      lastName:  "Larsen",
      email:     EMAIL,
      license:   LICENSE
    }),
  });
  console.log("  Status:", res.status, "Body:", await res.text());

  // Log in 
  console.log("\n→ POST /api/login");
  res = await fetch(`${apiBase}/api/login`, {
    method:  "POST",
    headers: { "Content-Type": "application/json" },
    body:    JSON.stringify({ email: EMAIL, license: LICENSE }),
  });
  if (!res.ok) {
    console.error("Login failed:", await res.text());
    return process.exit(1);
  }
  const { token } = await res.json();
  console.log("  Got token:", token);

  // Create booking 
  console.log("\n→ POST /api/bookings");
  const now       = new Date();
  const inOneHour = new Date(now.getTime() + 60 * 60 * 1000);

  res = await fetch(`${apiBase}/api/bookings`, {
    method:  "POST",
    headers: {
      "Content-Type":  "application/json",
      "Authorization": `Bearer ${token}`,
    },
    body: JSON.stringify({
      vehicleId:          VEHICLE,
      customerFirstName:  "Brian",
      customerLastName:   "Larsen",
      customerEmail:      EMAIL,
      customerLicenseId:  LICENSE,
      customerMobile:     "+4512345678",
      startDate:          now.toISOString(),
      endDate:            inOneHour.toISOString()
    }),
  });

  const booking = await res.json();
  console.log("  Status:", res.status, "Body:", booking);
  if (res.status !== 201) {
    console.error("Booking creation failed");
    return process.exit(1);
  }

  // Unlock drawer 
  console.log("\n→ POST /api/drawer/unlock");
  res = await fetch(`${apiBase}/api/drawer/unlock`, {
    method:  "POST",
    headers: {
      "Content-Type":  "application/json",
      "Authorization": `Bearer ${token}`,
    },

  });
  console.log("  Status:", res.status, "Body:", await res.text());

  process.exit(res.ok ? 0 : 1);
})().catch(err => {
  console.error(err);
  process.exit(1);
});
