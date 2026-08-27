using Common.Repositories;
using Modules.Categories.Domain;

namespace Modules.Categories.Application.Repositories;

public interface ICategoryRepository : IReadRepository<Category>, IWriteRepository<Category>
{
}