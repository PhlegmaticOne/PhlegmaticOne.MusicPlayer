using System.Collections.ObjectModel;
using PhlegmaticOne.MusicPlayer.Contracts.Base;
using PhlegmaticOne.MusicPlayer.Contracts.ControlViewModels.Reload;
using PhlegmaticOne.MusicPlayer.Contracts.ControlViewModels.Sort;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Player;
using PhlegmaticOne.MusicPlayer.Contracts.Services.UI;
using PhlegmaticOne.MusicPlayer.WPF.Core;

namespace PhlegmaticOne.MusicPlayer.Contracts.ApplicationViewModels.Base;

public abstract class CollectionViewModelBase<TCollectionViewModel, TCollectionItemType> : PlayerTrackableViewModel
    where TCollectionItemType : BaseViewModel, ICollectionItem
    where TCollectionViewModel : CollectionViewModelBase<TCollectionViewModel, TCollectionItemType>
{
    protected readonly IUIThreadInvokerService UiThreadInvokerService;
    public ReloadViewModelBase<TCollectionViewModel> ReloadViewModel { get; }
    public SortViewModelBase<TCollectionViewModel, TCollectionItemType> SortViewModel { get; }
    public ObservableCollection<TCollectionItemType> Items { get; }

    protected CollectionViewModelBase(IPlayerService playerService,
        ReloadViewModelBase<TCollectionViewModel> reloadViewModel,
        SortViewModelBase<TCollectionViewModel, TCollectionItemType> sortViewModelBase,
        IUIThreadInvokerService uiThreadInvokerService) : base(playerService)
    {
        UiThreadInvokerService = uiThreadInvokerService;
        ReloadViewModel = reloadViewModel;
        SortViewModel = sortViewModelBase;
        Items = new();
    }

    internal async Task UpdateItems(IEnumerable<TCollectionItemType> newItems)
    {
        await UiThreadInvokerService.InvokeAsync(() =>
        {
            Items.Clear();
            foreach (var newItem in newItems)
            {
                Items.Add(newItem);
            }
        });
    }
}