using AutoMapper;
using Users.Api.Models.Response.Token;
using Users.Models.Tokens;

namespace Users.Api.Automapper
{
    public class TokenProfile : Profile
    {
        public TokenProfile()
        {
            CreateMap<GetTokenDto, GetTokenResponse>();
        }
    }
}
