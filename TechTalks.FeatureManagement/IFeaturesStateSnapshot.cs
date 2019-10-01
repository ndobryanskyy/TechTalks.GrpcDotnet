namespace TechTalks.FeatureManagement.Abstractions
{
    public interface IFeaturesStateSnapshot
    {
        bool IsEnabled(string featureName);
    }
}