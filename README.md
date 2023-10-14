# MediatR.Extensions.FluentValidation.AspNetCore

[![GitHub Actions Status](https://github.com/GetoXs/MediatR.Extensions.FluentValidation.AspNetCore/workflows/Build%20&%20Test/badge.svg)](https://github.com/GetoXs/MediatR.Extensions.FluentValidation.AspNetCore/actions)
[![NuGet](https://img.shields.io/nuget/dt/MediatR.Extensions.FluentValidation.AspNetCore.svg)](https://www.nuget.org/packages/MediatR.Extensions.FluentValidation.AspNetCore) 
[![NuGet](https://img.shields.io/nuget/vpre/MediatR.Extensions.FluentValidation.AspNetCore.svg)](https://www.nuget.org/packages/MediatR.Extensions.FluentValidation.AspNetCore)
[![license](https://img.shields.io/github/license/GetoXs/MediatR.Extensions.FluentValidation.AspNetCore.svg)](https://github.com/GetoXs/MediatR.Extensions.FluentValidation.AspNetCore/blob/master/LICENSE)

MediatR extension to the FluentValidation for .NET framework

# Install

First you need to install packages [Mediatr](https://github.com/jbogard/MediatR) and [FluentValidation](https://github.com/FluentValidation/FluentValidation), then follow the instructions below

## Install with nuget

```
Install-Package MediatR.Extensions.FluentValidation.AspNetCore
```

## Install with .NET CLI
```
dotnet add package MediatR.Extensions.FluentValidation.AspNetCore
```

# How to use

## Setup - Add configuration in startup 


```csharp

public void ConfigureServices(IServiceCollection services)
{
    // Add framework services etc.
    services.AddMvc();
    
    var domainAssembly = typeof(GenerateInvoiceHandler).GetTypeInfo().Assembly;
    // Add MediatR
    services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(domainAssembly));

    //Add FluentValidation
    services.AddFluentValidation(new[] {domainAssembly});
    
    //Add other stuffs
    ...
}

```

## Use

Implement validator for your `IRequest` objects. Validation will be executed before handling `IRequestHandler`.

```csharp

public class GenerateInvoiceValidator : AbstractValidator<GenerateInvoiceRequest>
{
    public GenerateInvoiceValidator()
    {
        RuleFor(x => x.Month).LowerThan(13);
        // etc.
    }
}

public class GenerateInvoiceRequest : IRequest
{
    public int Month { get; set; }
}
public class GenerateInvoiceRequestHandler : IRequestHandler<GenerateInvoiceRequest>
{
    public async Task Handle(GenerateInvoiceRequest request, CancellationToken cancellationToken)
    {
        // request data has been validated
        ...
    }
}
```

More examples check FluentValidation docs:  https://fluentvalidation.net/start
