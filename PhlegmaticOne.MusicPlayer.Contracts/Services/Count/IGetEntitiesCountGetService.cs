using PhlegmaticOne.WPF.Core.ViewModels;

namespace PhlegmaticOne.MusicPlayer.Contracts.Services.Count;

public interface IGetEntitiesCountGetService<out T> where T : EntityBaseViewModel
{
    Task<int> GetEntitiesCountAsync(Func<T, bool>? selectFunc = null);
    int GetEntitiesCount(Func<T, bool>? selectFunc = null);
}