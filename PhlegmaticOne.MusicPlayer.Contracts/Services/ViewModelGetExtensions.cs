using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace PhlegmaticOne.MusicPlayer.Contracts.Services;

public static class ViewModelGetExtensions
{
    public static IServiceCollection AddViewModelGetters(this IServiceCollection serviceCollection, Assembly assemblyOfGetters)
    {
        var viewModelGettersTypes = assemblyOfGetters.GetTypes()
            .Where(x => x.IsAssignableTo(typeof(IViewModelGet)) && x.IsAbstract == false && x.IsInterface == false);

        var serviceTypes = new List<Type>();
        foreach (var getterType in viewModelGettersTypes)
        {
            var baseType = getterType.BaseType.GetInterfaces();
            var type = baseType.First();
            serviceTypes.Add(type);
            serviceCollection.AddSingleton(type, getterType);
        }

        serviceCollection.AddSingleton<IDictionary<Type, IViewModelGet>>(x =>
        {
            return serviceTypes.ToDictionary(key => key, value => (IViewModelGet)x.GetRequiredService(value));
        });
        serviceCollection.AddSingleton<IViewModelGetService>(x => new ViewModelGetService(x.GetRequiredService<IDictionary<Type, IViewModelGet>>()));
        return serviceCollection;
    }
}