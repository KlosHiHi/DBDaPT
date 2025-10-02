using Dapper;
using LabWork8.Models;

namespace LabWork8.Repository
{
    public class GenreRepository(DatabaseContext databaseContext) : IRepository<Genre>
    {
        private readonly DatabaseContext _dbContext = databaseContext;

        public async Task<Genre?> GetByIdAsync(int id)
            => await _dbContext.CreateConnection()
            .QueryFirstOrDefaultAsync<Genre>("SELECT * FROM Genre WHERE GenreId=@id", new { id });

        public async Task<IEnumerable<Genre>> GetAllAsync()
            => await _dbContext.CreateConnection()
            .QueryAsync<Genre>("SELECT * FROM Genre");

        public async Task<int> AddAsync(Genre genre)
            => ((int?)await _dbContext.CreateConnection()
            .ExecuteScalarAsync("INSERT INTO Genre(Name) OUTPUT INSERTED.GenreId VALUES(@Name)", genre)) ?? -1;

        public async Task DeleteAsync(int id)
            => await _dbContext.CreateConnection()
            .ExecuteAsync("DELETE Genre WHERE GenreId = @id", new { id });

        public async Task UpdateAsync(Genre genre)
            => await _dbContext.CreateConnection()
            .ExecuteAsync("UPDATE Genre SET Name = @Name WHERE GenreId = @GenreId", genre);
    }
}
