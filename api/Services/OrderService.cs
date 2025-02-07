using api.Datas;
using api.DTOs.Order;
using api.Models;
using api.Repos.Interfaces;
using api.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;

namespace api.Services
{
    public class OrderService(ApplicationContext appContext, IVoucherRepository voucherRepo, IOrderRepository orderRepo, IRepository<CartItem> cartItemRepo) : IOrderService
    {
        private readonly ApplicationContext _appContext = appContext;
        private readonly IOrderRepository _orderRepo = orderRepo;
        private readonly IVoucherRepository _voucherRepo = voucherRepo;
        private readonly IRepository<CartItem> _cartItemRepo = cartItemRepo;

        public async Task<Order?> Create(User user, OrderCreateDTO createDTO)
        {
            using var transaction = await _appContext.Database.BeginTransactionAsync();
            try
            {
                var newOrder = await _orderRepo.Create(new Order
                {
                    OrderTypeId = createDTO.OrderTypeId,
                    BuyerId = user.Id,
                    Description = createDTO.Description,
                    VoucherId = createDTO.VoucherId
                });
                if (createDTO.VoucherId != null)
                {
                    var voucher = await _voucherRepo.FindById((Guid)createDTO.VoucherId);
                    if ((bool)(voucher?.IsActive))
                    {
                        throw new Exception();
                    }
                    else
                    {

                        voucher.IsActive = true;
                        await _voucherRepo.Update(voucher.Id, voucher);
                    }
                }

                var itemCartIds = createDTO.Items;

                foreach (var itemId in itemCartIds)
                {
                    var cartItem = await _cartItemRepo.FindById(itemId);
                    if (cartItem == null || newOrder == null)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        cartItem.OrderId = newOrder.Id;
                        var updateItem = await _cartItemRepo.Update(cartItem.Id, cartItem);
                        if (updateItem == null)
                            throw new Exception();
                    }

                }
                await transaction.CommitAsync();
                return newOrder;
            }
            catch
            {
                await transaction.RollbackAsync();
                return null;
            }
        }

        public Task<Order?> Update(OrderUpdateDTO updateDTO)
        {
            throw new NotImplementedException();
        }
    }
}