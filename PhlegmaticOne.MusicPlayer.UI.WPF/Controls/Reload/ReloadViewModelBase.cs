using PhlegmaticOne.MusicPlayer.WPF.Core;
using System.Threading.Tasks;
using System.Windows.Input;
using PhlegmaticOne.MusicPlayer.Contracts.Services;
using PhlegmaticOne.MusicPlayer.Data.Context;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.Controls.Reload;

public abstract class ReloadViewModelBase<T> : BaseViewModel where T : BaseViewModel
{
    protected readonly ApplicationDbContext DbContext;
    protected readonly IViewModelGetService ViewModelGetService;

    protected ReloadViewModelBase(ApplicationDbContext dbContext, IViewModelGetService viewModelGetService)
    {
        DbContext = dbContext;
        ViewModelGetService = viewModelGetService;
        ReloadCommand = new DelegateCommand(ReloadAction, _ => true);
    }
    public ICommand ReloadCommand { get; }
    protected abstract Task ReloadViewModel(T viewModel);

    private async void ReloadAction(object? parameter)
    {
        if (parameter is T viewModel)
        {
            await ReloadViewModel(viewModel);
        }
    }
}