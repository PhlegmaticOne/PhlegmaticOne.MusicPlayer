namespace PhlegmaticOne.MusicPlayer.Contracts.Services.Download;

public interface IDownloadService<in T>
{
    public Task Download(T entity);
}