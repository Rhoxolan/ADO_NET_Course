CREATE PROCEDURE ShowAVGSellingsOnYear @year INT, @avgPrice INT OUTPUT
AS
SELECT @avgPrice = AVG(Stationeries.Price) FROM Sales JOIN Stationeries ON Sales.StationeryId = Stationeries.Id AND YEAR(Sales.DateOfSale) = @year