using System.Threading.Tasks;

namespace TechTalks.FeatureManagement.Abstractions
{
    public interface IFeaturesState
    {
        Task<bool> IsEnabledAsync(string featureName);
    }
}