using MediatR;
using Users.Domain.Contracts;
using Users.Domain.Contracts.Finders;
using Users.Domain.Contracts.Repositories;
using Users.Models.Exceptions;

namespace Users.Application.Features.Role.Commands.DeleteRole
{
    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand>
    {
        private readonly IRoleFinder _roleFinder;
        private readonly IRoleRepository _roleRepository;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public DeleteRoleCommandHandler(IRoleFinder roleFinder, IRoleRepository roleRepository, IUnitOfWork unitOfWork)
        {
            _roleFinder = roleFinder;
            _roleRepository = roleRepository;
            _unitOfWork = unitOfWork;
        }

        /// <summary>Handles a request</summary>
        /// <param name="request">The request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Response from the request</returns>
        public async Task Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var roleEntity = await _roleFinder.GetByIdAsync(request.Id, cancellationToken);
            if (roleEntity is null)
            {
                throw new EntityNotFoundException($"Role with id - {request.Id} doesn't exist");
            }
            _roleRepository.Delete(roleEntity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
