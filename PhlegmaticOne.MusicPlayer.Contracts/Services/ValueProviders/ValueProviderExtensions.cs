using Microsoft.Extensions.DependencyInjection;

namespace PhlegmaticOne.MusicPlayer.Contracts.Services.ValueProviders;

public static class ValueProviderExtensions
{
    public static IServiceCollection AddValueProvider<T>(this IServiceCollection serviceCollection) where T : class
    {
        serviceCollection.AddSingleton<IValueProvider<T>, ValueProvider<T>>();
        return serviceCollection;
    }
}