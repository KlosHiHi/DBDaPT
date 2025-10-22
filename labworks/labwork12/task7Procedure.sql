ALTER PROCEDURE GetVisitorTickets
	@number VARCHAR(11)
AS
	PRINT 'Параметр: [' + @number + ']'
	SELECT TicketId, SessionId, Ticket.VisitorId, [Row], Seat 
	FROM Ticket
		LEFT JOIN Visitor ON Visitor.VisitorId = Ticket.VisitorId
	WHERE Visitor.Phone = @number

EXEC GetVisitorTickets @number = '71234567899'

ALTER PROCEDURE AddVisitor
	@phone CHAR(11),
	@id INT OUTPUT
AS
	INSERT INTO Visitor(Phone)
	VALUES (@phone)
	SET @id = SCOPE_IDENTITY()
	PRINT @id

DECLARE @Id INT;
EXEC AddVisitor @phone = '71234567891', @newId = @Id OUTPUT

SELECT * FROM sys.procedures WHERE name = 'GetVisitorTickets';