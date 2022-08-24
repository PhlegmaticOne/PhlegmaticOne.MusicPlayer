using PhlegmaticOne.MusicPlayer.WPF.Core.ViewModels;

namespace PhlegmaticOne.MusicPlayer.Contracts.Services.Cache;

public interface ICacheService
{
    public bool ContainsKey<T>(Guid id);
    public bool TryGetCachedValue<T>(Guid id, out T viewModel) where T : BaseViewModel;
    public void Set<T>(Guid id, T viewModel) where T : BaseViewModel;
    public ICollection<T> GetAllCached<T>() where T : BaseViewModel;
}