using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
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
    public class UsersV2Controller : ControllerBase
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

        [HttpPatch("{id:int}", Name = "PartialUpdate")]
        public ActionResult<UsersDto> PartialUpdate(int id, [FromBody] JsonPatchDocument<UpdateUserDto> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            User existingEntity = userRepo.GetUser(id);

            if (existingEntity == null)
            {
                return NotFound();
            }

            UpdateUserDto userUpdateDto = mapper.Map<UpdateUserDto>(existingEntity);
            patchDoc.ApplyTo(userUpdateDto);

            TryValidateModel(userUpdateDto);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            mapper.Map(userUpdateDto, existingEntity);
            User updated = userRepo.Update(id, existingEntity);

            if (!userRepo.Save())
            {
                throw new Exception("Updating a fooditem failed on save.");
            }

            return Ok(mapper.Map<UsersDto>(updated));
        }
    }
}
