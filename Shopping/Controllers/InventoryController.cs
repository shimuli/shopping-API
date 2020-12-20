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
    [Authorize]
    [Route("api/v{version:apiVersion}/inventory")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryRepo inventoryRepo;
        private readonly IMapper mapper;

        public InventoryController(IInventoryRepo _inventoryRepo, IMapper _mapper)
        {
            inventoryRepo = _inventoryRepo;
            mapper = _mapper;
        }
        /// <summary>
        /// Get all Items in the inventory
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<GetInventoryDto>))]
        public IActionResult GetInventories()
        {
            var objList = inventoryRepo.GetInventorys();
            var objDto = new List<GetInventoryDto>();
            foreach (var obj in objList)
            {
                objDto.Add(mapper.Map<GetInventoryDto>(obj));
            }
            return Ok(objDto);
        }

        /// <summary>
        /// Get item by Id
        /// </summary>
        /// <param name="inventoryId"> The item Id</param>
        /// <returns></returns>
        [HttpGet("{inventoryId:int}", Name = "GetInventory")]
        [ProducesResponseType(200, Type = typeof(GetInventoryDto))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public IActionResult GetInventory(int inventoryId)
        {
            var obj = inventoryRepo.GetInventory(inventoryId);
            if (obj == null)
            {
                return NotFound();
            }
            var objDto = mapper.Map<GetInventoryDto>(obj);

            return Ok(objDto);
        }

        /// <summary>
        /// Get Inventory by Id
        /// </summary>
        /// <param name="inventoryId"></param>
        /// <returns></returns>
        [HttpGet("getdirectinventory/{inventoryId:int}", Name = "GetInventoryData")]
        [ProducesResponseType(200, Type = typeof(InventoryDto))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public IActionResult GetInventoryData(int inventoryId)
        {
            var obj = inventoryRepo.GetInventoryData(inventoryId);
            if (obj == null)
            {
                return NotFound();
            }
            var objDto = mapper.Map<InventoryDto>(obj);

            return Ok(objDto);
        }

        /// <summary>
        /// Get Item by Name
        /// </summary>
        /// <param name="name">Item Name</param>
        /// <returns></returns>
        [HttpGet("GetInventoryByName")]
        [ProducesResponseType(200, Type = typeof(InventoryDto))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public IActionResult GetInventoryByName(string name)
        {
            var objList = inventoryRepo.GetInventorybyName(name);
            var objDto = new List<InventoryDto>();
            if (objList == null)
            {
                return NotFound();
            }
            foreach (var obj in objList)
            {
                objDto.Add(mapper.Map<InventoryDto>(obj));
            }
            return Ok(objDto);

        }
        /// <summary>
        /// Get Item by ID and Name
        /// </summary>
        /// <param name="productId"> Item Id</param>
        /// <param name="name">Item Name</param>
        /// <returns></returns>
        [HttpGet("GetInventoryByNameandId")]
        [ProducesResponseType(200, Type = typeof(InventoryDto))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public ActionResult GetInventoryByNameandId(int productId, string name)
        {
            var obj = inventoryRepo.GetInventorybyNameandId(productId, name);

            if (obj == null)
            {
                return NotFound();
            }
            var objDto = mapper.Map<InventoryDto>(obj);

            return Ok(objDto);
        }

        /// <summary>
        /// Filter Items by Date and User Id
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <param name="startdate">Start Date Format- 2020-12-21</param>
        /// <param name="enddate">End Date Format- 2020-12-21</param>
        /// <returns></returns>
        [HttpGet("GetInventoryBydate")]
        [ProducesResponseType(200, Type = typeof(GetInventoryDto))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(200, Type = typeof(List<GetInventoryDto>))]
        public ActionResult GetInventoryBydate(int userId, DateTime? startdate, DateTime enddate)
        {
            var objList = inventoryRepo.GetInventorybyDate(userId, startdate, enddate);
            var objDto = new List<GetInventoryDto>();

            if (objList == null)
            {
                return NotFound();
            }
            foreach (var ob in objList)
            {
                objDto.Add(mapper.Map<GetInventoryDto>(ob));
            }

            return Ok(objDto);
        }


        /// <summary>
        /// Get Data by user Id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("GetUserInventory")]
        [ProducesResponseType(200, Type = typeof(GetInventoryDto))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(200, Type = typeof(List<GetInventoryDto>))]
        public ActionResult GetUserInventory(int userId)
        {
            var objList = inventoryRepo.GetUserInventories(userId);
            var objDto = new List<GetInventoryDto>();

            if (objList == null)
            {
                return NotFound();
            }
            foreach (var ob in objList)
            {
                objDto.Add(mapper.Map<GetInventoryDto>(ob));
            }

            return Ok(objDto);
        }

        /// <summary>
        /// Add an Item
        /// </summary>
        /// <param name="inventoryDto"> Boyd of new Item</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(PostInventoryDto))]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [ProducesDefaultResponseType]
        public IActionResult CreateInventory([FromBody] PostInventoryDto inventoryDto)
        {
            if (inventoryDto == null)
            {
                return BadRequest(ModelState);
            }

            var inventoryObj = mapper.Map<Inventory>(inventoryDto);

            if (!inventoryRepo.CreateInventory(inventoryObj))
            {
                ModelState.AddModelError("", $"Something went wrong while saving the record {inventoryObj.ProductName}");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetInventory", new { inventoryId = inventoryObj.InventoryId }, inventoryObj);

        }
        /// <summary>
        /// Update an item
        /// </summary>
        /// <param name="inventoryId"> Id of item</param>
        /// <param name="inventoryDto">object to update</param>
        /// <returns></returns>
        [HttpPatch("{inventoryId:int}", Name = "UpdateInventory")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [ProducesDefaultResponseType]
        public IActionResult UpdateInventory(int inventoryId, [FromBody] InventoryDto inventoryDto)
        {
            if (inventoryDto == null || inventoryId != inventoryDto.InventoryId)
            {
                return BadRequest(ModelState);
            }
            var inventoryObj = mapper.Map<Inventory>(inventoryDto);
            if (!inventoryRepo.UpdateInventory(inventoryObj))
            {
                ModelState.AddModelError("", $"Something went wrong while updating the record {inventoryObj.ProductName}");
                return StatusCode(500, ModelState);
            }
            return NoContent();

        }

        /// <summary>
        /// Delete an item by name and ID
        /// </summary>
        /// <param name="inventoryId">Id of the item</param>
        /// <param name="name">Name of the Item</param>
        /// <returns></returns>
        [HttpDelete("{inventoryId:int}", Name = "DeleteInventory")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult DeleteInventory(int inventoryId, string name)
        {
            if (!inventoryRepo.InventoryExist(inventoryId))
            {
                return NotFound();
            }
            var inventoryObj = inventoryRepo.GetInventorybyNameandId(inventoryId, name);
            if (!inventoryRepo.DeleteInventory(inventoryObj))
            {
                ModelState.AddModelError("", $"Something went wrong while updating the record {inventoryObj.ProductName}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }



    }
}
