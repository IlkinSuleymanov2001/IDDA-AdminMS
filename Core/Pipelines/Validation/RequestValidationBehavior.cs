using Core.Exceptions;
using FluentValidation;
using MediatR;
using Microsoft.IdentityModel.Tokens;


namespace Core.Pipelines.Validation
{
    public class RequestValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IBaseRequest
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public RequestValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {

            ValidationContext<object> context = new(request);

            var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));

/*            var failures = validationResults
                .Where(r => r.Errors.Any())
                .SelectMany(r => r.Errors)
                .ToList();
*/
            var error = validationResults
                .Where(r => r.Errors.Any())
                  .SelectMany(v => v.Errors)
                  .Select(v => v.ErrorMessage).
                  ToList().FirstOrDefault();


            if (!error.IsNullOrEmpty())
                 throw new BadRequestException(error);

            return await next();
        }
    }
}
