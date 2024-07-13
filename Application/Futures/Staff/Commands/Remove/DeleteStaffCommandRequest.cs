using Application.Futures.Constants;
using Application.Repositories;
using Core.Exceptions;
using Core.Pipelines.Authorization;
using Core.Pipelines.Transaction;
using Core.Response;
using Domain.Entities;
using MediatR;

namespace Application.Futures.Staff.Commands.Remove
{
    public record DeleteStaffCommandRequest(string Username) : ICommand<IResponse>, ISecuredRequest
    {
        public string[] Roles => [Role.ADMIN,Role.SUPER_STAFF];
    }
    public class DeleteStaffCommandHandler(IStaffRepository staffRepository)
        : IRequestHandler<DeleteStaffCommandRequest, IResponse>
    {

        public readonly IStaffRepository StaffRepository = staffRepository;

        public async Task<IResponse> Handle(DeleteStaffCommandRequest request, CancellationToken cancellationToken)
        {
            var staff = await StaffRepository.GetAsync(c => c.Username == request.Username) ?? throw new NotFoundException();
            await StaffRepository.DeleteAsync(staff);
            await StaffRepository.SaveChangesAsync(cancellationToken);

            return new Response();

        }
    }


}
