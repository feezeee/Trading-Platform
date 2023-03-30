using AutoMapper;
using Users.Domain.Entities;
using Users.Models.RefreshTokens;

namespace Users.Application.Automapper
{
    public class TokenProfile : Profile
    {
        public TokenProfile()
        {
            CreateMap<RefreshTokenEntity, GetRefreshTokenDto>();
        }
    }
}
