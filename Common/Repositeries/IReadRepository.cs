using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.Repositories;

public interface IReadRepository<T> : IRepository<T> where T : class
{
    Task<IReadOnlyList<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
}