using Microsoft.Azure.Cosmos;

string accountEndpoint = "<Cosmos DB Endpoint>";
string key = "<Cosmos DB Access Key>";

string databaseName = "TaxisDB";
string sourceContainerName = "Zones";
string leaseContainerName = "ZonesLease";
string destinationContainerName = "ZonesCopy";

// Create CosmosDB client
CosmosHandler handler = new CosmosHandler(accountEndpoint, key);

// Create handler for change feed
ChangeFeedHandler changeFeedHandler = new ChangeFeedHandler(handler.Client, 
                                                            databaseName,
                                                            sourceContainerName,
                                                            leaseContainerName,
                                                            destinationContainerName);

// Start change feed processor
ChangeFeedProcessor processor = await changeFeedHandler.StartChangeFeedProcessorAsync();

Console.ReadLine();

// Stop change feed processor
Console.WriteLine("Stopping Change Feed Processor...");
await processor.StopAsync();
Console.WriteLine("Stopped Change Feed Processor.");