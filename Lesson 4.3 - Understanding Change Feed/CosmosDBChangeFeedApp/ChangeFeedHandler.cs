using Microsoft.Azure.Cosmos;

public class ChangeFeedHandler
{

    private CosmosClient client;
    private Container sourceContainer;
    private Container leaseContainer;
    private Container destinationContainer;
    
    /// <summary>
    /// Method to create Change Feed Handler by providing containers' details
    /// </summary>
    public ChangeFeedHandler(
                                CosmosClient cosmosClient, string databaseName, 
                                string sourceContainerName, string leaseContainerName,
                                string destinationContainerName
                            )
    {
        client = cosmosClient;

        // Get source, lease and destination containers
        sourceContainer = client.GetContainer(databaseName, sourceContainerName);
        leaseContainer = client.GetContainer(databaseName, leaseContainerName);
        destinationContainer = client.GetContainer(databaseName, destinationContainerName);
    }

    /// <summary>
    /// Method to create and start Cosmos DB change feed processor
    /// </summary>
    public async Task<ChangeFeedProcessor> StartChangeFeedProcessorAsync()
    {
        // Create a change feed processor
        ChangeFeedProcessor changeFeedProcessor = sourceContainer

                                                    // define function to handle changes
                                                    .GetChangeFeedProcessorBuilder<TaxiZone>
                                                            (
                                                                processorName: "TaxiRidesChangeProcessor",
                                                                onChangesDelegate: HandleChangesAsync
                                                            )

                                                    // define compute name to handle change feed
                                                    .WithInstanceName("TaxiRidesCompute")

                                                    // provide lease container
                                                    .WithLeaseContainer(leaseContainer)
                                                    
                                                    .Build();

        // Start change feed processor
        await changeFeedProcessor.StartAsync();

        Console.WriteLine("Started Change Feed Processor");
        
        return changeFeedProcessor;
    }

    /// <summary>
    /// Method to handle change feed returned by the processor
    /// </summary>
    public async Task HandleChangesAsync(
                                            ChangeFeedProcessorContext context,
                                            IReadOnlyCollection<TaxiZone> changedItems,
                                            CancellationToken cancellationToken
                                        )
    {
        // Loop and insert each changed item
        foreach (TaxiZone item in changedItems)
        {
            Console.WriteLine($"Item ID = {item.id}");
            
            // Update item in destination container
            await destinationContainer.UpsertItemAsync<TaxiZone>(item, new PartitionKey(item.id));

            await Task.Delay(10);
        }

        Console.WriteLine("\nAll changes handled");
    }
}