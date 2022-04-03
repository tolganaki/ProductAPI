using AutoMapper;
using ProductAPI.Data.Entities;
using ProductAPI.Domain.Models;

namespace ProductAPI.Mapping
{
    public class EntitylToModeProfile : Profile
    {
        public EntitylToModeProfile()
        {
            CreateMap<ProductModel, Product>();
            CreateMap<AddProductModel, Product>();
        }
    }
}