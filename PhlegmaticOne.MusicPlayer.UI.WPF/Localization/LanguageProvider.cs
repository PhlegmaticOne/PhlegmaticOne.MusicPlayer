using PhlegmaticOne.MusicPlayer.UI.WPF.ConfigurationSections.SupportedCulturesSection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.Localization;

public class LanguageProvider : ILanguageProvider
{
    private readonly List<CultureInfo> _languages;
    public LanguageProvider()
    {
        _languages = new();
        var supportedCultures = (CultureConfigurationSection)ConfigurationManager.GetSection("supportedCultures");
        foreach (CultureElement cultureElement in supportedCultures.CultureCollection)
        {
            _languages.Add(new CultureInfo(cultureElement.Name));
        }
    }

    public CultureInfo CurrentCulture => App.Language;
    public IEnumerable<CultureInfo> GetSupportedLanguages() => _languages;

    public bool SetLanguage(string languageName)
    {
        if (languageName == "OS")
        {
            var osLanguage = CultureInfo.InstalledUICulture;
            var osCultureName = osLanguage.Name;
            var osCultureInfo = _languages.FirstOrDefault(c => c.Name.Equals(osCultureName, StringComparison.InvariantCultureIgnoreCase));
            if (osCultureInfo is not null && App.Language.Name != osCultureName)
            {
                App.Language = osCultureInfo;
                return true;
            }
            return false;
        }
        var cultureInfo = new CultureInfo(languageName);
        var existingCultureInfo = _languages.FirstOrDefault(c => c.Name.Equals(cultureInfo.Name, StringComparison.InvariantCultureIgnoreCase));
        if (existingCultureInfo is not null && App.Language.Name != existingCultureInfo.Name)
        {
            App.Language = existingCultureInfo;
            return true;
        }

        return false;
    }
}