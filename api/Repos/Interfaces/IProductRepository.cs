using api.DTOs;
using api.Models;
using api.Query;

namespace api.Repos.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<PaginationResponseDTO<Product>> FindAll(ProductQuery query);
    }

}