using System.Threading.Tasks;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.DownloadConfiguration;

public interface IDownloadSettings
{
    public string DownloadDirectoryPath { get; }
    public void SetNewDirectoryPath(string newDirectoryPath);
    public Task<long> GetDirectorySizeAsync();
    public Task DeleteTracksAsync();
}