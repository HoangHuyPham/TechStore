using System.Security.Claims;
using api.DTOs.Order;
using api.Models;
using api.Repos.Interfaces;
using api.Responses;
using api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class OrderController(IOrderRepository orderRepo, IUserRepository repoUser, IOrderService orderService) : ControllerBase
    {
        private readonly IOrderRepository _orderRepo = orderRepo;
        private readonly IUserRepository _repoUser = repoUser;
        private readonly IOrderService _orderService = orderService;
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _orderRepo.FindAll();
            return Ok(new APIResponse<IEnumerable<Order>>
            {
                Status = 200,
                Data = categories
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var existOrder = await _orderRepo.FindById(id);
            if (existOrder == null) return Ok(new APIResponse<string>
            {
                Status = 10000,
                Data = "No order found."
            });
            return Ok(new APIResponse<Order>
            {
                Status = 200,
                Data = existOrder
            });
        }

        [HttpGet("ByBuyer/{id}")]
        public async Task<IActionResult> GetByBuyer([FromRoute] Guid id)
        {
            var existOrders = await _orderRepo.FindByBuyerId(id);
            if (existOrders == null) return Ok(new APIResponse<string>
            {
                Status = 10000,
                Data = "No order found."
            });
            return Ok(new APIResponse<ICollection<Order>>
            {
                Status = 200,
                Data = existOrders
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OrderCreateDTO createDTO)
        {
            var userId = HttpContext.User.FindFirstValue("Id");
            var existUser = await _repoUser.FindById(Guid.Parse(userId!));

            if (userId == null || existUser == null)
            {
                return Ok(new APIResponse<string>
                {
                    Status = 10000,
                    Data = "Invalid user."
                });
            }

            if (existUser.Phone == null || existUser.Address == null){
                return Ok(new APIResponse<string>
                {
                    Status = 10001,
                    Data = "Address and phone must be filled on your profile."
                });
            }

            var newOrder = await _orderService.Create(existUser, createDTO);

            return Ok(new APIResponse<Order>
            {
                Status = 200,
                Data = newOrder
            });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateStatus([FromBody] OrderUpdateDTO updateDTO)
        {
            var existOrder = await _orderRepo.FindById(updateDTO.Id);
            if (existOrder == null) return Ok(new APIResponse<string>
            {
                Status = 10000,
                Data = "No order found."
            });

            existOrder.OrderTypeId = updateDTO.OrderTypeId;

            var result = await _orderRepo.Update(updateDTO.Id, existOrder);
            if (result == null) return Ok(new APIResponse<string>
            {
                Status = 10001,
                Data = "Update failed."
            });

            return Ok(new APIResponse<Order>
            {
                Status = 200,
                Data = existOrder
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var Order = await _orderRepo.Delete(id);
            if (Order == null) return Ok(new APIResponse<string>
            {
                Status = 10000,
                Data = "Not found order."
            });
        
            return Ok(new APIResponse<string>
            {
                Status = 200,
                Data = "Removed order."
            });
        
        }
    }
}