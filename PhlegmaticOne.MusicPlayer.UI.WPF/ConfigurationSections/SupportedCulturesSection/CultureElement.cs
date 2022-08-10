using System.Configuration;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.ConfigurationSections.SupportedCulturesSection;

public class CultureElement : ConfigurationElement
{
    private const string CULTURE_ELEMENT_NAME_ATTRIBUTE_NAME = "name";

    private const string EMPTY = "";

    [ConfigurationProperty(CULTURE_ELEMENT_NAME_ATTRIBUTE_NAME, DefaultValue = EMPTY, IsRequired = true, IsKey = true)]

    public string Name
    {
        get => (string)base[CULTURE_ELEMENT_NAME_ATTRIBUTE_NAME];
        set => base[CULTURE_ELEMENT_NAME_ATTRIBUTE_NAME] = value;
    }
}