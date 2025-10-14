using LabWork10.DTOs;
using LabWork10.Model;

namespace LabWork10.Extensions
{
    public static class FilmExtension
    {
        public static FilmDto? ToDto(this Film film)
            => film is null ? null : new FilmDto
            {
                FilmId = film.FilmId,
                Name = film.Name,
                Duration = film.Duration,
            };

        public static FilmGenreDto? ToFilmGenreDto(this Film film)
        {
            List<string> filmGeners = new();
            foreach (var genre in film.Genres)
                filmGeners.Add(genre.Name);

            return film is null ? null : new FilmGenreDto
            {
                FilmId = film.FilmId,
                FilmName = film.Name,
                Genres = filmGeners
            };
        }

        public static IEnumerable<FilmDto?> ToDtos(this IEnumerable<Film> films)
            => films.Select(f => f.ToDto());

        public static IEnumerable<FilmGenreDto?> ToFilmGenreDtos(this IEnumerable<Film> films)
            => films.Select(f => f.ToFilmGenreDto());
    }
}
