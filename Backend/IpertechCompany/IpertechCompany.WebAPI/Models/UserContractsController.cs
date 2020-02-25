using AutoMapper;
using IpertechCompany.DbConnection;
using IpertechCompany.DbRepositories;
using IpertechCompany.IRepositories;
using IpertechCompany.IServices;
using IpertechCompany.Models;
using IpertechCompany.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace IpertechCompany.WebAPI.Models
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserContractsController : ControllerBase
    {
        private readonly IUserContractRepository _userContractRepository;
        private readonly IUserContractService _userContractService;
        private readonly IMapper _mapper;

        public UserContractsController(IDbContext dbContext, IUserContractRepository userContractRepository, IUserContractService userContractService, IMapper mapper)
        {
            _userContractRepository = new UserContractRepository(dbContext);
            _userContractService = new UserContractService(_userContractRepository);
            _mapper = mapper;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult GetAllUserContracts()
        {
            return Ok(_userContractService.GetAllUserContracts().Select(userContract => _mapper.Map<UserContractViewModel>(userContract)));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult InsertUserContract(UserContractViewModel userContract)
        {
            UserContractViewModel insertedUserContract = _mapper.Map<UserContractViewModel>(_userContractService.CreateUserContract(_mapper.Map<UserContract>(userContract)));

            return CreatedAtAction(nameof(GetAllUserContracts), new { id = insertedUserContract.UserContractId }, insertedUserContract);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult UpdateUserContract(UserContractViewModel userContract)
        {
            _userContractService.UpdateUserContract(_mapper.Map<UserContract>(userContract));

            return Accepted(userContract);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteUserContract(Guid id)
        {
            if (!_userContractService.DeleteUserContract(id))
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
