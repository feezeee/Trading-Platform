using MediatR;
using Users.Domain.Contracts;
using Users.Domain.Contracts.Finders;
using Users.Domain.Contracts.Repositories;
using Users.Models.Exceptions;

namespace Users.Application.Features.Users.Commands.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly IUserFinder _userFinder;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public DeleteUserCommandHandler(IUserFinder userFinder, IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userFinder = userFinder;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        /// <summary>Handles a request</summary>
        /// <param name="request">The request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Response from the request</returns>
        public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var userEntity = await _userFinder.GetByIdAsync(request.Id, cancellationToken);
            if (userEntity is null)
            {
                throw new EntityNotFoundException($"User with id - {request.Id} doesn't exist");
            }

            _userRepository.Delete(userEntity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
