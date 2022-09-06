using System.Drawing;
using Microsoft.Extensions.DependencyInjection;

namespace PhlegmaticOne.MusicPlayer.Contracts.Services.Cache;

public static class CacheServiceExtensions
{
    public static IServiceCollection AddCacheService(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<ICacheService<Bitmap>, CollectionCoversCacheService>();
        return serviceCollection;
    }
}