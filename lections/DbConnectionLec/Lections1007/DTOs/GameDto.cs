using Lections1007.Model;
using System.Linq.Expressions;

namespace Lections1007.DTOs
{
    public class GameDto
    {
        public string Title { get; set; } = null!;
        public decimal Price { get; set; }
        public decimal Tax { get; set; }
        public string? Category { get; set; }
    }

    public static class GameExtension
    {
        public static GameDto? ToDto(this Game game)
            => game is null ? null : new GameDto
            {
                Title = game.Name,
                Price = game.Price,
                Tax = game.Price * 0.2m,
                Category = game.Category?.Name
            };

        public static IEnumerable<GameDto> ToDtos(this IEnumerable<Game> games)
                => games.ToDtos();
    }

    public static class GameExpression
    {
        public static Expression<Func<Game, GameDto>> ToDto
            => game => new GameDto
            {
                Title = game.Name,
                Price = game.Price,
                Tax = game.Price * 0.2m,
                Category = game.Category.Name
            };
    }
}