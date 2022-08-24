using System.Collections.ObjectModel;
using PhlegmaticOne.MusicPlayer.Contracts.ControlViewModels.Reload;
using PhlegmaticOne.MusicPlayer.Contracts.ControlViewModels.Sort;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Player;
using PhlegmaticOne.MusicPlayer.Contracts.Services.UI;
using PhlegmaticOne.MusicPlayer.WPF.Core.ViewModels;
using PhlegmaticOne.WPF.Navigation;

namespace PhlegmaticOne.MusicPlayer.Contracts.ApplicationViewModels.Base;

public abstract class CollectionViewModelBase<TViewModel, TCollectionItemType> : PlayerTrackableViewModel
    where TViewModel : CollectionViewModelBase<TViewModel, TCollectionItemType>
    where TCollectionItemType : EntityBaseViewModel
{
    protected readonly IUIThreadInvokerService UiThreadInvokerService;
    protected readonly IEntityContainingViewModelsNavigationService EntityContainingViewModelsNavigationService;

    protected CollectionViewModelBase(IPlayerService playerService,
        IUIThreadInvokerService uiThreadInvokerService, 
        IEntityContainingViewModelsNavigationService entityContainingViewModelsNavigationService,
        ReloadViewModelBase<TViewModel> reloadViewModel,
        SortViewModelBase<TViewModel, TCollectionItemType> sortViewModel) : base(playerService)
    {
        ReloadViewModel = reloadViewModel;
        SortViewModel = sortViewModel;
        UiThreadInvokerService = uiThreadInvokerService;
        EntityContainingViewModelsNavigationService = entityContainingViewModelsNavigationService;
        Items = new();
    }

    public ObservableCollection<TCollectionItemType> Items { get; }
    public ReloadViewModelBase<TViewModel> ReloadViewModel { get; }
    public SortViewModelBase<TViewModel, TCollectionItemType> SortViewModel { get; }

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