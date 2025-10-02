using Dapper;
using LabWork8.Models;

namespace LabWork8.Repository
{
    public class VisitorRepository(DatabaseContext databaseContext) : IRepository<Visitor>
    {
        private readonly DatabaseContext _dbContext = databaseContext;

        public async Task<Visitor?> GetByIdAsync(int id)
            => await _dbContext.CreateConnection()
            .QueryFirstOrDefaultAsync<Visitor>("SELECT * FROM Visitor WHERE VisitorId=@id", new { id });

        public async Task<IEnumerable<Visitor>> GetAllAsync()
            => await _dbContext.CreateConnection()
            .QueryAsync<Visitor>("SELECT * FROM Visitor");

        public async Task<int> AddAsync(Visitor visitor)
            => ((int?)await _dbContext.CreateConnection()
            .ExecuteScalarAsync(@"INSERT INTO Visitor(Phone, Name, BirthDate, Email) 
OUTPUT INSERTED.VisitorId VALUES(@Phone, @Name, @BirthDate, @Email)", visitor)) ?? -1;

        public async Task DeleteAsync(int id)
            => await _dbContext.CreateConnection()
            .ExecuteAsync("DELETE Visitor WHERE VisitorId = @id", new { id });

        public async Task UpdateAsync(Visitor visitor)
            => await _dbContext.CreateConnection()
            .ExecuteAsync(@"UPDATE Visitor SET Phone = @Phone, Name = @Name, BirthDate = @BirthDate, Email = @Email 
WHERE VisitorId = @VisitorId", visitor);
    }
}
