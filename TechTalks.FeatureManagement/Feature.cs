namespace TechTalks.FeatureManagement.Abstractions
{
    public readonly struct Feature
    {
        public Feature(string name, bool isEnabled)
        {
            Name = name;
            IsEnabled = isEnabled;
        }

        public string Name { get; }

        public bool IsEnabled { get; }
    }
}