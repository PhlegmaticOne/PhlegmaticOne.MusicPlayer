namespace PhlegmaticOne.MusicPlayer.Players.DownloadSongsFeature;

public class HttpDownloader : IDownloader
{
    public async Task DownloadAsync(string downloadingFilePath, string localFolderPathToSave, string fileName)
    {
        var httpClient = HttpClientSingleton.Instance;
        var songBytes = await httpClient.GetByteArrayAsync(downloadingFilePath);
        if (Directory.Exists(localFolderPathToSave) == false)
        {
            Directory.CreateDirectory(localFolderPathToSave);
        }
        var fullPath = Path.Combine(localFolderPathToSave, fileName);
        await File.WriteAllBytesAsync(fullPath, songBytes);
    }
}