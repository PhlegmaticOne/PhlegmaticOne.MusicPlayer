using PhlegmaticOne.MusicPlayer.Contracts.ApplicationViewModels.Base;
using PhlegmaticOne.MusicPlayer.Contracts.ViewModels.Base;

namespace PhlegmaticOne.MusicPlayer.Contracts.Services.Navigation.ViewModelFactories;

public interface IMusicViewModelsFactory<in TFrom, TTo>
    where TFrom : EntityBaseViewModel
    where TTo : ApplicationBaseViewModel
{
    public Task<TTo> CreateViewModelAsync(TFrom entity);
}