using Application.Common.Exceptions;
using Application.Common.Pipelines.Authorization;
using Application.Common.Pipelines.Transaction;
using Application.Common.Response;
using Application.Futures.Staff.Commands.Create;
using Application.Repositories;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Futures.Staff.Commands.Remove
{
    public record DeleteStaffCommandRequest(string Username) : ICommand<IResponse>, ISecuredRequest
    {
        public string[] Roles => ["ADMIN"];
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
