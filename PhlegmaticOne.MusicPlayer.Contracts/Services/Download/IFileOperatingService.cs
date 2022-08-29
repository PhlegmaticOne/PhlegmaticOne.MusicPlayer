namespace PhlegmaticOne.MusicPlayer.Contracts.Services.Download;

public interface IFileOperatingService<in T>
{
    public Task Download(T entity);
    public Task Delete(T entity);
}