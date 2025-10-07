using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Samhammer.DependencyInjection.Attributes;

namespace Samhammer.DependencyInjection.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class InjectAbWithConfigsAttribute : DependencyInjectionAttribute
    {
        public Target Target { get; }

        public string ConfigSections { get; set; }

        public InjectAbWithConfigsAttribute(string configSections, Target target = Target.Matching, ServiceLifetime lifetime = ServiceLifetime.Scoped) 
            : base(lifetime)
        {
            Target = target;
            ConfigSections = configSections;
        }
    }
}
