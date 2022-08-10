using System.Threading.Tasks;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.Services;

public interface IDownloadService<in T>
{
    public Task Download(T entity);
}