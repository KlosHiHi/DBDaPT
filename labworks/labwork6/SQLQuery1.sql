--1st Task
CREATE TRIGGER TrSaveEmail
    ON Visitor
    AFTER UPDATE
    AS
		IF UPDATE(Email)
			INSERT INTO VisitorEmail(VisitorId, OldEmail)
			SELECT VisitorId, Email
			FROM deleted;

--2nd Task
CREATE TRIGGER TrDeleteFilm
	ON Film
	INSTEAD OF DELETE
	AS
		UPDATE Film
		SET IsDeleted = 1
		WHERE FilmId IN(SELECT FilmId
						FROM deleted);

--3rd Task
CREATE TRIGGER TrSaveVisitor
    ON Visitor
    AFTER DELETE
    AS
		INSERT INTO DeletedVisitor(VisitorId, Phone, [Name], BirthDate, Email)
		SELECT VisitorId, Phone, [Name], BirthDate, Email
		FROM deleted;

--4th Task
ALTER TRIGGER TrAddSessionPrice
ON Session
INSTEAD OF INSERT
AS
BEGIN
	DECLARE @newPrice decimal(4,0)
	SET @newPrice=(SELECT Price
				   FROM inserted);
	IF (@newPrice<100)
		INSERT INTO [Session](FilmId, HallId, Price, StartDate, IsFilm3d)
		SELECT FilmId, HallId, 100, StartDate, IsFilm3d
		FROM inserted;
END;

--5th Task
CREATE TRIGGER TrAddTicket
ON Ticket
INSTEAD OF INSERT
AS
BEGIN
	DECLARE @ticketRow tinyint
	DECLARE @ticketSeat tinyint
	DECLARE @maxRow tinyint
	DECLARE @maxSeat tinyint

	SELECT @ticketRow = i.[Row], @ticketSeat = i.Seat
	FROM inserted i;

	SELECT @maxRow = h.RowsCount, @maxSeat = h.SeatsCount
	FROM CinemaHall h 
		JOIN [Session] s ON s.HallId = h.HallId
		JOIN Ticket t ON t.SessionId = s.SessionId
	IF (@ticketRow > @maxRow OR @ticketSeat > @maxSeat)
		THROW 50000, 'Номер ряда или места выше допустимого', 1
END;

--6th Task
