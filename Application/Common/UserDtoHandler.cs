using AutoMapper;
using MediatR;

namespace Application.Controllers
{
    public class UserDtoHandler : IRequestHandler<UserDto, string>
    {
        private readonly IMapper mapper;

        public UserDtoHandler(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public Task<string> Handle(UserDto request, CancellationToken cancellationToken)
        {
            var user = mapper.Map<User>(request);
            return Task.FromResult(user.Name);
        }
    }
}
