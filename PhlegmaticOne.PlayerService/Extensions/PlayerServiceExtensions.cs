using Microsoft.Extensions.DependencyInjection;
using PhlegmaticOne.Players.Base;
using PhlegmaticOne.PlayerService.Base;
using PhlegmaticOne.PlayerService.Services;

namespace PhlegmaticOne.PlayerService.Extensions;

public static class PlayerServiceExtensions
{
    public static PlayerServiceExtensionsHelper AddPlayerService<T>(this IServiceCollection serviceCollection) where T : class, IHaveUrl
    {
        serviceCollection.AddSingleton<IPlayerService<T>, PlayerService<T>>();
        return new PlayerServiceExtensionsHelper(serviceCollection);
    }
}

public class PlayerServiceExtensionsHelper
{
    private readonly IServiceCollection _serviceCollection;

    public PlayerServiceExtensionsHelper(IServiceCollection serviceCollection) => 
        _serviceCollection = serviceCollection;

    public IServiceCollection UsingPlayer<T>() where T : class, IPlayer
    {
        _serviceCollection.AddSingleton<IPlayer, T>();
        return _serviceCollection;
    }
}
