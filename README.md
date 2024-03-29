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
services.ResolveDependencies(options => options.SetAssemblyStrategy(new MyAssemblyResolvingStrategy()));
```

#### How to change type resolving strategy?
It is possible to change the way types are found by the DependencyInjectionAttribute. Also one
could change the naming schema of interfaces by using a custom type resolving strategy.

```csharp
services.ResolveDependencies(options => options.SetTypeStrategy(new MyTypeResolvingStrategy()));
```

#### How to add additional service provider
By default the project will only resolve types with Inject attributes.
But you can add additonal resolving provider with your own implementation.

```csharp
services.ResolveDependencies(options => options.AddProvider<MyServiceDescriptorProvider>((logger, o) => new MyServiceDescriptorProvider(logger, o)));
```

## Using overrides
You can use overrides to have a different behavior depending on the configuration used on resolving dependencies.

Just add the override attribute to a new class that inherits a class with one of the dependency injection attributes.
As soon as the configuration matches "MyConfigName" your original implementation will be overwritten by the implementation of this class.

Classes with non matching configuration names are not registered.

```csharp
[Override("MyConfigName")]
public class MyConfigNameOverride : InjectParentClass
{
}

[Override("OtherConfigName")]
public class OtherConfigNameOverride : InjectParentClass
{
}

[Inject]
public class InjectParentClass : IInjectParentClass
{
}

public interface IInjectParentClass
{
}
```

### How to use

Add this nuget package to your project: https://www.nuget.org/packages/Samhammer.DependencyInjection.Override/

```csharp
serviceCollection.ResolveDependencies(o => { o.UseOverride("MyConfigName"); });
```

## Contribute

#### How to publish package
- Create a tag and let the github action do the publishing for you
