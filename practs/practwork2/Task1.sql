--Task1
CREATE FUNCTION GetVisitorWatchMinuties
(
    @id int
)
RETURNS INT
AS
BEGIN
	DECLARE @watchtime int
	SET @watchtime = (SELECT SUM(Film.Duration)
	FROM Ticket INNER JOIN
		 Session ON Ticket.SessionId = Session.SessionId INNER JOIN
		 Film ON Session.FilmId = Film.FilmId
	WHERE Ticket.VisitorId = @id);

	RETURN ISNULL(@watchtime, 0);
END
