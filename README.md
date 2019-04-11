# Samhammer.DependencyInjection

## Usage

#### How to add this to your project:
- reference this package to your project: https://www.nuget.org/packages/Samhammer.DependencyInjection/
- call ResolveDependencies on the IServiceCollection

```csharp
services.ResolveDependencies();
```

#### How to register a class
- add Inject attribute to the class
- add at least one interface to the class (class will register on all interfaces)
- set servicelifetime optionally (default: Scoped)

```csharp
[Inject(ServiceLifetime.Scoped)]
public class Class : IClass
{
}

public interface IClass
{
}
```

#### How to register a instace
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

## Contribute

#### How to publish package
- set package version in Samhammer.DependencyInjection.csproj
- dotnet pack -c Release
- nuget push .\bin\Release\Samhammer.DependencyInjection.*.nupkg NUGET_API_KEY -src https://api.nuget.org/v3/index.json
- (optional) nuget setapikey NUGET_API_KEY -source https://api.nuget.org/v3/index.json
