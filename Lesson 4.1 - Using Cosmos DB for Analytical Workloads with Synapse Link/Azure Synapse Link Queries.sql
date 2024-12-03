CREATE CREDENTIAL [oreillycosmosdb]
WITH IDENTITY = 'SHARED ACCESS SIGNATURE', SECRET = '<Enter your Azure Cosmos DB key here>'
GO

SELECT TOP 100 *
FROM OPENROWSET(​PROVIDER = 'CosmosDB',
                CONNECTION = 'Account=<Name of Cosmos DB account>;Database=<Name of database>',
                OBJECT = '<Name of container>',
                SERVER_CREDENTIAL = 'oreillycosmosdb'
) AS [TaxiRides]