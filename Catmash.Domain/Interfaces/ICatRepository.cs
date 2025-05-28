using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catmash.Domain.Interfaces
{
    public interface ICatRepository
    {
        Task<Cat?> GetByIdAsync(string id);
        Task<List<Cat>> GetTwoRandomCatsAsync();
        Task<List<Cat>> GetTopCatsAsync(int count);
        Task UpdateAsync(Cat cat);
        Task AddAsync(Cat cat);
    }
}
