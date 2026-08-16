using Modules.Categories.Contract.CategoryDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modules.Categories.Contract.Services
{
    public interface ICategoryModuleService
    {
        Task<string>? GetCategoryNameAsync(int id);

        Task<Dictionary<int,string>> GetCategoryNamesAsync(List<int> ids);

        Task<List<ResponseCategory>> Get();

        Task Update(int id, RequestCategoryCreate categorydto);

        Task Delete(int id);

        Task Post(RequestCategoryCreate requestCategoryCreate);
    }
}
