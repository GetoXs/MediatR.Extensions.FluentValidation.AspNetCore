using FluentValidation;
using System.Threading;
using System.Threading.Tasks;

namespace MediatR.Extensions.FluentValidation.AspNetCore.Tests.Commands
{
    public class Command : IRequest
    {
        public decimal Discount { get; set; }
    }

    public class CommandHandler : IRequestHandler<Command>
    {
        public Task Handle(Command request, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }

    public class CommandValidation : AbstractValidator<Command>
    {
        public CommandValidation()
        {
            RuleFor(x => x.Discount)
                .GreaterThanOrEqualTo(0)
                .MustAsync(async (_, __) => await Task.Delay(0).ContinueWith(x => true));
        }
    }
}
