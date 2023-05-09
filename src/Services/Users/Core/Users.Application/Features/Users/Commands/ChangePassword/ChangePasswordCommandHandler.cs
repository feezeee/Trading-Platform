using MediatR;
using Users.Domain.Contracts;
using Users.Domain.Contracts.Finders;
using Users.Domain.Contracts.Repositories;
using Users.Models.Exceptions;

namespace Users.Application.Features.Users.Commands.ChangePassword
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserFinder _userFinder;
        private readonly IUnitOfWork _unitOfWork;

        public ChangePasswordCommandHandler(IUserRepository userRepository, IUserFinder userFinder, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _userFinder = userFinder;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userFinder.GetByNicknameAsync(request.Nickname, cancellationToken);
            if (user is null)
            {
                throw new EntityNotFoundException("User not found");
            }

            user.Password = request.NewPassword;
            _userRepository.Update(user);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
