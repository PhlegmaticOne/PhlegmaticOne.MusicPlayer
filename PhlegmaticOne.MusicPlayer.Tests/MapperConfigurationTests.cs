using PhlegmaticOne.MusicPlayer.Contracts.MapperConfigurations;

namespace PhlegmaticOne.MusicPlayer.Tests;

public class MapperConfigurationTests
{
    [Fact]
    public void ItShould_Validate_MapperConfiguration()
    {
        var configuration = MapperConfig.GetMapperConfiguration();

        configuration.AssertConfigurationIsValid();
    }
}