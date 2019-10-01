using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace TechTalks.FeatureManagement.Abstractions
{
    public delegate void FeatureStateChangedEvent(string featureName, bool isEnabled);

    public interface IFeaturesStateConfigurationSource
    {
        Task<IReadOnlyCollection<Feature>> GetInitialFeaturesConfigurationAsync(CancellationToken token);

        event FeatureStateChangedEvent OnFeatureStateChanged;
    }
}