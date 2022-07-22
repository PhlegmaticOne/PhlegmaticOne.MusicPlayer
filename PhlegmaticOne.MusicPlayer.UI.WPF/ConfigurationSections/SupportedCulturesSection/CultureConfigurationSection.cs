using System.Configuration;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.ConfigurationSections.SupportedCulturesSection;

public class CultureConfigurationSection : ConfigurationSection
{

    private const string CONFIGURATION_SECTION_NAME = "cultures";
    [ConfigurationProperty(CONFIGURATION_SECTION_NAME)]

    public CultureCollection CultureCollection =>
        (CultureCollection)base[CONFIGURATION_SECTION_NAME];
}