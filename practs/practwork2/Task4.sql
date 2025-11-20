--Task4
ALTER PROCEDURE AddCinemaHall
    @cinemaName NVARCHAR(50) = 'Титан-Арена',
    @hallNumber TINYINT,
	@rowsCount INT,
	@seatsCount INT
AS
BEGIN
	IF EXISTS(SELECT  HallId
			FROM CinemaHall
			WHERE CinemaName = @cinemaName AND HallNumber = @hallNumber)
		INSERT INTO CinemaHall(CinemaName, HallNumber, RowsCount, SeatsCount, IsVip)
		VALUES (@cinemaName, @hallNumber, @rowsCount, @seatsCount, 0)
	ELSE
		UPDATE CinemaHall
		SET RowsCount = @rowsCount,
			SeatsCount = @seatsCount
		WHERE CinemaName = @cinemaName AND HallNumber = @hallNumber
END

EXEC AddCinemaHall 'Русь', 3, 30, 20;

EXEC AddCinemaHall 'Титан-Арена', 1, 15, 20;