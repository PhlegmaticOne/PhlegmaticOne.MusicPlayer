using System;
using System.Collections.Generic;
using System.Globalization;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Localization;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.Services.Localization;

public class LocalizationService : ILocalizationService
{
    private readonly ILanguageProvider _languageProvider;
    private readonly ILocalizeValuesGetter _localizeValuesGetter;

    public LocalizationService(ILanguageProvider languageProvider, ILocalizeValuesGetter localizeValuesGetter)
    {
        _languageProvider = languageProvider;
        _localizeValuesGetter = localizeValuesGetter;
    }
    public bool SetLanguage(string language)
    {
        var isSet = _languageProvider.SetLanguage(language);
        LanguageChanged?.Invoke(this, EventArgs.Empty);
        return isSet;
    }

    public CultureInfo CurrentCulture => _languageProvider.CurrentCulture;
    public IEnumerable<CultureInfo> GetSupportedCultures() => _languageProvider.GetSupportedLanguages();

    public string? GetLocalizedValue(string key) => _localizeValuesGetter.GetLocalizedValue(key);

    public event EventHandler? LanguageChanged;
}