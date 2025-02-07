using System.Security.Claims;
using api.DTOs.CartItem;
using api.Mappers;
using api.Models;
using api.Repos.Interfaces;
using api.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController(IRepository<Cart> cartRepo, IRepository<CartItem> cartItemRepo, IProductRepository productRepo) : ControllerBase
    {
        private readonly IRepository<Cart> _cartRepo = cartRepo;
        private readonly IRepository<CartItem> _cartItemRepo = cartItemRepo;
        private readonly IProductRepository _productRepo = productRepo;

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var cartId = HttpContext.User.FindFirstValue("CartId");
            if (cartId == null) return BadRequest();
            var existCart = await _cartRepo.FindById(Guid.Parse(cartId));

            existCart ??= await _cartRepo.Create(new Cart
            {
                UserId = Guid.Parse(HttpContext.User.FindFirstValue("Id")!)
            });

            return Ok(new APIResponse<Cart>
            {
                Status = 200,
                Data = existCart
            });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddCartItem([FromBody] CartItemCreateDTO createDTO)
        {
            var cartId = HttpContext.User.FindFirstValue("CartId");
            if (cartId == null) return BadRequest();
            var existCart = await _cartRepo.FindById(Guid.Parse(cartId));

            existCart ??= await _cartRepo.Create(new Cart
            {
                UserId = Guid.Parse(HttpContext.User.FindFirstValue("Id")!)
            });


            var existProduct = await _productRepo.FindById(createDTO.ProductId);

            var cartItemSameProduct = existCart!.CartItems?.FirstOrDefault(x => x.ProductId == createDTO.ProductId);
            var cartItemSameProductOption = existCart!.CartItems?.FirstOrDefault(x => x.ProductOptionId == createDTO.ProductOptionId);

            if (createDTO.Anonymous)
            {
                cartItemSameProduct = null;
                cartItemSameProductOption = null;
            }

            // check valid product
            if (existProduct == null)
            {
                return Ok(new APIResponse<string>
                {
                    Status = 10000,
                    Data = "Invalid product."
                });
            }

            // check product quantity
            var stock = existProduct.ProductDetail?.Stock;
            if (stock < createDTO.Quantity)
            {
                return Ok(new APIResponse<string>
                {
                    Status = 10001,
                    Data = "Product is insufficient."
                });
            }

            // check invalid option
            if (existProduct!.ProductDetail!.ProductOptions!.Count <= 0)
            {
                if (createDTO.ProductOptionId != null)
                {
                    return Ok(new APIResponse<string>
                    {
                        Status = 10002,
                        Data = "This product has no option."
                    });
                }
            }
            else
            {
                // check no matching option
                if (!existProduct!.ProductDetail!.ProductOptions!.Any(x => x.Id == createDTO.ProductOptionId))
                {
                    return Ok(new APIResponse<string>
                    {
                        Status = 10003,
                        Data = "No matching option found."
                    });
                }
            }

            // check no duplicate product
            if (cartItemSameProduct == null)
            {
                if (createDTO.Anonymous)
                {
                    return Ok(new APIResponse<CartItem>
                    {
                        Status = 200,
                        Data = await _cartItemRepo.Create(createDTO.ParseToCartItem(Guid.Parse(cartId)))
                    });
                }

                await _cartItemRepo.Create(createDTO.ParseToCartItem(Guid.Parse(cartId)));
                return Ok(new APIResponse<Cart>
                {
                    Status = 200,
                    Data = await _cartRepo.FindById(Guid.Parse(cartId))
                });
            }

            // check no duplicate option
            if (cartItemSameProductOption == null)
            {
                if (createDTO.Anonymous)
                {
                    return Ok(new APIResponse<CartItem>
                    {
                        Status = 200,
                        Data = await _cartItemRepo.Create(createDTO.ParseToCartItem(Guid.Parse(cartId)))
                    });
                }

                await _cartItemRepo.Create(createDTO.ParseToCartItem(Guid.Parse(cartId)));
                return Ok(new APIResponse<Cart>
                {
                    Status = 200,
                    Data = await _cartRepo.FindById(Guid.Parse(cartId))
                });
            }

            // check product quantity when duplicate cart item
            if (stock < createDTO.Quantity + cartItemSameProduct.Quantity)
            {
                return Ok(new APIResponse<string>
                {
                    Status = 10001,
                    Data = "Product is insufficient."
                });
            }

            cartItemSameProduct.Quantity = createDTO.Quantity + cartItemSameProduct.Quantity;
            cartItemSameProduct.IsSelected = true;
            cartItemSameProduct.ProductOptionId = createDTO.ProductOptionId;

            await _cartItemRepo.Update(cartItemSameProduct.Id, cartItemSameProduct);

            return Ok(new APIResponse<Cart>
            {
                Status = 200,
                Data = existCart
            });
        }

        [Authorize]
        [HttpPut("{cartItemId}")]
        public async Task<IActionResult> UpdateCartItem([FromRoute] Guid cartItemId, [FromBody] CartItemUpdateDTO updateDTO)
        {
            var cartId = HttpContext.User.FindFirstValue("CartId");
            if (cartId == null) return BadRequest();
            var existCart = await _cartRepo.FindById(Guid.Parse(cartId));

            existCart ??= await _cartRepo.Create(new Cart
            {
                UserId = Guid.Parse(HttpContext.User.FindFirstValue("Id")!)
            });

            var existCartItem = existCart!.CartItems?.FirstOrDefault(x => x.Id == cartItemId);

            // check invalid cartitem in cart
            if (existCartItem == null)
            {
                return Ok(new APIResponse<string>
                {
                    Status = 10000,
                    Data = "CartItem is unavailable."
                });
            }

            existCartItem.Quantity = updateDTO.Quantity;
            existCartItem.IsSelected = updateDTO.IsSelected;
            existCartItem.ProductOptionId = updateDTO.ProductOptionId;

            await _cartItemRepo.Update(existCartItem.Id, existCartItem);

            return Ok(new APIResponse<Cart>
            {
                Status = 200,
                Data = existCart
            });
        }

        [Authorize]
        [HttpDelete("{cartItemId}")]
        public async Task<IActionResult> Delete([FromRoute] Guid cartItemId)
        {
            var cartId = HttpContext.User.FindFirstValue("CartId");
            if (cartId == null) return BadRequest();
            var existCart = await _cartRepo.FindById(Guid.Parse(cartId));

            await _cartItemRepo.Delete(cartItemId);

            return Ok(new APIResponse<Cart>
            {
                Status = 200,
                Data = existCart
            });
        }
    }
}