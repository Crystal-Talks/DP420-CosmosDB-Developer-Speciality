using Microsoft.Azure.Cosmos;

string accountEndpoint = "<Cosmos DB Endpoint>";

string key = "<Cosmos DB Access Key>";

CosmosHandler handler = new CosmosHandler(accountEndpoint, key);

Container container = await handler.GetOrCreateContainerAsync("TaxisDB", "TaxiRides", "");

Console.WriteLine();

// Read a single item
Console.WriteLine("Read a single item");
await handler.ReadItemAsync<TaxiRideStart>(container, "10001", "10001");
Console.WriteLine();

// Query the container with id and partition key
string query = "SELECT * FROM c WHERE c.rideId = \"10001\" AND c.id = \"10001\"";
Console.WriteLine(query);
await handler.ReadItemsAsync<TaxiRideStart>(container, query);
Console.WriteLine();

// Query the container on id
query = "SELECT * FROM c WHERE c.id = \"10001\"";
Console.WriteLine(query);
await handler.ReadItemsAsync<TaxiRideStart>(container, query);
Console.WriteLine();

// Query the container on partition key
query = "SELECT * FROM c WHERE c.rideId = \"10001\"";
Console.WriteLine(query);
await handler.ReadItemsAsync<TaxiRideStart>(container, query);
Console.WriteLine();

// Query the container on a non-id, non-partition key attribute
query = "SELECT * FROM c WHERE c.cabLicense = \"TAC399\"";
Console.WriteLine(query);
await handler.ReadItemsAsync<TaxiRideStart>(container, query);
Console.WriteLine();

