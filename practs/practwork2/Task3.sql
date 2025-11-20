--Task3
ALTER PROCEDURE AddTicket
    @phone char(11),
    @sessionId int,
	@row tinyint,
	@seat tinyint,
	@id INT OUTPUT
AS
BEGIN
	INSERT INTO Ticket(SessionId, VisitorId, Row, Seat)
	SELECT @sessionId, Visitor.VisitorId, @row, @seat
	FROM Visitor 
	WHERE Visitor.Phone = @phone

	SET @id = SCOPE_IDENTITY();
END

DECLARE @id int;
EXEC AddTicket '79856586124', 4, 10, 10, @id OUTPUT
SELECT @id;