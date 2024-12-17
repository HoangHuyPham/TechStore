using api.DTOs.Product;
using api.Models;

namespace api.Services.Interfaces
{
    public interface IProductService
    {
        Task<Product?> Update(ProductUpdateDTO updateDTO);
        Task<Product?> Create(ProductCreateDTO createDTO);
    }
}