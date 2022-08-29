CREATE PROCEDURE ShowAVGSellingsOnYear (year integer, OUT avgPrice float)
LANGUAGE SQL
AS $$
SELECT AVG("Stationeries"."Price"::numeric)::float FROM "Sales" JOIN "Stationeries"  ON "Sales"."StationeryId" = "Stationeries"."Id"
$$