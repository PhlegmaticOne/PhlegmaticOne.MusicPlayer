namespace PhlegmaticOne.MusicPlayer.UI.WPF.Localization;

public class LocalizeValuesGetter : ILocalizeValuesGetter
{
    public string? GetLocalizedValue(string key)
    {
        if (App.CurrentResourceDictionary.Contains(key))
        {
            return (App.CurrentResourceDictionary[key] as string)!;
        }

        return null;
    }
}