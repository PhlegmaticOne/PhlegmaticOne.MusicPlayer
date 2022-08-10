using System.Threading.Tasks;
using System.Windows.Input;
using PhlegmaticOne.MusicPlayer.WPF.Core;

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
    protected abstract Task<BaseViewModel> CreateViewModel(T entity);

    private async void Navigate(object? parameter)
    {
        var viewModel = await CreateViewModel(parameter as T);
        _navigator.NavigateTo(viewModel);
    }
}