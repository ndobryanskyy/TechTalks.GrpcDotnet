using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using TechTalks.FeatureManagement.Abstractions;namespace TechTalks.GrpcDotnet.Services.Features
{
    internal sealed class ConfigurationDrivenFeaturesStateConfigurationSource : IFeaturesStateConfigurationSource
    {
        private const string FeaturesConfigurationSectionName = "Features";

        private readonly IConfiguration _configuration;

        public ConfigurationDrivenFeaturesStateConfigurationSource(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task<IReadOnlyCollection<Feature>> GetInitialFeaturesConfigurationAsync(CancellationToken token)
        {
            var featuresSection = _configuration.GetSection(FeaturesConfigurationSectionName);

            if (featuresSection.Exists())
            {
                var features = featuresSection.GetChildren()
                    .Select(x =>
                    {
                        if (bool.TryParse(x.Value, out var isEnabled))
                        {
                            return new Feature(x.Key, isEnabled);
                        }

                        return default(Feature?);
                    })
                    .Where(x => x.HasValue)
                    .Select(x => x.Value)
                    .ToArray();

                return Task.FromResult<IReadOnlyCollection<Feature>>(features);
            }

            return Task.FromResult<IReadOnlyCollection<Feature>>(new Feature[0]);
        }

        public event FeatureStateChangedEvent OnFeatureStateChanged;
    }
}