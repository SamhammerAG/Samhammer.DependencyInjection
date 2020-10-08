[![Build Status](https://travis-ci.com/SamhammerAG/Samhammer.DependencyInjection.svg?branch=master)](https://travis-ci.com/SamhammerAG/Samhammer.DependencyInjection)

# Samhammer.DependencyInjection

## Usage

#### How to add this to your project:
- reference this package to your project: https://www.nuget.org/packages/Samhammer.DependencyInjection/
- call ResolveDependencies on the IServiceCollection

```csharp
services.ResolveDependencies();
```

#### How to register a class as service
- add Inject attribute to the class
- set target optionally (default: Matching)
- set servicelifetime optionally (default: Scoped)

##### Target Matching
- will register as service to interface with same name as class (IClassname)

```csharp
[Inject(Target.Matching, ServiceLifetime.Scoped)]
public class Class : IClass
{
}

public interface IClass : IBaseClass
{
}

public interface IBaseClass
{
}
```

##### Target All
- will register as service to all implemented interfaces of class
- ATTENTION with lifetime singleton / scoped. Each interface service returns his own instance of the class

```csharp
[Inject(Target.All, ServiceLifetime.Scoped)]
public class Class : IClass
{
}

public interface IClass : IBaseClass
{
}

public interface IBaseClass
{
}
```

#### How to register a class as service with specific type
- add InjectAs attribute to the class
- add interface or class as paramter is required (class will register to this type)

```csharp
[InjectAs(typeof(IServiceToRegister))]
public class ClassWithSpecificService: IServiceToRegister, IServiceNotRegister
{
}

public interface IServiceToRegister
{
}

public interface IServiceNotRegister
{
}
```

#### How to register a instance as service
- add Factory attribute to the factory class
- add at least one public static method with parameter IServiceProvider
- the returned instance will be registered to the return type of method

```csharp
[Factory]
public class Factory
{
    public static IClassFromFactory Build(IServiceProvider serviceProvider)
    {
        return new ClassFromFactory();
    }
}
```

## Configuration
Starting with version 3.1.5 all customizations needs to be done with the options action.

The registrations to servicecollection will no longer be used because we dont want to use ioc to setup ioc.
@see also https://docs.microsoft.com/de-de/dotnet/core/compatibility/2.2-3.1#hosting-generic-host-restricts-startup-constructor-injection

#### How to enable logging?
By default the project will not do any logging, but you can activate it.
This will require that you provide an ILoggerFactory from Microsoft.Extensions.Logging.

###### Sample with microsoft console logger.
```csharp
var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole().SetMinimumLevel(LogLevel.Debug));
services.ResolveDependencies(options => options.SetLogging(loggerFactory));
```

###### Sample with serilog logger. (you need to setup serilog before)
```csharp
services.ResolveDependencies(options => options.SetLogging(new SerilogLoggerFactory()));
```

#### How to change assembly resolving strategy?
By default the project will only resolve types of project assemblies, but not on packages or binaries.
But you can replace the default strategy with your own implementation.

```csharp
services.ResolveDependencies(options => options.SetStrategy(new MyAssemblyResolvingStrategy()));
```

#### How to add additional service provider
By default the project will only resolve types with Inject attributes.
But you can add additonal resolving provider with your own implementation.

```csharp
services.ResolveDependencies(options => options.AddProvider<MyServiceDescriptorProvider>((logger, strategy) => new MyServiceDescriptorProvider(logger, strategy)));
```

## Contribute

#### How to publish package
- set package version in Samhammer.DependencyInjection.csproj
- add information to changelog
- create git tag
- dotnet pack -c Release
- nuget push .\bin\Release\Samhammer.DependencyInjection.*.nupkg NUGET_API_KEY -src https://api.nuget.org/v3/index.json
- (optional) nuget setapikey NUGET_API_KEY -source https://api.nuget.org/v3/index.json
