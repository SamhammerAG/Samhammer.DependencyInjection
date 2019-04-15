using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace Samhammer.DependencyInjection.Providers
{
    public interface IServiceDescriptorProvider
    {
        IEnumerable<ServiceDescriptor> ResolveServices();
    }
}
