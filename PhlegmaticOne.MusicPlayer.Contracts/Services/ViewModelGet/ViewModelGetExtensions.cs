using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace PhlegmaticOne.MusicPlayer.Contracts.Services.ViewModelGet;

public static class ViewModelGetExtensions
{
    public static IServiceCollection AddViewModelGetters(this IServiceCollection serviceCollection, Assembly assemblyOfGetters)
    {
        var viewModelGettersTypes = assemblyOfGetters.GetTypes()
            .Where(x => x.IsAssignableTo(typeof(IEntityCollectionGet)) && x.IsAbstract == false && x.IsInterface == false);
        return AddEntityCollectionGetterTypes(serviceCollection, viewModelGettersTypes);
    }

    public static IServiceCollection AddEntityCollectionGetterTypes(this IServiceCollection serviceCollection, 
        params Type[] entityCollectionGets) =>
        AddEntityCollectionGetterTypes(serviceCollection, (IEnumerable<Type>) entityCollectionGets);

    private static IServiceCollection AddEntityCollectionGetterTypes(IServiceCollection serviceCollection, IEnumerable<Type> types)
    {
        var serviceTypes = new List<Type>();
        foreach (var getterType in types)
        {
            var baseType = getterType.GetInterfaces();
            var type = baseType.First();
            serviceTypes.Add(type);
            serviceCollection.AddSingleton(type, getterType);
        }
        serviceCollection.AddSingleton<IDictionary<Type, IEntityCollectionGet>>(x =>
        {
            return serviceTypes.ToDictionary(key => key, value => (IEntityCollectionGet)x.GetRequiredService(value));
        });
        serviceCollection.AddSingleton<IEntityCollectionGetService>(x => new ViewModelGetService(x.GetRequiredService<IDictionary<Type, IEntityCollectionGet>>()));
        return serviceCollection;
    }
}