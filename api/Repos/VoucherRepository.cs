using api.Datas;
using api.Models;
using api.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Repos
{
    public class VoucherRepository(ApplicationContext context) : IVoucherRepository
    {
        private readonly ApplicationContext _context = context;
        public async Task<Voucher?> Create(Voucher target)
        {
            await _context.Vouchers.AddAsync(target);
            await _context.SaveChangesAsync();
            return target;
        }

        public async Task<Voucher?> Delete(Guid id)
        {
            var existVoucher = await _context.Vouchers.FirstOrDefaultAsync(x => x.Id == id);
            if (existVoucher == null) return null;
            _context.Vouchers.Remove(existVoucher);
            await _context.SaveChangesAsync();
            return existVoucher;
        }

        public async Task<List<Voucher>> FindAll()
        {
            return await _context.Vouchers.ToListAsync();
        }

        public async Task<Voucher?> FindByCode(Guid code)
        {
            var existVoucher = await _context.Vouchers.FirstOrDefaultAsync(x => x.Code == code);
            if (existVoucher == null) return null;
            return existVoucher;
        }

        public async Task<Voucher?> FindById(Guid id)
        {
            var existVoucher = await _context.Vouchers.FirstOrDefaultAsync(x => x.Id == id);
            if (existVoucher == null) return null;
            return existVoucher;
        }

        public async Task<Voucher?> Update(Guid id, Voucher data)
        {
            _context.Vouchers.Update(data);
            await _context.SaveChangesAsync();
            return data;
        }
    }
}