using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace PhlegmaticOne.HandMapper.Extensions;

public static class HandMapperServiceExtensions
{
    public static IServiceCollection AddHandMappers(this IServiceCollection services, Assembly handMappersAssembly)
    {
        var mappers = handMappersAssembly.GetTypes().Where(x => x.IsAbstract == false &&
            x.IsAssignableTo(typeof(IHandMapper)));

        var instances = mappers.Select(x => Activator.CreateInstance(x) as IHandMapper).ToList();

        services.AddSingleton(typeof(List<IHandMapper>), instances);

        services.AddSingleton(typeof(IHandMapperService), x => 
                new HandMapperService(x.GetRequiredService<List<IHandMapper>>()));

        return services;
    }
}
