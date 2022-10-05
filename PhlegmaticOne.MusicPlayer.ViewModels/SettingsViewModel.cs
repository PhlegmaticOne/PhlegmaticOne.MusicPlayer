using System.Collections.ObjectModel;
using System.Windows.Forms;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Download;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Localization;
using PhlegmaticOne.MusicPlayer.ViewModels.Helpers;
using PhlegmaticOne.WPF.Core.Commands;
using PhlegmaticOne.WPF.Core.ViewModels;

namespace PhlegmaticOne.MusicPlayer.ViewModels;

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

        ChangeLanguageCommand = RelayCommandFactory.CreateRequiredParameterCommand<string>(ChangeLanguage);
        SetNewDownloadDirectoryPathCommand = RelayCommandFactory.CreateEmptyCommand(SetNewDownloadDirectory);
        DeleteTracksFromDeviceCommand = RelayCommandFactory.CreateEmptyAsyncCommand(DeleteTracksFromDevice, _ => DirectorySize > 0);
    }
    public IRelayCommand ChangeLanguageCommand { get; set; }
    public IRelayCommand SetNewDownloadDirectoryPathCommand { get; set; }
    public IRelayCommand DeleteTracksFromDeviceCommand { get; set; }

    private void ChangeLanguage(string language)
    {
        if (_localizationService.SetLanguage(language))
        {
            UpdateLanguageMenu();
        }
    }

    private void SetNewDownloadDirectory()
    {
        using var dialog = new FolderBrowserDialog();
        if (dialog.ShowDialog() == DialogResult.OK)
        {
            var directoryPath = dialog.SelectedPath;
            _downloadSettings.SetNewDirectoryPath(directoryPath);
        }
        UpdateDirectoryMenu();
    }
    private async Task DeleteTracksFromDevice()
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