using System.Threading.Tasks;
using PhlegmaticOne.MusicPlayer.Contracts.ViewModels.Base;
using PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels.Base;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.ViewModelsFactories.Application;

public interface IMusicViewModelsFactory<in TFrom, TTo>
    where TFrom : EntityBaseViewModel
    where TTo : ApplicationBaseViewModel
{
    public Task<TTo> CreateViewModelAsync(TFrom entity);
}