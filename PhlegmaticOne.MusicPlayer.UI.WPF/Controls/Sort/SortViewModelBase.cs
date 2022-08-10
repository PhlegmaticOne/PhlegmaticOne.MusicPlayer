using System.Threading.Tasks;
using System.Windows.Input;
using PhlegmaticOne.MusicPlayer.WPF.Core;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.Controls.Sort;

public abstract class SortViewModelBase<T> : BaseViewModel where T: BaseViewModel
{
    protected SortViewModelBase()
    {
        SortCommand = new DelegateCommand(SortAction, _ => true);
    }
    public ICommand SortCommand { get; }
    protected abstract Task SortViewModel(T viewModel);

    private async void SortAction(object? parameter)
    {
        if (parameter is T viewModel)
        {
            await SortViewModel(viewModel);
        }
    }
}