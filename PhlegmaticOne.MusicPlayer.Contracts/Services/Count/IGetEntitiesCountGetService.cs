using PhlegmaticOne.WPF.Core.ViewModels;

namespace PhlegmaticOne.MusicPlayer.Contracts.Services.Count;

public interface IGetEntitiesCountGetService<T> where T : EntityBaseViewModel
{
    Task<int> GetEntitiesCountAsync();
    int GetEntitiesCount();
}