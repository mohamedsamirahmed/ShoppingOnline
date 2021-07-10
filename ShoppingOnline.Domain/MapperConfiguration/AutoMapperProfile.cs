using AutoMapper;
using ShoppingOnline.Domain.Model;
using ShoppingOnline.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingOnline.Domain.MapperConfiguration
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Product, ProductDTO>().ForMember(dest=>dest.PhotoUrl,
                opt=>opt.MapFrom(src=> src.Photos.FirstOrDefault(x=>x.IsMain).Url));
            CreateMap<Order, OrderDTO>();
            CreateMap<ProductCategory, ProductCategoryDTO>();
            CreateMap<Photos, PhotoDTO>();
            CreateMap<Category, LookupDTO>();
            CreateMap<OrderStatus, LookupDTO>();
            CreateMap<User, RegisterDTO>();
            CreateMap<User, LoginDto>();
        }
        
    }
}
