--Task1
SELECT        dbo.Session.SessionId, dbo.Film.Name, dbo.CinemaHall.CinemaName, dbo.CinemaHall.HallNumber, dbo.Session.Price, FORMAT(dbo.Session.StartDate, 'HH:mm') AS Start, FORMAT(DATEADD(minute, dbo.Film.Duration, 
                         dbo.Session.StartDate), 'HH:mm') AS [End], dbo.Film.Duration
FROM            dbo.Session INNER JOIN
                         dbo.Film ON dbo.Session.FilmId = dbo.Film.FilmId INNER JOIN
                         dbo.CinemaHall ON dbo.Session.HallId = dbo.CinemaHall.HallId
WHERE        (dbo.Session.StartDate >= GETDATE()) AND (CAST(dbo.Session.StartDate AS date) <= CAST(DATEADD(day, 1, GETDATE()) AS date))

--Task2
SELECT        dbo.Session.SessionId, dbo.Film.Name, dbo.CinemaHall.CinemaName, dbo.CinemaHall.HallId, FORMAT(dbo.Session.StartDate, 'HH:mm') AS Start, FORMAT(DATEADD(minute, dbo.Film.Duration, dbo.Session.StartDate), 
                         'HH:mm') AS [End], CAST(dbo.CinemaHall.RowsCount AS int) * CAST(dbo.CinemaHall.SeatsCount AS int) AS SeatsQuantity
FROM            dbo.Session INNER JOIN
                         dbo.CinemaHall ON dbo.Session.HallId = dbo.CinemaHall.HallId INNER JOIN
                         dbo.Film ON dbo.Session.FilmId = dbo.Film.FilmId

--Task3
SELECT        dbo.Film.FilmId, dbo.Film.Name, dbo.Film.ReleaseYear, CONCAT_WS('', CAST(FLOOR(dbo.Film.Duration / 60) AS VARCHAR(2)), ' ÷ ', CAST(FLOOR(dbo.Film.Duration % 60) AS VARCHAR(2)), ' ì ') AS Duration, 
                         STRING_AGG(dbo.Genre.Name, ', ') AS Genres, dbo.Film.Description, dbo.Film.RentalStart
FROM            dbo.Film INNER JOIN
                         dbo.FilmGenre ON dbo.Film.FilmId = dbo.FilmGenre.FilmId INNER JOIN
                         dbo.Genre ON dbo.FilmGenre.GenreId = dbo.Genre.GenreId
GROUP BY dbo.Film.FilmId, dbo.Film.Name, dbo.Film.ReleaseYear, dbo.Film.Description, dbo.Film.RentalStart, CONCAT_WS('', CAST(FLOOR(dbo.Film.Duration / 60) AS VARCHAR(2)), ' ÷ ', CAST(FLOOR(dbo.Film.Duration % 60) 
                         AS VARCHAR(2)), ' ì ')

--Task4
SELECT        FilmId, Name, ReleaseYear, Duration, Genres, Description, RentalStart
FROM            dbo.ViewFilmInfo
WHERE        (RentalStart >= GETDATE()) AND (RentalStart <= DATEADD(day, 30, GETDATE()))

--Task5
SELECT        dbo.Session.SessionId, dbo.Film.Name, dbo.Session.IsFilm3d, dbo.CinemaHall.CinemaName, dbo.CinemaHall.HallNumber, dbo.Session.StartDate
FROM            dbo.Session INNER JOIN
                         dbo.Film ON dbo.Session.FilmId = dbo.Film.FilmId INNER JOIN
                         dbo.CinemaHall ON dbo.Session.HallId = dbo.CinemaHall.HallId
WHERE        (dbo.Session.IsFilm3d = 1) AND (dbo.Session.StartDate >= GETDATE())