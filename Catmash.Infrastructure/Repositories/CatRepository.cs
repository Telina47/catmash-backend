using Catmash.Domain.Interfaces;
using Catmash.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catmash.Infrastructure.Repositories
{
    public class CatRepository : ICatRepository
    {
        private readonly ApplicationDbContext _context;

        public CatRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Cat?> GetByIdAsync(string id)
        {
            return await _context.Cats.FindAsync(id);
        }

        public async Task<List<Cat>> GetTwoRandomCatsAsync()
        {
            var allCats = await _context.Cats.ToListAsync();

            var random = new Random();
            return allCats
                .OrderBy(_ => random.Next())
                .Take(2)
                .ToList();
        }


        public async Task<List<Cat>> GetTopCatsAsync(int count)
        {
            return await _context.Cats
                .OrderByDescending(c => c.Score)
                .Take(count)
                .ToListAsync();
        }

        public async Task UpdateAsync(Cat cat)
        {
            _context.Cats.Update(cat);
            await _context.SaveChangesAsync();
        }

        public async Task AddAsync(Cat cat)
        {
            await _context.Cats.AddAsync(cat);
            await _context.SaveChangesAsync();
        }
    }
}
