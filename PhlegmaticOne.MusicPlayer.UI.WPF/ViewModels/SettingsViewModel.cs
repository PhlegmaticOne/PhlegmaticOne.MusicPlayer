using System.Collections.ObjectModel;
using PhlegmaticOne.MusicPlayer.UI.WPF.Commands;
using PhlegmaticOne.MusicPlayer.UI.WPF.LanguagesSettings;
using PhlegmaticOne.MusicPlayer.UI.WPF.Localization;
using PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels.Base;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels;

public class SettingsViewModel : BaseViewModel
{
    private readonly ILanguageProvider _languageProvider;
    private readonly ILocalizeValuesGetter _localizeValuesGetter;
    public ObservableCollection<DisplayCultureInfo> SupportedLanguages { get; set; } = new();
    public SettingsViewModel(ILanguageProvider languageProvider, ILocalizeValuesGetter localizeValuesGetter)
    {
        _languageProvider = languageProvider;
        _localizeValuesGetter = localizeValuesGetter;
        UpdateLanguageMenu();
        ChangeLanguageCommand = new(ChangeLanguage, _ => true);
    }
    public DelegateCommand ChangeLanguageCommand { get; set; }

    private void ChangeLanguage(object? parameter)
    {
        if (parameter is not string language) return;

        if (_languageProvider.SetLanguage(language))
        {
            UpdateLanguageMenu();
        }
    }

    private void UpdateLanguageMenu()
    {
        SupportedLanguages.Clear();
        foreach (var supportedLanguage in _languageProvider.GetSupportedLanguages())
        {
            SupportedLanguages.Add(new DisplayCultureInfo(supportedLanguage.DisplayName, supportedLanguage.Name));
        }
        var osSettingsText = _localizeValuesGetter.GetLocalizedValue("OsBasedLanguageSelectText")!;
        SupportedLanguages.Add(new DisplayCultureInfo(osSettingsText, "OS"));
    }
}