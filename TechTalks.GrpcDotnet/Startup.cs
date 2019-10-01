using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TechTalks.FeatureManagement.Abstractions;
using TechTalks.GrpcDotnet.Services;
using TechTalks.GrpcDotnet.Services.Features;

namespace TechTalks.GrpcDotnet
{
    public sealed class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddFeatureManagement();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddFeatureManagement(this IServiceCollection services)
        {
            services.AddOptions<InitialFeaturesState>();
            services.AddSingleton<IFeaturesManager, InMemoryFeaturesManager>();
            services.AddSingleton<IFeaturesState>(container => container.GetService<IFeaturesManager>());
            services.AddScoped<IFeaturesStateSnapshot, RequestScopedFeaturesStateSnapshot>();
            services.AddSingleton<IFeaturesStateConfigurationSource, ConfigurationDrivenFeaturesStateConfigurationSource>();

            services.AddHostedService<InitialFeaturesStateLoader>();

            return services;
        }
    }
}
