using LabWork10.Contexts;
using LabWork10.DTOs;
using LabWork10.Extensions;
using LabWork10.Model;
using LabWork10.Pagination;
using LabWork10.Sorts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace LabWork10.Services
{
    public class VisitorService(AppDbContext context)
    {
        private readonly AppDbContext _context = context;

        public async Task<List<Visitor>> GetAsync(Paginate pageInfo = null!, Sort sort = null!)
        {
            var visitors = _context.Visitors.AsQueryable();

            if (sort is not null)
                visitors = visitors.OrderBy($"{sort.ColumnName} {(sort.isDescending ? "desc" : "")}");

            return (pageInfo is not null) ?
                await visitors
                    .Skip(pageInfo.PageSize * (pageInfo.CurrentPage - 1))
                    .Take(pageInfo.PageSize)
                    .ToListAsync() :
                await visitors.ToListAsync();
        }

        public async Task<List<VisitorDto?>> GetDtoAsync()
        {
            return await _context.Visitors
                .Include(v => v.Tickets)
                .Select(v => v.ToDto())
                .ToListAsync();
        }

        public async Task<Visitor> GetByIdAsync(int id)
            => await _context.Visitors.FindAsync(id) ?? null!;

    }
}
