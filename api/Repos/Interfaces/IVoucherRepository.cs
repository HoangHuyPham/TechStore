using api.Models;

namespace api.Repos.Interfaces
{
    public interface IVoucherRepository : IRepository<Voucher>
    {
        Task<Voucher?> FindByCode(Guid code);
    }
}