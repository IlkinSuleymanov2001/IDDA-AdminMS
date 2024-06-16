using Application.Common.Mappings;
using Application.Common.Pipelines.Authorization;
using AutoMapper;
using MediatR;

namespace Application.Controllers
{
    public class UserDto : IRequest<string> ,ISecuredRequest
    { 
        public string Name { get; set; }

        public string[] Roles => new string[] { "ADMIN" };
    }
}
