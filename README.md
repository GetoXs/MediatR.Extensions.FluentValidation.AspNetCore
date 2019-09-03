# MediatR.Extensions.FluentValidation.AspNetCore

[![NuGet](https://img.shields.io/nuget/dt/MediatR.Extensions.FluentValidation.AspNetCore.svg)](https://www.nuget.org/packages/MediatR.Extensions.FluentValidation.AspNetCore) 
[![NuGet](https://img.shields.io/nuget/vpre/MediatR.Extensions.FluentValidation.AspNetCore.svg)](https://www.nuget.org/packages/MediatR.Extensions.FluentValidation.AspNetCore)
[![license](https://img.shields.io/github/license/GetoXs/MediatR.Extensions.FluentValidation.AspNetCore.svg)](https://github.com/GetoXs/MediatR.Extensions.FluentValidation.AspNetCore/blob/master/lICENSE)

MediatR extension for FluentValidation using asp.net core

### Install with nuget

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
    services.AddMediatR(domainAssembly);

    //Add FluentValidation
    services.AddFluentValidation(new[] {domainAssembly});
    
    //Add other stuffs
    ...
}

```


## Use

```csharp

public class GenerateInvoiceValidator : AbstractValidator<GenerateInvoiceCommand>
{
    public Valid()
    {
        RuleFor(x => x.Month).LowerThan(13);
        // etc.
    }
}
```

More examples check FluentValidation docs:  https://fluentvalidation.net/start
