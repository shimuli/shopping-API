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
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private IUserRepo userRepo;
        private readonly IMapper mapper;

        public UsersController(IUserRepo _userRepo, IMapper _mapper)
        {
            userRepo = _userRepo;
            mapper = _mapper;
        }
        [HttpGet]
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

        [HttpGet("{userId:int}")]
        public IActionResult GetInventory(int userId)
        {
            var obj = userRepo.GetUser(userId);
            if (obj == null)
            {
                return NotFound();
            }
            var objDto = mapper.Map<UsersDto>(obj);

            return Ok(objDto);
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] PostUserDto postusersDto)
        {
            if (postusersDto == null)
            {
                return BadRequest(ModelState);
            }
            if (userRepo.UserExist(postusersDto.Name))
            {
                ModelState.AddModelError("", "User Exits!");
                return StatusCode(404, ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userObj = mapper.Map<User>(postusersDto);

            if (!userRepo.CreateUser(userObj))
            {
                ModelState.AddModelError("", $"Something went wrong while saving the reord{userObj.Name}");
                return StatusCode(500, ModelState);
            }
            return Ok();
        }


    }
}
