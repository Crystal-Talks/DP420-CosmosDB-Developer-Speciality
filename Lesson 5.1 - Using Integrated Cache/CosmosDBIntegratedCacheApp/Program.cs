using Microsoft.Azure.Cosmos;

string accountEndpoint = "<Cosmos DB Endpoint>";
string key = "<Cosmos DB Access Key>";

string databaseName = "TaxisDB";
string containerName = "TaxiRides";

CosmosHandler handler = new CosmosHandler(accountEndpoint, key);
Container container = await handler.GetOrCreateContainerAsync(databaseName, containerName, "");

string query = "SELECT * FROM c WHERE c.pickupLocationId = 141";

// Run query and print item details for first time
await handler.PrintItemsAsync<TaxiRide>(container, query);

// Run query and print item details for second time
await handler.PrintItemsAsync<TaxiRide>(container, query);
