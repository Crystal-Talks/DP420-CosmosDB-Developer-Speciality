// Class to define Taxi Ride items with type as 'rideStart'

public class TaxiRideStart
{
    public string? id { get; set; }
    public string? rideId { get; set; }
    public string? type { get; set; }
    public int vendorId { get; set; }
    public DateTime pickupTime { get; set; }
    public string? cabLicense { get; set; }
    public int driverLicense { get; set; }
    public int pickupLocationId { get; set; }
    public int passengerCount { get; set; }
    public int rateCodeId { get; set; }

}