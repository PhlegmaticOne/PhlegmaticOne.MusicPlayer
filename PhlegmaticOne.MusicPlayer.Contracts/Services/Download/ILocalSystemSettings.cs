namespace PhlegmaticOne.MusicPlayer.Contracts.Services.Download;

public interface ILocalSystemSettings
{
    public string DownloadDirectoryPath { get; }
    public void SetNewDirectoryPath(string newDirectoryPath);
    public Task<long> GetDirectorySizeAsync();
    public Task DeleteTracksAsync();
}