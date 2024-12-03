using Microsoft.Azure.Cosmos;

/// <summary>
/// This class is used to create and maintain Cosmos DB entities
/// </summary>
public class CosmosHandler
{
    private readonly CosmosClient client;

    public CosmosClient Client
    {
        get
        {
            return client;
        }
    }

    /// <summary>
    /// Method to create Cosmos client by defining the options
    /// </summary>  
    public CosmosHandler(string accountEndpoint, string key)
    {
        // Class to define Cosmos client options
        CosmosClientOptions options = new () {
            
            ConnectionMode = ConnectionMode.Gateway,

            MaxRetryAttemptsOnRateLimitedRequests = 5,

            ConsistencyLevel = ConsistencyLevel.Session, 
            // Other options: Strong, Bounded Staleness, Consistent Prefix, Eventual

            ApplicationRegion = Regions.EastUS2,

            // ApplicationPreferredRegions = new List<string>() { Regions.EastUS2, Regions.WestUS2 }
        };

        /*
            Other option classes: RequestOptions, ItemRequestOptions, StoredProcedureRequestOptions, QueryRequestOptions, etc.
        */

        // Create Cosmos client using account endpoint and key
        client = new CosmosClient
        (
            accountEndpoint: accountEndpoint,
            
            authKeyOrResourceToken: key,

            clientOptions: options
        );
    }
    
    /// <summary>
    /// Method to get or create a Cosmos DB database
    /// </summary>
        
    public async Task<Database> GetOrCreateDatabaseAsync(string databaseName)
    {
        // Create database
        Database database = await client.CreateDatabaseIfNotExistsAsync(
            id: databaseName
        );

        // Return database
        return database;
    }

    /// <summary>
    /// Method to get or create a container in Cosmos DB database
    /// If partition key is empty, the container is created, else it is retrieved
    /// </summary>    
        
    public async Task<Container> GetOrCreateContainerAsync(string databaseName, 
                                                            string containerName,
                                                            string partitionKey)
    {
        // Get database
        Database database = await GetOrCreateDatabaseAsync(databaseName);        

        Container container;

        // Get container
        if (String.IsNullOrEmpty(partitionKey))
        {
            container = database.GetContainer(containerName);
        }

        // Create container
        else
        {
            ContainerProperties properties = new
                                             (
                                                 id: containerName,
                                                 partitionKeyPath: partitionKey

                                                 // throughput: 400
                                             );

            container = await database.CreateContainerIfNotExistsAsync(properties);
        }

        // Return container
        return container; 
    }

    /// <summary>
    /// Function to create item that is generically typed
    /// </summary>
    public async Task<double> CreateItemAsync<T>(Container container, T item)
    {
        var response = await container.CreateItemAsync<T>(item);

        return response.RequestCharge;
    }

    /// <summary>
    /// Function to replace item that is generically typed
    /// </summary>
    public async Task<double> ReplaceItemAsync<T>(Container container, T item, string id)
    {
        // Replace item that already exist

        var response = await container.ReplaceItemAsync<T>(item, id);

        return response.RequestCharge;
    }

    /// <summary>
    /// Function to upsert item that is generically typed
    /// </summary>
    public async Task<double> UpsertItemAsync<T>(Container container, T item)
    {
        // Replace item that already exist, else create a new one

        var response = await container.UpsertItemAsync<T>(item);

        return response.RequestCharge;
    }

    /// <summary>
    /// Function to delete item that is generically typed
    /// </summary>
    public async Task<double> DeleteItemAsync<T>(Container container, string id, PartitionKey partitionKey)
    {
        // Replace item that already exist, else create a new one

        var response = await container.DeleteItemAsync<T>(id, partitionKey);

        return response.RequestCharge;
    }
}

