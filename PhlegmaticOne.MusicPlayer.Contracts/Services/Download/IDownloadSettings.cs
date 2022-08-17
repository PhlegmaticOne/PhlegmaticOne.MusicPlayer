namespace PhlegmaticOne.MusicPlayer.Contracts.Services.Download;

public interface IDownloadSettings
{
    public string DownloadDirectoryPath { get; }
    public void SetNewDirectoryPath(string newDirectoryPath);
    public Task<long> GetDirectorySizeAsync();
    public Task DeleteTracksAsync();
}