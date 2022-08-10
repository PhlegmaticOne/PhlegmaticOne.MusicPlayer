using PhlegmaticOne.MusicPlayer.UI.WPF.Properties;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.DownloadConfiguration;

public class DownloadSettings : IDownloadSettings
{
    private readonly string _defaultDirectoryPath = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
    public string DownloadDirectoryPath { get; private set; }

    public DownloadSettings()
    {
        var path = Settings.Default.DownloadDirectoryPath;
        if (Directory.Exists(path) == false)
        {
            path = _defaultDirectoryPath;
            SavePath(path);
        }
        DownloadDirectoryPath = path;
    }
    public void SetNewDirectoryPath(string newDirectoryPath)
    {
        if (Directory.Exists(newDirectoryPath))
        {
            SavePath(newDirectoryPath);
        }
        DownloadDirectoryPath = newDirectoryPath;
    }


    public async Task<long> GetDirectorySizeAsync()
    {
        var dirInfo = new DirectoryInfo(DownloadDirectoryPath);
        var dirSize = await Task.Run(() => dirInfo.EnumerateFiles("*", SearchOption.AllDirectories).Sum(file => file.Length));
        return dirSize;
    }

    public async Task DeleteTracksAsync()
    {
        await Task.Run(() =>
        {
            foreach (var enumerateFile in Directory.EnumerateFiles(DownloadDirectoryPath))
            {
                File.Delete(enumerateFile);
            }
            foreach (var enumerateDirectory in Directory.EnumerateDirectories(DownloadDirectoryPath))
            {
                Directory.Delete(enumerateDirectory, true);
            }
        });
    }

    private void SavePath(string path)
    {
        Settings.Default.DownloadDirectoryPath = path;
        Settings.Default.Save();
    }
}