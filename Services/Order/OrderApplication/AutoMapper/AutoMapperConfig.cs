using AutoMapper;
using OrderApplication.DTO;
using OrderApplication.Enums;
using OrderApplication.Models;
using SharedMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderApplication.AutoMapper
{
    internal class AutoMapperConfig : Profile
    {

        public AutoMapperConfig()
        {
            CreateMap<OrderDto, Order>() //source ,Dest
           .ForMember(orderDto => orderDto.OrderDate, opt => opt.MapFrom(src => DateTime.Now))
           .ForMember(orderDto => orderDto.Status, opt => opt.MapFrom(src => OrderStatus.NotPaid));

            CreateMap<OrderDetailsDto, OrderDetails>();


            CreateMap<OrderDetailsDto, ProductQuantities>();
        }
    }
}
