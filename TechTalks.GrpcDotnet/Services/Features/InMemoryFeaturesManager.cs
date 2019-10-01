using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using TechTalks.FeatureManagement.Abstractions;

namespace TechTalks.GrpcDotnet.Services.Features
{
    internal sealed class InMemoryFeaturesManager : IFeaturesManager
    {
        private readonly ConcurrentDictionary<string, bool> _featuresStates;

        public InMemoryFeaturesManager(IOptions<InitialFeaturesState> initialFeaturesState)
        {
            _featuresStates = new ConcurrentDictionary<string, bool>(initialFeaturesState.Value ?? new InitialFeaturesState());
        }

        public Task<bool> IsEnabledAsync(string featureName)
        {
            var isEnabled = _featuresStates.GetValueOrDefault(featureName, false);

            return Task.FromResult(isEnabled);
        }

        public IReadOnlyCollection<Feature> GetAllConfiguredFeatures()
        {
            var features = _featuresStates.Select(x => new Feature(x.Key, x.Value));

            return features.ToArray();
        }

        public Task EnableFeatureAsync(string featureName, CancellationToken token)
        {
            SetFeature(featureName, true);

            return Task.CompletedTask;
        }

        public Task DisableFeatureAsync(string featureName, CancellationToken token)
        {
            SetFeature(featureName, true);

            return Task.CompletedTask;
        }

        private void SetFeature(string featureName, bool isEnabled) => 
            _featuresStates.AddOrUpdate(featureName, isEnabled, (key, value) => isEnabled);
    }
}