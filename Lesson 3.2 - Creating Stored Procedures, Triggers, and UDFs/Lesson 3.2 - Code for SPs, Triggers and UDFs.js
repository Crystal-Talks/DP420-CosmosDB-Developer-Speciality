===========================================
1. First Stored Procedure
===========================================

function helloWorld (name) {

    var context = getContext();

    var response = context.getResponse();

    response.setBody("Hello " + name);
}

SP ID - helloWorld

Execute - PK - Nothing, Name - Mohit


===========================================
2. Stored Procedure - Create taxi ride items
===========================================

function createTaxiRideItem(document) {

    var context = getContext();

    var response = context.getResponse();
    var container = context.getCollection();

    var containerLink = container.getSelfLink();

    if (!document)
        throw new Error("The document is undefined or null.");
    
    var accepted = container.createDocument(
        
         containerLink,

         document,

         function (error, newItem) {
             if (error)
                    throw new Error('Error' + error.message);

             response.setBody(newItem);
         }
     );

     if (!accepted)
        return;
}

/* Items to insert

{"rideId":"1002","type":"rideStart","vendorId":1,"pickupTime":"2024-02-07T00:00:00.000Z","cabLicense":"TAC399","driverLicense":5131685,"pickupLocationId":170,"passengerCount":1,"rateCodeId":1}

{"rideId":"5001","type":"rideStart","vendorId":1,"pickupTime":"2024-02-07T00:00:00.000Z","cabLicense":"TAC399","driverLicense":5131685,"pickupLocationId":170,"passengerCount":1,"rateCodeId":1}

*/

===========================================
3. Triggers
===========================================

function validateTaxiRideTimestamp() {

    var context = getContext();
    var request = context.getRequest();

    // Item getting created in current operation
    var itemToCreate = request.getBody();

    // Validate properties of item
    if (!("pickupTime" in itemToCreate)) {

        var currentDate = new Date();
        itemToCreate["pickupTime"] = currentDate.getTime();

    }

    // Update the item to be created
    request.setBody(itemToCreate);
}


===========================================
4. User-defined function
===========================================

function tripType(rateCodeId) {

    var tripType = '';

    if (tripType == undefined)
        throw 'Rate Code Id must be defined';

    if (rateCodeId == 1)
        tripType = 'Shared Trip';

    else if (rateCodeId == 2)
        tripType = 'Group Booking';

    else
        tripType = 'Solo Trip';

    return tripType;

}

/* Query to use UDF

SELECT *
FROM TaxiRides c
WHERE udf.tripType(c.rateCodeId) = 'Solo Trip'

*/