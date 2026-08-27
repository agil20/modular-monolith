using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.Repositories; 

public interface IReadRepository<T> where T : class
{
    Task<T?> GetByIdAsync(int id);
    Task<IReadOnlyList<T>> GetAllAsync();
}