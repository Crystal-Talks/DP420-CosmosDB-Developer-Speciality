using Microsoft.Azure.Cosmos;

string accountEndpoint = "<Cosmos DB Endpoint>";

string key = "<Cosmos DB Access Key>";

CosmosHandler handler = new CosmosHandler(accountEndpoint, key);

Container container = await handler.GetOrCreateContainerAsync("TaxisDB", "TaxiRides", "");

Console.WriteLine(container.Id);

Create TaxiRideStart item
TaxiRideStart item3 = new()
                     {
                        id               = "10003",
                        rideId           = "10003",
                        type             = "rideStart",
                        vendorId         = 1,
                        pickupTime       = Convert.ToDateTime("2024-02-07T10:03:00.000Z"),
                        cabLicense       = "TAC399",
                        driverLicense    = 5131688,
                        pickupLocationId = 161,
                        passengerCount   = 1,
                        rateCodeId       = 1
                     };

// Creates item by calling generic type method
var rc_genericTypeCreate = await handler.CreateItemAsync<TaxiRideStart>(container, item3);
Console.WriteLine(rc_genericTypeCreate);


// Replaces item by calling generic type method
item3.passengerCount = 2;

var rc_genericTypeReplace = await handler.ReplaceItemAsync<TaxiRideStart>(container, item3, item3.id);
Console.WriteLine(rc_genericTypeReplace);


// Deletes item by calling generic type method
var rc_genericTypeDelete = await handler.DeleteItemAsync<TaxiRideStart>(container, item3.id, new PartitionKey(item3.id));
Console.WriteLine(rc_genericTypeDelete);
















