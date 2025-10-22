CREATE FUNCTION dbo.GetGamesByPrices
(
    @min DECIMAL(16,2),
    @max DECIMAL(16,2)
)
RETURNS TABLE AS RETURN
    SELECT *
	FROM Game
	WHERE Price <= @max AND Price >= @min


SELECT *
FROM dbo.GetGamesByPrices(1000, 2000)