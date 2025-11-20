--Task5

ALTER FUNCTION GetTodayFilmsByCinema
(
    @cinemaName nvarchar(50)
)
RETURNS TABLE AS RETURN
(
    SELECT Film.FilmId, Film.Name, FORMAT(Session.StartDate, 'HH:mm') AS StartTime, CinemaHall.HallNumber
	FROM CinemaHall INNER JOIN
		 Session ON CinemaHall.HallId = Session.HallId INNER JOIN
		 Film ON Session.FilmId = Film.FilmId
	WHERE CinemaHall.CinemaName = @cinemaName AND 
		(CAST(Session.StartDate AS date) = CAST(GETDATE() AS date))
)

SELECT * 
FROM dbo.GetTodayFilmsByCinema('Титан-Арена')
