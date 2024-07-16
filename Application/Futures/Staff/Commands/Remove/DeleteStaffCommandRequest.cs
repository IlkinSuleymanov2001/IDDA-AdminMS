using Application.Futures.Constants;
using Application.Repositories;
using Core.Exceptions;
using Core.Pipelines.Authorization;
using Core.Pipelines.Transaction;
using Core.Response;
using Core.Services.Security;
using MediatR;

namespace Application.Futures.Staff.Commands.Remove
{
    public record DeleteStaffCommandRequest(string Username) : ICommand<IResponse>, ISecuredRequest
    {
        public string[] Roles => [Role.ADMIN,Role.SUPER_STAFF,Role.STAFF];
    }
    public class DeleteStaffCommandHandler(IStaffRepository staffRepository,ISecurityService securityService)
        : IRequestHandler<DeleteStaffCommandRequest, IResponse>
    {

        public readonly IStaffRepository StaffRepository = staffRepository;

        public async Task<IResponse> Handle(DeleteStaffCommandRequest request, CancellationToken cancellationToken)
        {

            if (securityService.CurrentRoleEqualsTo(Role.ADMIN))
            {
                var staff = await StaffRepository.GetAsync(c => c.Username == request.Username, 
                                filterIgnore: true) ?? throw new NotFoundException();
                await StaffRepository.DeleteAsync(staff);
            }
            else
            {
                var staff = await StaffRepository.GetAsync(c => c.Username == securityService.GetUsername())
                            ?? throw new NotFoundException();
                await StaffRepository.DeleteAsync(staff);
            }

            await StaffRepository.SaveChangesAsync(cancellationToken);
            return  Response.Ok("delete operation is success");

        }
    }


}
