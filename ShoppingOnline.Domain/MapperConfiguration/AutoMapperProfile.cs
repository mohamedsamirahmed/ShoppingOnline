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
            CreateMap<Order, OrderDTO>().ForPath(x => x.User.Token, x => x.Ignore());
            CreateMap<ProductCategory, ProductCategoryDTO>();
            CreateMap<Photos, PhotoDTO>();
            CreateMap<Category, LookupDTO>();
            CreateMap<OrderStatus, LookupDTO>().ForMember(dest=>dest.text,
                opt=>opt.MapFrom(src=>src.Name))
                .ForMember(dest=>dest.value,
                opt=>opt.MapFrom(src=>src.Id));

            CreateMap<User, RegisterDTO>();
            CreateMap<User, LoginDto>();
            CreateMap<CartItem, CartItemDTO>();
            CreateMap<CartItemDTO, CartItem>().ForMember("Product",opt=>opt.Ignore())
                .ForMember("Cart",opt=>opt.Ignore());
            CreateMap<ProductDTO, CartItem>().ForMember(dest=>dest.ProductId,
                opt=>opt.MapFrom(src=>src.id)).ForMember("Id",opt=>opt.Ignore());
            CreateMap<Cart, CartDTO>();
            CreateMap<User, UserDTO>();
            CreateMap<ProductDTO, Product>();
            CreateMap<CartDTO, Cart>();
            CreateMap<CartItem, OrderItems>()
                .ForMember("Id", opt => opt.Ignore());
            CreateMap<OrderItems, OrderItemDTO>();
            CreateMap<RegisterDTO, User>();
                

        }
        
    }
}
