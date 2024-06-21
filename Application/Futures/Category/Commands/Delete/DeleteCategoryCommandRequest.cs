using Application.Futures.Category.Commands.Create;
using Application.Futures.Constants;
using Application.Repositories;
using Core.Pipelines.Authorization;
using Core.Pipelines.Transaction;
using Core.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Application.Futures.Category.Commands.Delete
{
    public record DeleteCategoryCommandRequest(string Name) : ICommand<IResponse>, ISecuredRequest
    {
        public string[] Roles => [Role.ADMIN];
    }

    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommandRequest, IResponse>
    {
        private readonly ICategoryRepository _categoryRepository;

        public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IResponse> Handle(DeleteCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            await _categoryRepository.DeleteWhere(c=>c.Name == request.Name );
            return new Response()
            {
                Message = $"{request.Name} category was successfully Deleted"
            };
        }
    }

}
