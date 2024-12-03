using System;
using System.Collections.Generic;

public static void Run(IReadOnlyList<TaxiZone> input, ILogger log)
{
    if (input != null && input.Count > 0)
    {
        log.LogInformation("Documents modified " + input.Count);

        foreach(TaxiZone item in input)
        {
            string documentDetails = "\n"
                                   + "\n Document Id = "    + item.id
                                   + "\n Location Id = "    + item.locationId
                                   + "\n Borough = "        + item.borough
                                   + "\n Zone = "           + item.zone
                                   + "\n Service Zone = "   + item.serviceZone
                                   + "\n\n";

            log.LogInformation(documentDetails);
        }
    }
}

public class TaxiZone
{
    public string id { get; set; }
    public string locationId { get; set; }
    public string borough { get; set; }
    public string zone { get; set; }
    public string serviceZone { get; set; }
}