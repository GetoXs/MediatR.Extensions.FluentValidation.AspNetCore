using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MediatR.Extensions.FluentValidation.AspNetCore.Tests
{
    public class ValidationTests
    {
        private readonly ServiceProvider Services;

        public readonly IMediator Mediator;

        public ValidationTests()
        {
            ServiceCollection sc = new ServiceCollection();

            var domainAssembly = typeof(ValidationTests).GetTypeInfo().Assembly;

            // Add MediatR
            sc.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(domainAssembly));

            //Add FluentValidation
            sc.AddFluentValidation(new[] { domainAssembly });

            Services = sc.BuildServiceProvider();
            Mediator = Services.GetService<IMediator>()!;

            Assert.NotNull(Mediator);
        }

        [Fact]
        public async Task CheckForValidationPass()
        {
            await Mediator.Send(new Command { Discount = 1 });
        }

        [Fact]
        public async Task CheckForValidationFailure()
        {
            await Assert.ThrowsAsync<ValidationException>(() => Mediator.Send(new Command { Discount = -1 }));
        }
    }

    public class CommandValidation : AbstractValidator<Command>
    {
        public CommandValidation()
        {
            RuleFor(x => x.Discount).GreaterThanOrEqualTo(0);
        }
    }

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
}
