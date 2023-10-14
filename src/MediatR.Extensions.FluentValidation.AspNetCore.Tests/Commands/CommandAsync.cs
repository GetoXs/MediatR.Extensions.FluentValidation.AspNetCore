using FluentValidation;
using System.Threading;
using System.Threading.Tasks;

namespace MediatR.Extensions.FluentValidation.AspNetCore.Tests.Commands
{
    public class CommandAsync : Command
    {
    }

    public class CommandAsyncHandler : IRequestHandler<CommandAsync>
    {
        public Task Handle(CommandAsync request, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }

    public class CommandAsyncValidation : AbstractValidator<CommandAsync>
    {
        public CommandAsyncValidation()
        {
            RuleFor(x => x.Discount)
                .GreaterThanOrEqualTo(0)
                .MustAsync(async (_, __) => await Task.Delay(1000).ContinueWith(x => true));
        }
    }
}
