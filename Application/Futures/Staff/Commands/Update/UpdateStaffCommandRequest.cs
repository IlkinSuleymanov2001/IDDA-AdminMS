using Application.Repositories;
using AutoMapper;
using Core.Pipelines.Authorization;
using Core.Pipelines.Transaction;
using Core.Response;
using Core.Services.Security;
using MediatR;

namespace Application.Futures.Staff.Commands.Update
{
    public record UpdateStaffCommandRequest(string? Fullname, string Username) : ICommand<IResponse>, ISecuredRequest
    {
        public string[] Roles => ["GOVERMENT"];
    }

    public class UpdateStaffCommandHandler : IRequestHandler<UpdateStaffCommandRequest, IResponse>
    {

       private readonly IStaffRepository _staffRepository;
       private readonly IMapper _mapper;
       private readonly ISecurityService _securityService;


        public UpdateStaffCommandHandler(IStaffRepository staffRepository, IMapper mapper, ISecurityService securityService)
        {
            _staffRepository = staffRepository;
            _mapper = mapper;
            _securityService = securityService;
        }



        public async Task<IResponse> Handle(UpdateStaffCommandRequest request, CancellationToken cancellationToken)
        {
           string? username = _securityService.GetUsername();
           var currentStaff =  await _staffRepository.GetAsync(g => g.Username == username);
            _staffRepository.Update(_mapper.Map(request, currentStaff));

            return new Response { Message = "Staff success updated" };

        }
    }


}
