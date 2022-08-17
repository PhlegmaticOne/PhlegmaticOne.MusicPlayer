using System.Globalization;

namespace PhlegmaticOne.MusicPlayer.Contracts.Services.Localization;

public interface ILanguageProvider
{
    public CultureInfo CurrentCulture { get; }
    public IEnumerable<CultureInfo> GetSupportedLanguages();
    public bool SetLanguage(string languageName);
}