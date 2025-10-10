using LabWork10.Contexts;
using LabWork10.Model;
using Microsoft.EntityFrameworkCore;

namespace LabWork10.Services
{
    public class GenreService(AppDbContext context)
    {
        private readonly AppDbContext _context = context;

        public async Task<List<Genre>> GetAsync(int pageSize = 3, int currentPage = 1)
            => await _context.Genres
                .Skip(pageSize * (currentPage - 1))
                .Take(pageSize)
                .ToListAsync();

        public async Task<Genre> GetByIdAsync(int id)
            => await _context.Genres.FindAsync(id) ?? null!;
    }
}
