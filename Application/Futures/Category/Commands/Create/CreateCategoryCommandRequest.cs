using Application.Futures.Constants;
using Application.Repositories;
using Core.Pipelines.Authorization;
using Core.Pipelines.Transaction;
using Core.Response;
using MediatR;

namespace Application.Futures.Category.Commands.Create
{
    public record CreateCategoryCommandRequest(string Name) : ICommand<IResponse>, ISecuredRequest
    {
        public string[] Roles => [Role.ADMIN];
    }

    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommandRequest, IResponse>
    {
        private readonly ICategoryRepository _categoryRepository;

        public CreateCategoryCommandHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IResponse> Handle(CreateCategoryCommandRequest request, CancellationToken cancellationToken)
        {
           await  _categoryRepository.CreateAsync(new Domain.Entities.Category() { Name= request.Name});
            return new Response()
            {
                Message = $"{request.Name} category was successfully created"
            };
        }
    }
}
