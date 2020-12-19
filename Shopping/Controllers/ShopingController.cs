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
    //[Route("api/[controller]")]
    [Authorize]
    [Route("api/v{version:apiVersion}/shopping")]
    [ApiController]

    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class ShopingController : ControllerBase
    {
        private readonly IShoppingRepo shoppingRepo;
        private readonly IMapper mapper;

        public ShopingController(IShoppingRepo _shoppingRepo, IMapper _mapper)
        {
            shoppingRepo = _shoppingRepo;
            mapper = _mapper;
        }

        /// <summary>
        /// Get All shoping data in a list
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<GetShopingDto>))]
        public IActionResult GetShopings()
        {
            var objList = shoppingRepo.GetShopings();
            var objDto = new List<GetShopingDto>();
            foreach (var obj in objList)
            {
                objDto.Add(mapper.Map<GetShopingDto>(obj));
            }
            return Ok(objDto);
        }

        /// <summary>
        /// Get item based on item Id
        /// </summary>
        /// <param name="itemId"> item Id</param>
        /// <returns></returns>
        [HttpGet("{itemId:int}", Name = "GetShoping")]
        [ProducesResponseType(200, Type = typeof(GetShopingDto))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public IActionResult GetShoping(int itemId)
        {
            var obj = shoppingRepo.GetShopping(itemId);
            if (obj == null)
            {
                return NotFound();
            }
            var objDto = mapper.Map<GetShopingDto>(obj);

            return Ok(objDto);
        }

        /// <summary>
        /// Get Item by name
        /// </summary>
        /// <param name="item"> Item Name</param>
        /// <returns></returns>
        [HttpGet("GetShopingByName")]
        [ProducesResponseType(200, Type = typeof(ShopingDto))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public IActionResult GetShopingByName(string item)
        {
            var objList = shoppingRepo.GetShopingbyName(item);
            var objDto = new List<ShopingDto>();
            if (objList == null)
            {
                return NotFound();
            }
            foreach (var obj in objList)
            {
                objDto.Add(mapper.Map<ShopingDto>(obj));
            }
            return Ok(objDto);

        }

        /// <summary>
        /// Get item by Id and Name
        /// </summary>
        /// <param name="itemId"> Id of the item</param>
        /// <param name="name">Name of the item</param>
        /// <returns></returns>
        [HttpGet("GetShopingbyNameandId")]
        [ProducesResponseType(200, Type = typeof(ShopingDto))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public ActionResult GetShopingbyNameandId(int itemId, string name)
        {
            var obj = shoppingRepo.GetShopingbyNameandId(itemId, name);

            if (obj == null)
            {
                return NotFound();
            }
            var objDto = mapper.Map<ShopingDto>(obj);

            return Ok(objDto);
        }

        /// <summary>
        /// Filter Data by Date and Id
        /// </summary>
        /// <param name="userId">Id of User</param>
        /// <param name="startdate">Start date Format: 2020-12-21</param>
        /// <param name="enddate">End date Format: 2020-12-21</param>
        /// <returns></returns>
        [HttpGet("GetShopingbyDate")]
        [ProducesResponseType(200, Type = typeof(GetShopingDto))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(200, Type = typeof(List<GetShopingDto>))]
        public ActionResult GetShopingbyDate(int userId, DateTime? startdate, DateTime enddate)
        {
            var objList = shoppingRepo.GetShopingbyDate(userId, startdate, enddate);
            var objDto = new List<GetShopingDto>();

            if (objList == null)
            {
                return NotFound();
            }
            foreach (var ob in objList)
            {
                objDto.Add(mapper.Map<GetShopingDto>(ob));
            }

            return Ok(objDto);
        }

        /// <summary>
        /// Create new Item
        /// </summary>
        /// <param name="postShopingDto"> Dta body</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(PostInventoryDto))]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [ProducesDefaultResponseType]
        public IActionResult CreateInventory([FromBody] PostShopingDto postShopingDto)
        {
            if (postShopingDto == null)
            {
                return BadRequest(ModelState);
            }

            var shopingObj = mapper.Map<Shoping>(postShopingDto);

            if (!shoppingRepo.CreateShoping(shopingObj))
            {
                ModelState.AddModelError("", $"Something went wrong while saving the record {shopingObj.ItemName}");
                return StatusCode(500, ModelState);
            }
            return Ok();
           /* return CreatedAtRoute("GetShoping", new { inventoryId = shopingObj.ItemId }, shopingObj);*/

        }

        /// <summary>
        /// Update An Item
        /// </summary>
        /// <param name="itemId">Item Id</param>
        /// <param name="shopingDto">Body to Update</param>
        /// <returns></returns>
        [HttpPatch("{itemId:int}", Name = "UpdateItem")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [ProducesDefaultResponseType]
        public IActionResult UpdateItem(int itemId, [FromBody] ShopingDto shopingDto)
        {
            if (shopingDto == null || itemId != shopingDto.ItemId)
            {
                return BadRequest(ModelState);
            }
            var shopingObj = mapper.Map<Shoping>(shopingDto);
            if (!shoppingRepo.UpdateShoping(shopingObj))
            {
                ModelState.AddModelError("", $"Something went wrong while updating the record {shopingObj.ItemName}");
                return StatusCode(500, ModelState);
            }
            return NoContent();

        }

        /// <summary>
        /// Delete Item from Shoping List
        /// </summary>
        /// <param name="itemId">Item Id</param>
        /// <param name="name">Item Name</param>
        /// <returns></returns>
        [HttpDelete("{itemId:int}", Name = "DeleteItem")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult DeleteItem(int itemId, string name)
        {
            if (!shoppingRepo.ShopingExist(itemId))
            {
                return NotFound();
            }
            var itemObj = shoppingRepo.GetShopingbyNameandId(itemId, name);
            if (!shoppingRepo.DeleteShoping(itemObj))
            {
                ModelState.AddModelError("", $"Something went wrong while updating the record {itemObj.ItemName}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

    }
}
