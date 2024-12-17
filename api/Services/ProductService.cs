using api.Datas;
using api.DTOs.Product;
using api.Mappers;
using api.Models;
using api.Repos.Interfaces;
using api.Services.Interfaces;

namespace api.Services
{
    public class ProductService (ApplicationContext appContext, IRepository<Product> repoProduct, IRepository<Category> repoCategory) : IProductService
    {
        private readonly IRepository<Product> _repoProduct = repoProduct;
        private readonly IRepository<Category> _repoCategory = repoCategory;
        private readonly ApplicationContext _appContext = appContext;
        public async Task<Product?> Update(ProductUpdateDTO updateDTO){
            var transation = await _appContext.Database.BeginTransactionAsync();
            var category = await _repoCategory.FindById(updateDTO.CategoryId);
            if (category == null) return null;
            var existProduct = await _repoProduct.FindById(updateDTO.Id);
            if (existProduct == null) return null;
        
            existProduct.Name = updateDTO.Name;
            existProduct.Thumbnail = updateDTO.Thumbnail;
            existProduct.ProductDetail = updateDTO.ProductDetail.ParseToProductDetail();
            existProduct.Category = category;
            
            await _repoProduct.Update(updateDTO.Id, existProduct);
            await transation.CommitAsync();

            return await _repoProduct.FindById(updateDTO.Id);
        }

        public async Task<Product?> Create(ProductCreateDTO createDTO){
            var transation = await _appContext.Database.BeginTransactionAsync();
            var category = await _repoCategory.FindById(createDTO.CategoryId);
            if (category == null) return null;
     
            var product = new Product{
                Name = createDTO.Name,
                Thumbnail = createDTO.Thumbnail,
                ProductDetail = createDTO.ProductDetail.ParseToProductDetail(),
                Category = category
            };
            
            var result = await _repoProduct.Create(product);
            await transation.CommitAsync();

            return result;
        }
    }
}