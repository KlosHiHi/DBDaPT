
CREATE PROCEDURE dbo.GetGamesByPrice 
    @maxPrice DECIMAL(16,2) 
AS
    SELECT *
	FROM Game
	WHERE Price <= @maxPrice

EXEC dbo.GetGamesByPrice  1000

CREATE PROCEDURE dbo.AddCategory
    @name NVARCHAR(100),
    @id int OUTPUT 
AS
BEGIN
	INSERT INTO Category([Name]) 
	VALUES(@name)
END