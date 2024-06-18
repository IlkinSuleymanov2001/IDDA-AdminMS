using Application.Common.Mappings;
using Application.Common.Pipelines.Authorization;
using Application.Common.Pipelines.Transaction;
using AutoMapper;
using MediatR;

namespace Application.Controllers
{
    public class UserDto : IQuery,ISecuredRequest
    { 
        public string Name { get; set; }
        public string[] Roles =>new string[] {"TTTT","abc", "Test","USER", "TTTT" };
    }
}
