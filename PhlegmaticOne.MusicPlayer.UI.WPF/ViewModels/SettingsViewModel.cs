using PhlegmaticOne.MusicPlayer.UI.WPF.DownloadConfiguration;
using PhlegmaticOne.MusicPlayer.WPF.Core;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using PhlegmaticOne.MusicPlayer.UI.WPF.Services;
using PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels.Base;
using PhlegmaticOne.MusicPlayer.UI.WPF.Helpers;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels;

public class SettingsViewModel : ApplicationBaseViewModel
{
    private readonly ILocalizationService _localizationService;
    private readonly IDownloadSettings _downloadSettings;
    public ObservableCollection<DisplayCultureInfo> SupportedLanguages { get; set; } = new();
    public string CurrentLanguageName { get; set; }
    public long DirectorySize { get; set; }
    public string CurrentDownloadDirectoryFolderPath { get; set; }
    public SettingsViewModel(ILocalizationService localizationService, IDownloadSettings downloadSettings)
    {
        _localizationService = localizationService;
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

        if (_localizationService.SetLanguage(language))
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
        foreach (var supportedLanguage in _localizationService.GetSupportedCultures())
        {
            SupportedLanguages.Add(new DisplayCultureInfo(supportedLanguage.DisplayName, supportedLanguage.Name));
        }
        var osSettingsText = _localizationService.GetLocalizedValue("OsBasedLanguageSelectText")!;
        SupportedLanguages.Add(new DisplayCultureInfo(osSettingsText, "OS"));
        CurrentLanguageName = _localizationService.CurrentCulture.DisplayName;
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