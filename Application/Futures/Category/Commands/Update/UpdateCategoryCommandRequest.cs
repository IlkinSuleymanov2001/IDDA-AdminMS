using Application.Common.Pipelines.Transaction;
using Application.Futures.Constants;
using Application.Repositories;
using Core.Exceptions;
using Core.Pipelines.Authorization;
using Core.Pipelines.Transaction;
using Core.Response;
using MediatR;

namespace Application.Futures.Category.Commands.Update
{
    public record UpdateCategoryCommandRequest(string oldName, string newName) : ICommand<IResponse>, ISecuredRequest
    {
        public string[] Roles => [Role.ADMIN];
       
    }

    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommandRequest, IResponse>
    {
        private readonly ICategoryRepository _categoryRepository;

        public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IResponse> Handle(UpdateCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetAsync(c => c.Name == request.oldName);
            if (category is null) throw new NotFoundException("not found category");
            category.Name = request.newName;

            _categoryRepository.UpdateAsync(category);
            return new Response() { Message = "category was successfully Updated"};
        }
    }
}
