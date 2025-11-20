--Task2
ALTER FUNCTION GetFilmsByGenre
(
    @genre nvarchar(50)
)
RETURNS TABLE AS RETURN
(
    SELECT Film.FilmId, Film.Name, STRING_AGG(Genre.Name, ', ') AS Genres
	FROM Film INNER JOIN
		 FilmGenre ON Film.FilmId = FilmGenre.FilmId INNER JOIN
		 Genre ON FilmGenre.GenreId = Genre.GenreId
	
	GROUP BY Film.FilmId, Film.Name
	HAVING CHARINDEX(@genre, STRING_AGG(dbo.Genre.Name, ', ')) > 0
)

SELECT *
FROM dbo.GetFilmsByGenre('триллер');
