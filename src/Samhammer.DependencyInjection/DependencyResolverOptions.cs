using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Samhammer.DependencyInjection.Handlers;
using Samhammer.DependencyInjection.Providers;

namespace Samhammer.DependencyInjection
{
    public class DependencyResolverOptions
    {
        public ILoggerFactory LoggerFactory { get; set; }

        public IAssemblyResolvingStrategy AssemblyResolvingStrategy { get; set; }
        
        public ITypeResolvingStrategy TypeResolvingStrategy { get; set; }

        public List<IServiceDescriptorProvider> Providers { get; } = new List<IServiceDescriptorProvider>();

        public List<IAttributeServiceDescriptorHandler> Handlers { get; } = new List<IAttributeServiceDescriptorHandler>();
    }
}
