
using Application.Futures.Constants;
using Application.Futures.Organization.Dtos;
using Application.Repositories;
using AutoMapper;
using Core.Exceptions;
using Core.Pipelines.Authorization;
using Core.Pipelines.Transaction;
using Core.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Futures.Organization.Commands.Update
{
    public record UpdateOrganizationActiveOrDeActiveCommand(string Name):ICommand<IDataResponse>,ISecuredRequest
    {
        public string[] Roles => [Role.ADMIN];

        public class UpdateStaffCommandHandler(IOrganizationRepository organizationRepository,IMapper mapper,IStaffRepository staffRepository)
            : IRequestHandler<UpdateOrganizationActiveOrDeActiveCommand, IDataResponse>
        {
            public async Task<IDataResponse> Handle(UpdateOrganizationActiveOrDeActiveCommand request, CancellationToken cancellationToken)
            {
                var org = await organizationRepository.
                    GetAsync(c => c.Name == request.Name,filterIgnore:true,
                        include:c=>c.Include(c=>c.Staffs)) 
                    ?? throw new NotFoundException(Messages.NotFoundOrganization);

                org.Active = org.Active switch
                {
                    true => false,
                    false => true,
                    _ => true 
                };

                if (org.Staffs != null)
                {
                    if (org.Active == true)  foreach (var staff in org.Staffs) staff.Active = true;
                    else  foreach (var staff in org.Staffs) staff.Active = false;
                }

                await organizationRepository.UpdateAsync(org);
                if (org.Staffs != null)  staffRepository.UpdateRange(org.Staffs);

                await organizationRepository.SaveChangesAsync(cancellationToken);
                var orgDto = mapper.Map<OrganizationDto>(org);
                return DataResponse.Ok(orgDto);

            }
        }
    }
}
