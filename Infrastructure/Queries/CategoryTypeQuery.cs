using Application.Interfaces.Query;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Queries
{
   public class CategoryTypeQuery : ICategoryTypeQuery

    {
        private readonly AppDbContext _context;

        public CategoryTypeQuery(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CategoryType>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.CategoryTypes 
                                 .AsNoTracking()
                                 .ToListAsync(cancellationToken);
        }

        public async Task<CategoryType?> GetByIdAsync(int id, CancellationToken cancellationToken = default) 
        {
            return await _context.CategoryTypes
                                 .AsNoTracking()
                                 .FirstOrDefaultAsync(t => t.TypeId == id, cancellationToken);
        }
    }
}
