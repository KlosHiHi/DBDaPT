using LabWork12.Contexts;
using LabWork12.Models;
using LabWork12.Sorts;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace LabWork12.Services
{
    public class VisitorService(CinemaDbContext context)
    {
        private CinemaDbContext _context = context;

        public async Task<List<Visitor>> GetAllOrderedAsync(Sort sort)
            => sort.isDescending
                ? await _context.Visitors.FromSqlRaw($"select * from visitor order by {sort.ColumnName} desc").ToListAsync()
                : await _context.Visitors.FromSqlRaw($"select * from visitor order by {sort.ColumnName}").ToListAsync();

        public async Task<int> AddVisitor(string phone)
        {
            var phoneParam = new SqlParameter("@phone", phone);

            var param = new SqlParameter
            {
                ParameterName = "@newId",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output
            };

            await _context.Database.ExecuteSqlRawAsync($"EXEC AddVisitor @phone, @newId OUT", phoneParam, param);

            return (int)param.Value;
        }
    }
}