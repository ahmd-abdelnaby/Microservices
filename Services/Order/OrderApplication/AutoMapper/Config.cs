using AutoMapper;
using OrderApplication.DTO;
using OrderApplication.Enums;
using OrderApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderApplication.AutoMapper
{
    internal class Config: Profile
    {

        public Config()
        {
            CreateMap<OrderDto, Order>(); //source ,Dest
              // .ForMember(x => x.Status, opt => opt.MapFrom(OrderStatus.NotPaid));


            CreateMap<OrderDetailsDto, OrderDetails>();
        }
    }
}
