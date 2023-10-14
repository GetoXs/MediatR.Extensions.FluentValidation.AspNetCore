using FluentValidation;
using MediatR.Extensions.FluentValidation.AspNetCore.Tests.Commands;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using System.Reflection;
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

        [Fact(Timeout = 2000)]
        public async Task CheckForAsyncValidationPass()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            await Mediator.Send(new CommandAsync { Discount = 1 });
            stopwatch.Stop();

            Assert.True(stopwatch.ElapsedMilliseconds > 1000);
        }
    }
}
