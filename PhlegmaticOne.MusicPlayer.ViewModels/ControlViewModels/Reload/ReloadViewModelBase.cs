using PhlegmaticOne.MusicPlayer.Contracts.Services.PagedList;
using PhlegmaticOne.WPF.Core.Commands;
using PhlegmaticOne.WPF.Core.ViewModels;

namespace PhlegmaticOne.MusicPlayer.ViewModels.ControlViewModels.Reload;

public abstract class ReloadViewModelBase<TViewModel> : BaseViewModel
    where TViewModel : ApplicationBaseViewModel
{
    protected readonly IEntityPagedListGetService EntityCollectionGetService;

    protected ReloadViewModelBase(IEntityPagedListGetService entityCollectionGetService)
    {
        EntityCollectionGetService = entityCollectionGetService;
        ReloadCommand = RelayCommandFactory.CreateCommand(ReloadAction, _ => true);
    }
    public IRelayCommand ReloadCommand { get; }
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