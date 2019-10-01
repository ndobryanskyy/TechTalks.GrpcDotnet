using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using TechTalks.FeatureManagement.Abstractions;

namespace TechTalks.GrpcDotnet.Services.Features
{
    internal sealed class InitialFeaturesStateLoader : IHostedService
    {
        private readonly IEnumerable<IFeaturesStateConfigurationSource> _featuresStateConfigurationSources;
        private readonly InitialFeaturesState _featuresState;

        public InitialFeaturesStateLoader(
            IOptions<InitialFeaturesState> featuresState, 
            IEnumerable<IFeaturesStateConfigurationSource> featuresStateConfigurationSources)
        {
            _featuresStateConfigurationSources = featuresStateConfigurationSources;
            _featuresState = featuresState.Value ?? new InitialFeaturesState();
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            foreach (var configurationSource in _featuresStateConfigurationSources)
            {
                var configurations = await configurationSource.GetInitialFeaturesConfigurationAsync(cancellationToken);

                foreach (var configuration in configurations)
                {
                    _featuresState[configuration.Name] = configuration.IsEnabled;
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}