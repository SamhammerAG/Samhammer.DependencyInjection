using System;

namespace Samhammer.DependencyInjection.Override.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class OverrideAttribute : Attribute
    {
        public string ConfigurationName { get; set; }

        public OverrideAttribute(string configurationName)
        {
            ConfigurationName = configurationName;
        }
    }
}
