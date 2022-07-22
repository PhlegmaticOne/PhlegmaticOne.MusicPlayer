using System.Configuration;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.ConfigurationSections.SupportedCulturesSection;

[ConfigurationCollection(typeof(CultureElement), AddItemName = SectionItemsName.Instance)]
public class CultureCollection : ConfigurationElementCollection
{
    protected override ConfigurationElement CreateNewElement() => new CultureElement();

    protected override object GetElementKey(ConfigurationElement element) => (CultureElement)element;

    public CultureElement this[int idx] => (CultureElement)BaseGet(idx);

    private static class SectionItemsName
    {
        internal const string Instance = "culture";
    }
}

