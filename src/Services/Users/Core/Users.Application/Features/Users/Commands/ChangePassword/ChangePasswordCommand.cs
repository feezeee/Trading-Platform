using MediatR;

namespace Users.Application.Features.Users.Commands.ChangePassword
{
    public class ChangePasswordCommand : IRequest
    {
        public string Nickname { get; set; }
        public string NewPassword { get; set; } = string.Empty;
    }
}
