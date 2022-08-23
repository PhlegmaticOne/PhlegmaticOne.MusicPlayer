using PhlegmaticOne.MusicPlayer.WPF.Core;

namespace PhlegmaticOne.WPF.Navigation;

public interface IEntityContainingViewModelsNavigationService
{
    IEntityContainingViewModelNavigation<TFrom, TTo> From<TFrom, TTo>()
        where TFrom : EntityBaseViewModel
        where TTo : EntityBaseViewModel;
}