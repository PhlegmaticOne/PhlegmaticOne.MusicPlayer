using System.Globalization;

namespace PhlegmaticOne.MusicPlayer.Contracts.Services.Localization;

public interface ILocalizationService
{
    public bool SetLanguage(string language);
    public CultureInfo CurrentCulture { get; }
    public IEnumerable<CultureInfo> GetSupportedCultures();
    public string? GetLocalizedValue(string key);
    public event EventHandler LanguageChanged;
}