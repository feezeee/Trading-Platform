using AutoMapper;
using Products.Api.Models.Products.Request;
using Products.Api.Models.Products.Response;
using Products.Models.Products;

namespace Products.Api.AutoMapper
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<GetProductDto, GetProductResponse>();
            CreateMap<PostProductRequest, CreateProductDto>();
        }
    }
}
