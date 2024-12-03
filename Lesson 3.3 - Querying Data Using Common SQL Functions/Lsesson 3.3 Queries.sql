SELECT TOP 10 *    
FROM c
WHERE c.type = 'rideEnd'

-- ===========================================
-- Working with Date & Time functions
-- ===========================================

-- Extracting date related fields

SELECT TOP 10
      c.rideId                                  
    , c.pickupTime                              
      
    , DateTimePart ("yyyy", c.pickupTime)       AS tripYear
    , DateTimePart ("mm", c.pickupTime)         AS tripMonth
    , DateTimePart ("dd", c.pickupTime)         AS tripDay
    
FROM c


-- Filtering by dates and Adding/subtracting - Manipulating dates
SELECT
       c.rideId                                
    ,  c.pickupTime                            
    
    , DateTimeAdd ("hh", -1, c.pickupTime)     AS newPickupTime                                -- Subtract 1 hour from PickupTime

    , DateTimeDiff("dd", c.pickupTime, GetCurrentDateTime())  AS diffBwCurrentAndPickupTime   -- Subtract PickupTime from current date

FROM TaxiRides c

WHERE c.type = 'rideStart'

ORDER BY c.rideId


-- ===========================================
-- Working with Numeric functions
-- ===========================================

-- Rounding off values

SELECT 
       ROUND (1022.72)          AS amt_NoDecimal   	 
     , ROUND (1022.72, 1)       AS amt_OneDecimal  
     , ROUND (1022.72, -1)      AS amt_OnesPlace
     
     , CEILING  (1022.72)       AS amt_Ceil_NoDecimal
     , FLOOR (1022.72)          AS amt_Floor_NoDecimal


-- Apply numeric functions on Amount attribute

SELECT c.rideId
     , stringToNumber(c.totalAmount) AS Amount

     , ROUND (stringToNumber(c.totalAmount))         AS amt_NoDecimal   	 
     , ROUND (stringToNumber(c.totalAmount), 1)      AS amt_OneDecimal  
     , ROUND (stringToNumber(c.totalAmount), -1)     AS amt_OnesPlace
     
     , CEILING (stringToNumber(c.totalAmount))       AS amt_Ceil_NoDecimal
     , FLOOR   (stringToNumber(c.totalAmount))       AS amt_Floor_NoDecimal

FROM c
WHERE c.type = 'rideEnd'


-- =======================================================
-- Working with Conditional and Type Checking functions
-- =======================================================

-- Applying If-Else conditions

SELECT
	  c.rideId, c.rateCodeId

    , IIF (c.rateCodeId = 5, 'SharedTrip', 'SoloTrip')       AS tripType
    	
FROM c


-- Perform Null checks

SELECT c.rideId, c.type, c.totalAmount

    , IS_DEFINED (c.totalAmount)    AS isDefined

    , IS_NULL    (c.totalAmount)    AS isNull       -- IS_STRING, IS_NUMBER, IS_INTEGER, IS_ARRAY, etc.

FROM c
ORDER BY c.rideId


-- ===========================================
-- Working with String functions
-- ===========================================

SELECT TOP 10
       c.name

     , LOWER(c.name)                                  AS cabOwner     		-- Use UPPER to convert to upper case
     
     , REPLACE( LOWER(c.name), ' and ', ' & ')        AS cabOwnerUpdatedName

     -- If address field is more than 30 characters, truncate the field
     , c.address
     , IIF (
                LENGTH(c.address) <= 30
                    , c.address
                    , CONCAT (
                                SUBSTRING (c.address, 0, 27),
                                '...' 
                             )
           )                                AS truncatedAddress    

FROM c

WHERE c.name LIKE '% AND %'


-- ===========================================
-- Working with Aggregate functions
-- ===========================================

SELECT 
        DateTimePart("yyyy", GetCurrentDateTime()) - c.vehicleYear AS CurrentVehicleAge

        , COUNT(1) AS TotalVehicles

FROM c

WHERE c.active = 'YES'

GROUP BY (DateTimePart("yyyy", GetCurrentDateTime()) - c.vehicleYear)
