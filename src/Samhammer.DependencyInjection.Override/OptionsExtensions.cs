using Samhammer.DependencyInjection.Override.Strategy;

namespace Samhammer.DependencyInjection.Override
{
    public static class OptionsExtensions
    {
        public static DependencyResolverOptions UseOverride(this DependencyResolverOptions options, string configurationName)
        {
            var overrideTypeResolvingStrategy = new OverrideTypeResolvingStrategy(configurationName);
            options.SetTypeStrategy(overrideTypeResolvingStrategy);
            return options;
        }
    }
}
