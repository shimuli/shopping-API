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
    public class InventoryController : Controller
    {
        private IInventoryRepo inventoryRepo;
        private readonly IMapper mapper;

        public InventoryController(IInventoryRepo _inventoryRepo, IMapper _mapper)
        {
            inventoryRepo = _inventoryRepo;
            mapper = _mapper;
        }
        [HttpGet]
        public IActionResult GetInventories()
        {
            var objList = inventoryRepo.GetInventorys();
            var objDto = new List<GetInventoryDto>();
            foreach(var obj in objList)
            {
                objDto.Add(mapper.Map<GetInventoryDto>(obj));
            }
            return Ok(objDto);
        }

        [HttpGet("{inventoryId:int}")]
        public IActionResult GetInventory(int inventoryId)
        {
            var obj = inventoryRepo.GetInventory(inventoryId);
            if(obj == null)
            {
                return NotFound();
            }
            var objDto = mapper.Map<GetInventoryDto>(obj);
           
            return Ok(objDto);
        }

        [HttpPost]
        public IActionResult CreateInventory([FromBody] InventoryDto inventoryDto)
        {
            if(inventoryDto == null)
            {
                return BadRequest(ModelState);
            }
            if (inventoryRepo.InventoryExist(inventoryDto.ProductName))
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var inventoryObj = mapper.Map<Inventory>(inventoryDto);

            if (!inventoryRepo.CreateInventory(inventoryObj))
            {
                ModelState.AddModelError("", $"Sometging went wrong while saving the reord{inventoryObj.ProductName}");
                return StatusCode(500, ModelState);
            }
            return Ok();



        }
    }
}
