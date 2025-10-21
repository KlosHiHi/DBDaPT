CREATE PROCEDURE GetVisitorTickets
	@number VARCHAR(11)
AS
BEGIN
	PRINT 'Параметр: [' + @number + ']'
	SELECT TicketId, SessionId, Ticket.VisitorId, [Row], Seat 
	FROM Ticket
		LEFT JOIN Visitor ON Visitor.VisitorId = Ticket.VisitorId
	WHERE Visitor.Phone = @number
END

EXEC GetVisitorTickets @number = '71234567899'

ALTER PROCEDURE AddVisitor
	@phone CHAR(11),
	@newId INT OUTPUT
AS
BEGIN
	INSERT INTO Visitor(Phone)
	VALUES (@phone)
	SET @newId = SCOPE_IDENTITY()
	PRINT @newId
END

DECLARE @Id INT;
EXEC AddVisitor @phone = '71234567891', @newId = @Id OUTPUT

SELECT * FROM sys.procedures WHERE name = 'GetVisitorTickets';