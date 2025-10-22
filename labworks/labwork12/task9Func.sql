CREATE FUNCTION dbo.GetSessionByFilmId
(
    @id INT
)
RETURNS TABLE AS RETURN
    SELECT [Session].FilmId, SessionId, HallId, Price, StartDate, IsFilm3d
	FROM [Session]
		JOIN Film ON Session.FilmId = Film.FilmId
	WHERE Film.FilmId = @id

SELECT *
FROM dbo.GetSessionByFilmId(1)