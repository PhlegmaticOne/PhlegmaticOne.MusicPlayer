namespace PhlegmaticOne.MusicPlayer.Data.EntityFramework.Other.DownloadSongsFeature;

public interface IDownloader
{
    public Task DownloadAsync(string downloadingFilePath, string localFolderPathToSave, string fileName);
}