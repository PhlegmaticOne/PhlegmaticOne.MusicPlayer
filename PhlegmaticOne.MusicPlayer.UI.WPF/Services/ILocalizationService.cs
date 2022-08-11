using System;
using System.Collections.Generic;
using System.Globalization;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.Services;

public interface ILocalizationService
{
    public bool SetLanguage(string language);
    public CultureInfo CurrentCulture { get; }
    public IEnumerable<CultureInfo> GetSupportedCultures();
    public string? GetLocalizedValue(string key);
    public event EventHandler LanguageChanged;
}