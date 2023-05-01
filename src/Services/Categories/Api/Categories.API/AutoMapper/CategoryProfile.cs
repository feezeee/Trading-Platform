using AutoMapper;
using Categories.API.Models.Category;
using Categories.BLL.Entities;

namespace Categories.API.AutoMapper
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CategoryEntity, GetCategoryResponse>();
            CreateMap<CreateCategoryRequest, CategoryEntity>();
            CreateMap<UpdateCategoryRequest, CategoryEntity>();
        }
    }
}
