using AutoMapper;
using Products.Api.Models.Products.Request;
using Products.Api.Models.Products.Response;

namespace Categories.API.AutoMapper
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<GetProductResponse, PutProductRequest>().ReverseMap();
        }
    }
}
