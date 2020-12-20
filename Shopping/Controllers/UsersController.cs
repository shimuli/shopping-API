using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shopping.Models;
using Shopping.Models.DTOs;
using Shopping.Repo.IRepo;

namespace Shopping.Controllers
{
    // [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    [Route("api/v{version:apiVersion}/users")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepo userRepo;
        private readonly IMapper mapper;

        public UsersController(IUserRepo _userRepo, IMapper _mapper)
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

        /// <summary>
        /// Get One User 
        /// </summary>
        /// <param name="userId"> The User Id</param>
        /// <returns></returns>
        
        [HttpGet("{userId:int}", Name = "GetUser")]
        [ProducesResponseType(200, Type = typeof(UsersDto))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public IActionResult GetUser(int userId)
        {
            var obj = userRepo.GetUser(userId);
            if (obj == null)
            {
                return NotFound();
            }
            var objDto = mapper.Map<UsersDto>(obj);

            return Ok(objDto);
        }

        /// <summary>
        /// Create a new User
        /// </summary>
        /// <param name="postusersDto"> Quired body</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(UsersDto))]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [ProducesDefaultResponseType]
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
            /*if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }*/
            var userObj = mapper.Map<User>(postusersDto);

            if (!userRepo.CreateUser(userObj))
            {
                ModelState.AddModelError("", $"Something went wrong while saving the record {userObj.Name}");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetUser", new { userId = userObj.UserId }, userObj);
        }

        /// <summary>
        /// Update user info
        /// </summary>
        /// <param name="userId"> User Id</param>
        /// <param name="userDto">Parameter to change</param>
        /// <returns></returns>
        [HttpPatch("{userId:int}", Name = "UpdateUser")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [ProducesDefaultResponseType]
        public IActionResult UpdateUser(int userId, [FromBody] UsersDto userDto)
        {
            if (userDto == null || userId != userDto.UserId)
            {
                return BadRequest(ModelState);
            }
            var userObj = mapper.Map<User>(userDto);
            if (!userRepo.UpdateUser(userObj))
            {
                ModelState.AddModelError("", $"Something went wrong while updating the record {userObj.Name}");
                return StatusCode(500, ModelState);
            }
            return NoContent();

        }

        /// <summary>
        /// Delete user
        /// </summary>
        /// <param name="userId"> User Id</param>
        /// <returns></returns>
        [HttpDelete("{userId:int}", Name = "DeleteUser")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesDefaultResponseType]
        public IActionResult DeleteUser(int userId)
        {
            if (!userRepo.UserExist(userId))
            {
                return NotFound();
            }
            var userObj = userRepo.GetUser(userId);
            if (!userRepo.DeleteUser(userObj))
            {
                ModelState.AddModelError("", $"Something went wrong while deleting the record {userObj.Name}");
                return StatusCode(500, ModelState);
            }
            return NoContent();

        }


    }
}
