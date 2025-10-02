using LabWork8;
using LabWork8.Models;
using LabWork8.Repository;
using System.Data;

//Task 1 
DatabaseContext databaseContext = new("mssql", "ispp3101", "ispp3101", "3101");
using IDbConnection connection = databaseContext.CreateConnection();

connection.Open();

VisitorRepository visitorRepository = new(databaseContext);
GenreRepository genreRepository = new(databaseContext);

//Task 3
Visitor? visitor = await visitorRepository.GetByIdAsync(5);
Genre? genre = await genreRepository.GetByIdAsync(2);
Console.WriteLine($"Почта пользователя:{visitor.Email}, Жанр:{genre.Name}");

var visitors = await visitorRepository.GetAllAsync();
var genres = await genreRepository.GetAllAsync();

//Task 4
Visitor newVisitor = new()
{
    Name = "Игорь",
    BirthDate = DateTime.Now,
    Email = "fermaSKartinkami@gg.ty",
    Phone = "79330423409"
};

Genre newGenre = new() { Name = "Документальный фильм" };

Console.WriteLine($"id нового пользователя - {await visitorRepository.AddAsync(newVisitor)}");
Console.WriteLine($"id нового жанра - {await genreRepository.AddAsync(newGenre)}");

//Task 5
await genreRepository.DeleteAsync(10);
await visitorRepository.DeleteAsync(18);

visitor.Name = "Олег";
visitor.Email = "cacto888@mail.ru";

await visitorRepository.UpdateAsync(visitor);

genre.Name = "Мелодрама";

await genreRepository.UpdateAsync(genre);