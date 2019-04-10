using System.Collections.Generic;
using System.Linq;
using Samhammer.DependencyInjection.Abstractions;

namespace Samhammer.DependencyInjection.Test.TestData.InjectedList
{
    [Inject]
    public class TargetService : ITargetService
    {
        public List<IService> Services { get; set; }

        public TargetService(IEnumerable<IService> services)
        {
            Services = services.ToList();
        }
    }

    public interface ITargetService
    {
    }

    public interface IService
    {
    }
}
