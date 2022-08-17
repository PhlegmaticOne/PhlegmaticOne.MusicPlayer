using PhlegmaticOne.MusicPlayer.Contracts.ViewModels.Base;

namespace PhlegmaticOne.MusicPlayer.Contracts.Services.ViewModelGet;

public interface IViewModelGetService
{
    public Task<T> GetViewModelAsync<T>(Guid id) where T : EntityBaseViewModel;
}