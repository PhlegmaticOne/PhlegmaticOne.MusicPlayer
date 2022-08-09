using System.Collections.ObjectModel;
using System.Windows.Forms;
using PhlegmaticOne.MusicPlayer.UI.WPF.DownloadConfiguration;
using PhlegmaticOne.MusicPlayer.UI.WPF.LanguagesSettings;
using PhlegmaticOne.MusicPlayer.UI.WPF.Localization;
using PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels.Base;
using PhlegmaticOne.MusicPlayer.WPF.Core;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels;

public class SettingsViewModel : BaseViewModel
{
    private readonly ILanguageProvider _languageProvider;
    private readonly ILocalizeValuesGetter _localizeValuesGetter;
    private readonly IDownloadSettings _downloadSettings;
    public ObservableCollection<DisplayCultureInfo> SupportedLanguages { get; set; } = new();
    public string CurrentLanguageName { get; set; }
    public long DirectorySize { get; set; }
    public string CurrentDownloadDirectoryFolderPath { get; set; }
    public SettingsViewModel(ILanguageProvider languageProvider, ILocalizeValuesGetter localizeValuesGetter, IDownloadSettings downloadSettings)
    {
        _languageProvider = languageProvider;
        _localizeValuesGetter = localizeValuesGetter;
        _downloadSettings = downloadSettings;

        UpdateDirectoryMenu();
        UpdateLanguageMenu();

        ChangeLanguageCommand = new(ChangeLanguage, _ => true);
        SetNewDownloadDirectoryPathCommand = new(SetNewDownloadDirectory, _ => true);
        DeleteTracksFromDeviceCommand = new(DeleteTracksFromDevice, _ => DirectorySize > 0);
    }
    public DelegateCommand ChangeLanguageCommand { get; set; }
    public DelegateCommand SetNewDownloadDirectoryPathCommand { get; set; }
    public DelegateCommand DeleteTracksFromDeviceCommand { get; set; }

    private void ChangeLanguage(object? parameter)
    {
        if (parameter is not string language) return;

        if (_languageProvider.SetLanguage(language))
        {
            UpdateLanguageMenu();
        }
    }

    private void SetNewDownloadDirectory(object? parameter)
    {
        using var dialog = new FolderBrowserDialog();
        if (dialog.ShowDialog() == DialogResult.OK)
        {
            var directoryPath = dialog.SelectedPath;
            _downloadSettings.SetNewDirectoryPath(directoryPath);
        }
        UpdateDirectoryMenu();
    }
    private async void DeleteTracksFromDevice(object? parameter)
    {
        await _downloadSettings.DeleteTracksAsync();
        UpdateDirectoryMenu();
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
        CurrentLanguageName = _languageProvider.CurrentCulture.DisplayName;
    }

    private void UpdateDirectoryMenu()
    {
        CurrentDownloadDirectoryFolderPath = _downloadSettings.DownloadDirectoryPath;
        SetDirectorySize();
    }

    private async void SetDirectorySize()
    {
        var size = await _downloadSettings.GetDirectorySizeAsync();
        DirectorySize = (size / 1000000);
        DeleteTracksFromDeviceCommand.RaiseCanExecute();
    }
}