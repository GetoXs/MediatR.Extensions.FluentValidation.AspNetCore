using FluentValidation;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MediatR.Extensions.FluentValidation.AspNetCore
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : class, IBaseRequest
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public Task<TResponse> Handle(TRequest request
            , RequestHandlerDelegate<TResponse> next
            , CancellationToken cancellationToken
        )
        {
            var failures = _validators
                .Select(v => v.Validate(request))
                .SelectMany(result => result.Errors)
                .Where(f => f != null)
                .ToList();

            if (failures.Any())
            {
                throw new ValidationException(failures);
            }

            return next();
        }
    }
}