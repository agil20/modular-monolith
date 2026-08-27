using System.Threading.Tasks;

namespace Common.Repositories;

public interface IWriteRepository<T> where T : class
{
    Task AddAsync(T entity);
    void Update(T entity);
    void Remove(T entity);
    Task SaveChangesAsync();
}