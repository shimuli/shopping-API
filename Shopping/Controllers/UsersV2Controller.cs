using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shopping.Models;
using Shopping.Models.DTOs;
using Shopping.Repo.IRepo;

namespace Shopping.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/v{version:apiVersion}/users")]
    [ApiVersion("2.0")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class UsersV2Controller : Controller
    {
        private readonly IUserRepo userRepo;
        private readonly IMapper mapper;

        public UsersV2Controller(IUserRepo _userRepo, IMapper _mapper)
        {
            userRepo = _userRepo;
            mapper = _mapper;
        }

        /// <summary>
        /// Get List of all users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type=typeof(List<UsersDto>))]
        public IActionResult GetAllUsers()
        {
            var objList = userRepo.GetUsers();
            var objDto = new List<UsersDto>();
            foreach (var obj in objList)
            {
                objDto.Add(mapper.Map<UsersDto>(obj));
            }
            return Ok(objDto);
        }
    }
}
