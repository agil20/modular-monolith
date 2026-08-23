using Modules.Products.Contract.ProductDTOs;
using Modules.Products.Contracts.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks; // <-- Bax bu mütləq lazımdır!

namespace Modules.Products.Contracts.Services;

public interface IProductModuleService
{
    Task<List<ResponseProductGet>> Get(int page, int size,string ? search);
    Task<ResponseProductGet> Get(int id);
    Task Post(RequestProductCreate requestProductCreate);
    Task Delete(int id);
    Task Update(int id, RequestUpdateProduct requestProductUpdate);
    Task<Dictionary<int, ResponseProductGet>> GetProductNamesByIdsAsync(List<int> productIds);

    Task<List<ResponseProductGet>> GetProductsByCategory(int categoryId);
}