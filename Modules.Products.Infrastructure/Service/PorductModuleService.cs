using Microsoft.EntityFrameworkCore;
using Modules.Categories.Contract.Services;
using Modules.Products.Contract.ProductDTOs;
using Modules.Products.Contracts.Services;
using Modules.Products.Domain;
using Modules.Products.Infrastructure.Persistence;
using MonolitModularLearning.Common.Extentions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modules.Products.Infrastructure.Service
{
    public class PrductModuleService : IProductModuleService
    {
        private readonly ProductsDbContext _context;
        private readonly ICategoryModuleService _categoryModuleService;
        public PrductModuleService(ProductsDbContext context, ICategoryModuleService categoryModuleService)
        {
            _context = context;
            _categoryModuleService = categoryModuleService;
        }

        public async Task Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                throw new Exception("Product not found");
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            
        }

        public  async Task<List<ResponseProductGet>> Get(int page, int size)
        {
            var products = await _context.Products
            .AsNoTracking()
            .Include(p => p.ProductDescription)
            .OrderBy(p => p.Id) // Səhifələmədə mütləq sıralama lazımdır
            .ToPaged(page, size) // Sənin yazdığın o gözəl Extension!
            .Select(p => new ResponseProductGet
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                CategoryId = p.CategoryId,
                Description = p.ProductDescription != null ? p.ProductDescription.Description : string.Empty,
                CategroyName = string.Empty // Hələlik boş qoyuruq
            })
            .ToListAsync();

            if (!products.Any()) throw new Exception("Məhsullar tapılmadı");

            // 2. RAM-dakı 10 məhsulun sadəcə CategoryId-lərini alırıq (Distinct = təkrarlananları atır)
            var categoryIds = products.Select(p => p.CategoryId).Distinct().ToList();

            // 3. Sənin yazdığın və bazanı yormayan o mükəmməl Dictionary metodu!
            var categoryNames = await _categoryModuleService.GetCategoryNamesAsync(categoryIds);

            // 4. Məlumatları RAM-da birləşdiririk (In-Memory Join)
            foreach (var product in products)
            {
                // Dictionary-də bu ID varsa adını götür, yoxdursa "Tapılmadı" yaz
                if (categoryNames.TryGetValue(product.CategoryId, out string catName))
                {
                    product.CategroyName = catName;
                }
                else
                {
                    product.CategroyName = "Tapılmadı";
                }
            }
            return products;

        }

        public async Task<ResponseProductGet> Get(int id)
        {
            var product = await _context.Products.Select(p => new ResponseProductGet
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                CategoryId = p.CategoryId,

                Description = p.ProductDescription != null ? p.ProductDescription.Description : string.Empty,
                CategroyName = string.Empty
            }).AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);

            if (product is null) throw new Exception("Məhsul tapılmadı");

            var categoryName = await _categoryModuleService.GetCategoryNameAsync(product.CategoryId);

            product.CategroyName = categoryName;
            return product;
        }

        public async Task<Dictionary<int, ResponseProductGet>> GetProductNamesByIdsAsync(List<int> productIds)
        {
            



            var products= await _context.Products
                .Where(p => productIds.Contains(p.Id))
                .Select(p => new ResponseProductGet
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price, 
                    CategoryId = p.CategoryId

                })
                .ToDictionaryAsync(p => p.Id, p => p);
            return products;

        }

        public async Task Post(RequestProductCreate productdto)
        {
            var product = new Product
            {
                Name = productdto.Name,
                Price = productdto.Price,
                CategoryId = productdto.CategoryId,
                ProductDescription = new ProductDescription
                {
                    Description = productdto.Description
                }
            };
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

       

        public async Task Update(int id, RequestProductCreate productdto)
        {
            var product = await _context.Products.Include(p => p.ProductDescription).FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                throw new Exception("Məhsul tapılmadı");    
            }
            product.Name = productdto.Name;
            product.Price = productdto.Price;
            product.CategoryId = productdto.CategoryId;
            if (product.ProductDescription == null)
            {
                product.ProductDescription = new ProductDescription
                {
                    Description = productdto.Description
                };
            }
            else
            {
                product.ProductDescription.Description = productdto.Description;
            }
            await _context.SaveChangesAsync();
        }

       
    }
}
