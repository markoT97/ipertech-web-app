using AutoMapper;
using IpertechCompany.DbConnection;
using IpertechCompany.DbRepositories;
using IpertechCompany.IRepositories;
using IpertechCompany.IServices;
using IpertechCompany.Models;
using IpertechCompany.Services;
using IpertechCompany.WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace IpertechCompany.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillsController : ControllerBase
    {
        private readonly IBillRepository _billRepository;
        private readonly IBillService _billService;
        private readonly IMapper _mapper;

        public BillsController(IDbContext dbContext, IBillRepository billRepository, IBillService billService, IMapper mapper)
        {
            _billRepository = new BillRepository(dbContext);
            _billService = new BillService(_billRepository);
            _mapper = mapper;
        }

        [Authorize(Roles = "User")]
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetBillsByUserContractId(Guid id)
        {
            return Ok(_billService.GetByUserContractId(id).Select(bill => _mapper.Map<BillViewModel>(bill)));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult InsertBill(BillViewModel bill)
        {
            BillViewModel insertedBill = _mapper.Map<BillViewModel>(_billService.CreateBill(_mapper.Map<Bill>(bill)));

            return CreatedAtAction(nameof(GetBillsByUserContractId), new { id = insertedBill.UserContract.UserContractId }, insertedBill);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult UpdateBill(BillViewModel bill)
        {
            _billService.UpdateBill(_mapper.Map<Bill>(bill));

            return Accepted(bill);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteBill(Guid id)
        {
            if (!_billService.DeleteBill(id))
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
