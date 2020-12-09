using AutoMapper;
using Shopping.Models;
using Shopping.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopping.Mapper
{
    public class ShoppingMappings : Profile
    {
        public ShoppingMappings()
        {
            CreateMap<Inventory, InventoryDto>().ReverseMap();
            CreateMap<User, UsersDto>().ReverseMap();
            CreateMap<User, PostUserDto>().ReverseMap();
            CreateMap<Inventory, PostInventoryDto>().ReverseMap();
            CreateMap<InventoryList, GetInventoryDto>().ReverseMap();
        }
    }
}
