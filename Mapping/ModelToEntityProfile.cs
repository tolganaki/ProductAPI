using AutoMapper;
using ProductAPI.Data.Entities;
using ProductAPI.Domain.Enum;
using ProductAPI.Domain.Models;
using ProductAPI.Extrensions;

namespace ProductAPI.Mapping
{
    public class ModelToEntityProfile : Profile
    {
        public ModelToEntityProfile()
        {
            CreateMap<Product, ProductModel>()
                .ForMember(dest => dest.TypeName, opt =>
                {
                    opt.MapFrom(src => EnumExtensions.ToDescription((ProductType)src.TypeId));
                });
        }
    }
}