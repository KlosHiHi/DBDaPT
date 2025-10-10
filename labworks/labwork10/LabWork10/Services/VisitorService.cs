using LabWork10.Contexts;
using LabWork10.Model;
using Microsoft.EntityFrameworkCore;

namespace LabWork10.Services
{
    public class VisitorService(AppDbContext context)
    {
        private readonly AppDbContext _context = context;

        public async Task<List<Visitor>> GetAsync(int pageSize = 3, int currentPage = 1)
            => await _context.Visitors
                .Skip(pageSize * (currentPage - 1))
                .Take(pageSize)
                .ToListAsync();

        public async Task<Visitor> GetByIdAsync(int id)
            => await _context.Visitors.FindAsync(id) ?? null!;

    }
}
