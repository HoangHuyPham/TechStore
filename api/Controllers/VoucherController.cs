using api.DTOs.Product;
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
    public class VoucherController(IVoucherRepository VoucherRepo) : ControllerBase
    {
        private readonly IVoucherRepository _VoucherRepo = VoucherRepo;
        [Authorize(Roles = "c26b7fcb-9e16-47aa-893e-3ef148de9714")] // Require admin
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var vouchers = await _VoucherRepo.FindAll();
            return Ok(new APIResponse<IEnumerable<Voucher>>
            {
                Status = 200,
                Data = vouchers
            });
        }

        [HttpGet("CheckVoucher/{code}")]
        public async Task<IActionResult> CheckVoucher([FromRoute] Guid code)
        {
            var Voucher = await _VoucherRepo.FindByCode(code);
            if (Voucher == null) return Ok(new APIResponse<string>
            {
                Status = 10000,
                Data = "No voucher found."
            });

            return Ok(new APIResponse<Voucher>
            {
                Status = 200,
                Data = Voucher
            });
        }

        [Authorize(Roles = "c26b7fcb-9e16-47aa-893e-3ef148de9714")] // Require admin
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var Voucher = await _VoucherRepo.FindById(id);
            if (Voucher == null) return NotFound();
            return Ok(new APIResponse<Voucher>
            {
                Status = 200,
                Data = Voucher
            });
        }

        [Authorize(Roles = "c26b7fcb-9e16-47aa-893e-3ef148de9714")] // Require admin
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] VoucherCreateDTO createDTO)
        {
            var Voucher = await _VoucherRepo.Create(new Models.Voucher
            {
                Code = createDTO.Code,
                Factor = createDTO.Factor,
                IsActive = createDTO.IsActive,
                ExpiredAt = createDTO.ExpiredAt,
                Name = createDTO.Name,
            });
            if (Voucher == null) return Ok(new APIResponse<string>
            {
                Status = 10000,
                Data = "Add voucher failed."
            });
            return Ok(new APIResponse<Voucher>
            {
                Status = 200,
                Data = Voucher
            });
        }

        [Authorize(Roles = "c26b7fcb-9e16-47aa-893e-3ef148de9714")] // Require admin
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Voucher updateDTO)
        {
            var Voucher = await _VoucherRepo.Update(updateDTO.Id, updateDTO);
            if (Voucher == null) return NotFound();
            return Ok(Voucher);
        }

        [Authorize(Roles = "c26b7fcb-9e16-47aa-893e-3ef148de9714")] // Require admin
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var Voucher = await _VoucherRepo.Delete(id);
            if (Voucher == null) return Ok(new APIResponse<string>
            {
                Status = 10000,
                Data = "Not found Voucher."
            });

            return Ok(new APIResponse<string>
            {
                Status = 200,
                Data = "Removed Voucher."
            });
        }
    }
}