using System.Reflection;
using AutoMapper;

namespace PhlegmaticOne.MusicPlayer.Contracts.MapperConfigurations;

public class MapperConfig
{
    public static MapperConfiguration GetMapperConfiguration()
    {
        var profiles = GetProfiles();
        return new MapperConfiguration(cfg =>
        {
            foreach (var profile in profiles.Select(profile => (Profile)Activator.CreateInstance(profile)!))
            {
                cfg.AddProfile(profile);
            }
        });
    }

    private static List<Type> GetProfiles()
    {
        return (from t in typeof(MapperConfig).GetTypeInfo().Assembly.GetTypes()
            where typeof(Profile).IsAssignableFrom(t) && !t.GetTypeInfo().IsAbstract
            select t).ToList();

    }
}