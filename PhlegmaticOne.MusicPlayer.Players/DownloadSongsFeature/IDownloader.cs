namespace PhlegmaticOne.MusicPlayer.Players.DownloadSongsFeature;

public interface IDownloader
{
    public Task DownloadAsync(string downloadingFilePath, string localFolderPathToSave, string fileName);
}