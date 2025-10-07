using Lections1007.Contexts;
using Microsoft.EntityFrameworkCore;

Console.WriteLine("Применение ORM [EF Core]");

using var context = new AppDbContext();

var categories = context.Categories.Include(c => c.Games);
foreach(var category in categories)
    Console.WriteLine($"[{category.CategoryId}] - {category.Name}, {category.Games?.Count()}");

Console.WriteLine();

var games = context.Games.Include(g => g.Category);
foreach(var game1 in games) 
    Console.WriteLine($"[{game1.GameId}] - {game1.Name}, {game1.Category?.Name}");


var btGame = context.Games.FirstOrDefault(x => x.GameId == 3);

var game = await context.Games.FindAsync(1);
game = context.Games.Find(1);

game = context.Games.FirstOrDefault(g => g.GameId > 2);
game = await context.Games.FirstOrDefaultAsync(g => g.GameId > 2);

game = context.Games.SingleOrDefault(g => g.GameId == 2); //Должен принимать лишь одно вхождение
game = await context.Games.SingleOrDefaultAsync(g => g.GameId == 2);