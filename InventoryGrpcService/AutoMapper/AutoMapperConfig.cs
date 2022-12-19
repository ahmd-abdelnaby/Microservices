using AutoMapper;

namespace InventoryGrpcService.AutoMapper
{
    internal class AutoMapperConfig : Profile
    {

        public AutoMapperConfig()
        {
            CreateMap<InventoryGrpcService.ProductModel, InventoryAppliction.Models.ProductModel>();         }
    }
}
