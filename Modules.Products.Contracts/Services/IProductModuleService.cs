using Modules.Products.Contract.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks; // <-- Bax bu mütləq lazımdır!

namespace Modules.Products.Contracts.Services;

public interface IProductModuleService
{
    Task<List<ResponseProductGet>> Get(int page, int size);
    Task<ResponseProductGet> Get(int id);
    Task Post(RequestProductCreate requestProductCreate);
    Task Delete(int id);
    Task Update(int id, RequestProductCreate requestProductUpdate);
    Task<Dictionary<int, ResponseProductGet>> GetProductNamesByIdsAsync(List<int> productIds);
}