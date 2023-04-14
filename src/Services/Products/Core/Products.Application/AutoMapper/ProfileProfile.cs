using AutoMapper;
using Products.Domain.Entities;
using Products.Models.Products;

namespace Products.Application.AutoMapper
{
    public class ProfileProfile : Profile
    {
        public ProfileProfile()
        {
            CreateMap<ProductEntity, GetProductDto>();
        }
    }
}
