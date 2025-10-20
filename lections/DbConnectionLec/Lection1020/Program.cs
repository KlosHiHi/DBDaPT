using Lection1020.Contexts;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

Console.WriteLine("Выполнение SQL-запросов средсвами ORM");

using GamesDbContext context = new();

var games = context.Games
    .Where(g => EF.Functions.Like(g.Name, "[a-d]%"));
Console.WriteLine(games.ToQueryString());

static async Task FromSql(GamesDbContext context)
{
    var games = context.Games
        .FromSql($"select * from game");
    Console.WriteLine(games.ToQueryString());
    await games.ForEachAsync(g =>
        Console.WriteLine($"{g.Name}, {g.Price} руб. [{g.CategoryId}]"));

    Console.WriteLine();

    int price = 1000;
    games = context.Games
        .FromSql($"select * from game where price < {price}");
    Console.WriteLine(games.ToQueryString());
    await games.ForEachAsync(g =>
        Console.WriteLine($"{g.Name}, {g.Price} руб. [{g.CategoryId}]"));

    Console.WriteLine();

    string columnName = "price";
    games = context.Games
        .FromSqlRaw($"select * from game order by {columnName}");
    Console.WriteLine(games.ToQueryString());
    await games.ForEachAsync(g =>
        Console.WriteLine($"{g.Name}, {g.Price} руб. [{g.CategoryId}]"));

    Console.WriteLine();

    string title = "SimCity";
    games = context.Games
        .FromSqlRaw($"select * from game where name = '{title}'");
    Console.WriteLine(games.ToQueryString());
    await games.ForEachAsync(g =>
        Console.WriteLine($"{g.Name}, {g.Price} руб. [{g.CategoryId}]"));

    Console.WriteLine();

    title = "SimCity";
    games = context.Games
        .FromSqlRaw($"select * from game where name = @p0", title);
    Console.WriteLine(games.ToQueryString());
    await games.ForEachAsync(g =>
        Console.WriteLine($"{g.Name}, {g.Price} руб. [{g.CategoryId}]"));

    Console.WriteLine();

    var sqlTitle = new SqlParameter("@title", "SimCity");

    games = context.Games
        .FromSqlRaw($"select * from game where name = @title", sqlTitle);
    Console.WriteLine(games.ToQueryString());
    await games.ForEachAsync(g =>
        Console.WriteLine($"{g.Name}, {g.Price} руб. [{g.CategoryId}]"));

    Console.WriteLine();
}

static async Task SqlQuery(GamesDbContext context)
{
    var titles = context.Database
        .SqlQuery<string>($"select name from game");
    Console.WriteLine(titles.ToQueryString());
    await titles.ForEachAsync(Console.WriteLine);

    Console.WriteLine();

    var minPrice = await context.Database
        .SqlQuery<decimal>($"select min(price) as value from game")
        .FirstOrDefaultAsync();
    Console.WriteLine(minPrice);

    //titles = context.Database.SqlQueryRaw();
}

static async Task ExecuteSql(GamesDbContext context)
{
    decimal addingPrice = -1m;

    int changedRows = await context.Database
        .ExecuteSqlAsync($"update game set price += {addingPrice}");
    Console.WriteLine($"изменено {changedRows} строк");
}