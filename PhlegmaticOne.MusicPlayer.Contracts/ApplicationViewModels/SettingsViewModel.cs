using System.Collections.ObjectModel;
using System.Windows.Forms;
using PhlegmaticOne.MusicPlayer.Contracts.Helpers;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Download;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Localization;
using PhlegmaticOne.MusicPlayer.WPF.Core.Commands;
using PhlegmaticOne.MusicPlayer.WPF.Core.ViewModels;

namespace PhlegmaticOne.MusicPlayer.Contracts.ApplicationViewModels;

public class SettingsViewModel : ApplicationBaseViewModel
{
    private readonly ILocalizationService _localizationService;
    private readonly ILocalSystemSettings _downloadSettings;
    public ObservableCollection<DisplayCultureInfo> SupportedLanguages { get; set; } = new();
    public string CurrentLanguageName { get; set; }
    public long DirectorySize { get; set; }
    public string CurrentDownloadDirectoryFolderPath { get; set; }
    public SettingsViewModel(ILocalizationService localizationService, ILocalSystemSettings downloadSettings)
    {
        _localizationService = localizationService;
        _downloadSettings = downloadSettings;

        UpdateDirectoryMenu();
        UpdateLanguageMenu();

        ChangeLanguageCommand = DelegateCommandFactory.CreateCommand(ChangeLanguage, _ => true);
        SetNewDownloadDirectoryPathCommand = DelegateCommandFactory.CreateCommand(SetNewDownloadDirectory, _ => true);
        DeleteTracksFromDeviceCommand = DelegateCommandFactory.CreateCommand(DeleteTracksFromDevice, _ => DirectorySize > 0);
    }
    public IDelegateCommand ChangeLanguageCommand { get; set; }
    public IDelegateCommand SetNewDownloadDirectoryPathCommand { get; set; }
    public IDelegateCommand DeleteTracksFromDeviceCommand { get; set; }

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