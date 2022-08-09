using System.Windows.Input;
using PhlegmaticOne.MusicPlayer.UI.WPF.Commands;
using PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels.Base;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.Navigation;

public abstract class MusicNavigationBase<T> where T: class
{
    private readonly INavigator _navigator;

    protected MusicNavigationBase(INavigator navigator)
    {
        _navigator = navigator;
        NavigateToMusicCommand = new DelegateCommand(Navigate, _ => true);
    }
    public ICommand NavigateToMusicCommand { get; set; }
    protected abstract BaseViewModel CreateViewModel(T entity);

    private void Navigate(object? parameter)
    {
        var viewModel = CreateViewModel(parameter as T);
        _navigator.NavigateTo(viewModel);
    }
}