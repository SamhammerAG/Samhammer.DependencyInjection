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
[Inject(ServiceLifetime.Scoped)]
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

## Contribute

#### How to publish package
- set package version in Samhammer.DependencyInjection.csproj
- dotnet pack -c Release
- nuget push .\bin\Release\Samhammer.DependencyInjection.*.nupkg NUGET_API_KEY -src https://api.nuget.org/v3/index.json
- (optional) nuget setapikey NUGET_API_KEY -source https://api.nuget.org/v3/index.json
