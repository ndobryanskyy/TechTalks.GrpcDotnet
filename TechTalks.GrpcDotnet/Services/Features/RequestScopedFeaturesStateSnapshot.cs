using System.Collections.Generic;
using System.Linq;
using TechTalks.FeatureManagement.Abstractions;

namespace TechTalks.GrpcDotnet.Services.Features
{
    internal sealed class RequestScopedFeaturesStateSnapshot : IFeaturesStateSnapshot
    {
        private readonly Dictionary<string, bool> _featuresState;

        public RequestScopedFeaturesStateSnapshot(IFeaturesManager manager)
        {
            _featuresState = manager.GetAllConfiguredFeatures()
                .ToDictionary(x => x.Name, x => x.IsEnabled);
        }

        public bool IsEnabled(string featureName)
        {
            var isEnabled = _featuresState.GetValueOrDefault(featureName, false);

            return isEnabled;
        }
    }
}