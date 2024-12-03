using Microsoft.Azure.Cosmos;

string accountEndpoint = "<Cosmos DB Endpoint>";

string key = "<Cosmos DB Access Key>";

CosmosHandler handler = new CosmosHandler(accountEndpoint, key);

Container container = await handler.GetOrCreateContainerAsync("TaxisDB", "TaxiRides", "/locationId");

Console.WriteLine(container.Id);
