using System.Diagnostics.CodeAnalysis;
using Application.Futures.Constants;
using Application.Repositories;
using Core.Exceptions;
using Core.Pipelines.Authorization;
using Core.Pipelines.Transaction;
using Core.Response;
using Core.Services.Security;
using MediatR;

namespace Application.Futures.Staff.Commands.Update
{
    public record UpdateStaffCommandRequest([NotNull]string Fullname, string? Username) : ICommand<IResponse>, ISecuredRequest
    {
        public string[] Roles => [Role.ADMIN,Role.STAFF,Role.SUPER_STAFF];
    }

    public class UpdateStaffCommandHandler(
        IStaffRepository staffRepository,
        ISecurityService securityService)
        : IRequestHandler<UpdateStaffCommandRequest, IResponse>
    {
        public async Task<IResponse> Handle(UpdateStaffCommandRequest request, CancellationToken cancellationToken)
        {
            if (securityService.CurrentRoleEqualsTo(Role.ADMIN))
            {
                var staff = await staffRepository.GetAsync(g => g.Username == request.Username,
                filterIgnore:true,enableTracking:true) ??  throw new NotFoundException(Messages.NotFoundStaff);
                staff.Fullname = request.Fullname;
            }
            else
            {
                var currentStaff = await staffRepository.GetAsync(g => g.Username == securityService.GetUsername(), enableTracking: true)
                 ?? throw new NotFoundException(Messages.NotFoundStaff);
                currentStaff.Fullname = request.Fullname;
            }

            //await  staffRepository.UpdateAsync(mapper.Map(request, currentStaff));
            await staffRepository.SaveChangesAsync(cancellationToken);
            return  Response.Ok("update successfully fullname");

        }
    }


}
