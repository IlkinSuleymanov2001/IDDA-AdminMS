using Application.Futures.Constants;
using Application.Repositories;
using Core.Exceptions;
using Core.Pipelines.Authorization;
using Core.Pipelines.Transaction;
using Core.Response;
using MediatR;

namespace Application.Futures.Staff.Commands.Remove
{
    public record DeleteStaffCommandRequest(string Username) : ICommand<IResponse>, ISecuredRequest
    {
        public string[] Roles => [Role.ADMIN,Role.SUPER_STAFF];
    }
    public class DeleteStaffCommandHandler : IRequestHandler<DeleteStaffCommandRequest, IResponse>
    {

        public readonly IStaffRepository StaffRepository;

        public DeleteStaffCommandHandler(IStaffRepository staffRepository)
        {
            StaffRepository = staffRepository;
        }

        public async Task<IResponse> Handle(DeleteStaffCommandRequest request, CancellationToken cancellationToken)
        {
            var staf = await StaffRepository.GetAsync(c => c.Username == request.Username);
            if (staf is null) throw new NotFoundException("staff not found");

            StaffRepository.Delete(staf);

            return new Response { Message = "Staff success deleted" };

        }
    }


}
