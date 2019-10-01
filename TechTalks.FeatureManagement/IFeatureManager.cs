using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace TechTalks.FeatureManagement.Abstractions
{
    public interface IFeaturesManager : IFeaturesState
    {
        IReadOnlyCollection<Feature> GetAllConfiguredFeatures();

        Task EnableFeatureAsync(string featureName, CancellationToken token);

        Task DisableFeatureAsync(string featureName, CancellationToken token);
    }
}