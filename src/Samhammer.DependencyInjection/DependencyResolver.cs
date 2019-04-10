using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Samhammer.DependencyInjection.Abstractions;
using Samhammer.Utils.Reflection;

namespace Samhammer.DependencyInjection
{
    public class DependencyResolver
    {
        private ILogger<DependencyResolver> Logger { get; }

        private readonly object lockObject = new object();

        public DependencyResolver(ILogger<DependencyResolver> logger)
        {
            Logger = logger;
        }

        public void ResolveDependencies(IServiceCollection services)
        {
            Logger.LogInformation("Start service initialization");

            lock (lockObject)
            {
                var assemblies = AssemblyUtils.LoadAllAssembliesOfProject();
                Logger.LogTrace("Loaded assemblies: {Assemblies}.", assemblies.Select(a => a.GetName().Name));

                var types = ReflectionUtils.FindAllExportedTypesWithAttribute(assemblies, typeof(InjectAttribute));
                Logger.LogTrace("Loaded types with attribute {Attribute}: {Types}.", typeof(InjectAttribute), types);

                var factoryTypes = ReflectionUtils.FindAllExportedTypesWithAttribute(assemblies, typeof(FactoryAttribute));
                Logger.LogTrace("Loaded types with attribute {Attribute}: {Types}.", typeof(FactoryAttribute), types);

                foreach (var type in types)
                {
                    ResolveService(services, type);
                }

                foreach (var type in factoryTypes)
                {
                    ResolveServiceFactory(services, type);
                }
            }

            Logger.LogInformation("Finished service initialization");
        }

        public void ResolveService(IServiceCollection services, Type implementationType)
        {
            var injectAttribute = implementationType.GetTypeInfo().GetCustomAttribute<InjectAttribute>(true);
            var serviceTypes = implementationType.GetInterfaces().ToList();

            if (serviceTypes.Count == 0)
            {
                Logger.LogWarning("Implementation {ServiceImpl} has no interfaces defined", implementationType);
            }

            foreach (var serviceType in serviceTypes)
            {
                var serviceDescriptor = new ServiceDescriptor(serviceType, implementationType, injectAttribute.LifeTime);
                services.Add(serviceDescriptor);
                Logger.LogDebug("Added service {Service} with implementation {ServiceImpl} and lifetime {LifeTime}", serviceType, implementationType, injectAttribute.LifeTime);
            }
        }

        public void ResolveServiceFactory(IServiceCollection services, Type factoryType)
        {
            var factoryAttribute = factoryType.GetTypeInfo().GetCustomAttribute<FactoryAttribute>(true);
            var factoryMethods = GetFactoryMethods(factoryType);

            foreach (var factoryMethod in factoryMethods)
            {
                var serviceType = factoryMethod.ReturnType;
                object FactoryFunc(IServiceProvider provider) => factoryMethod.Invoke(null, new object[] { provider });

                var serviceDescriptor = new ServiceDescriptor(serviceType, FactoryFunc, factoryAttribute.LifeTime);
                services.Add(serviceDescriptor);
                Logger.LogDebug("Added service {Service} with factory {Factory} and lifetime {LifeTime}", serviceType, factoryType, factoryAttribute.LifeTime);
            }
        }

        public List<MethodInfo> GetFactoryMethods(Type factoryType)
        {
            var methods = factoryType.GetMethods(BindingFlags.Static | BindingFlags.Public).ToList();

            methods = methods
                .Where(method => method.GetParameters().Length == 1 && method.GetParameters()[0].ParameterType == typeof(IServiceProvider))
                .ToList();

            if (methods.Count == 0)
            {
                Logger.LogWarning("Factory {Factory} has no factory methods defined", factoryType);
            }

            return methods;
        }
    }
}
