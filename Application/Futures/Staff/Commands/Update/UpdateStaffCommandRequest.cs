using Application.Common.Exceptions;
using Application.Common.Pipelines.Authorization;
using Application.Common.Pipelines.Transaction;
using Application.Common.Response;
using Application.Futures.Staff.Commands.Create;
using Application.Repositories;
using Application.Services;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
