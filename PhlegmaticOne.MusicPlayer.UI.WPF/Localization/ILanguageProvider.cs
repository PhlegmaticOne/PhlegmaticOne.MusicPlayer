using System.Collections.Generic;
using System.Globalization;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.Localization;

public interface ILanguageProvider
{
    public CultureInfo CurrentCulture { get; }
    public IEnumerable<CultureInfo> GetSupportedLanguages();
    public bool SetLanguage(string languageName);
}