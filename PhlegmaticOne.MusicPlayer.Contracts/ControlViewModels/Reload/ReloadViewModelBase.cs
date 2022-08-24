using PhlegmaticOne.MusicPlayer.Contracts.Services.ViewModelGet;
using PhlegmaticOne.MusicPlayer.WPF.Core.Commands;
using PhlegmaticOne.MusicPlayer.WPF.Core.ViewModels;

namespace PhlegmaticOne.MusicPlayer.Contracts.ControlViewModels.Reload;

public abstract class ReloadViewModelBase<TViewModel> : BaseViewModel where TViewModel : ApplicationBaseViewModel
{
    protected readonly IEntityCollectionGetService EntityCollectionGetService;

    protected ReloadViewModelBase(IEntityCollectionGetService entityCollectionGetService)
    {
        EntityCollectionGetService = entityCollectionGetService;
        ReloadCommand = DelegateCommandFactory.CreateCommand(ReloadAction, _ => true);
    }
    public IDelegateCommand ReloadCommand { get; }
    protected abstract Task ReloadViewModel(TViewModel viewModel);

    private async void ReloadAction(object? parameter)
    {
        if (parameter is TViewModel viewModel)
        {
            await Task.Run(async () =>
            {
                await ReloadViewModel(viewModel);
            });
        }
    }
}