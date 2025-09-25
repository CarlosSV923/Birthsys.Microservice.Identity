using Birthsys.Identity.Application.Abstractions;
using Birthsys.Identity.Domain.Abstractions;
using FluentValidation;
using MediatR;

namespace Birthsys.Identity.Application.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> (
        IEnumerable<IValidator<TRequest>> validators
    ): IPipelineBehavior<TRequest, TResponse>
        where TRequest : IUseCaseBase
        where TResponse : Result
    {
        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);
                var validationResults = validators
                    .Select(v => v.Validate(context))
                    .Where(result => result != null && !result.IsValid)
                    .SelectMany(result => result.Errors)
                    .Select(error => $"{error.PropertyName} -> {error.ErrorMessage}")
                    .ToList();

                if (validationResults.Count != 0)
                { 
                    Task.FromResult(Result.Failure(Error.Build(400, "One or more validation errors occurred.", validationResults)));
                }
                
            }

            return next(cancellationToken);
        }
    }
}