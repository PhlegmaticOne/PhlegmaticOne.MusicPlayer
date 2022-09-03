using System.Collections.ObjectModel;
using PhlegmaticOne.MusicPlayer.Contracts.Models.Base;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Like;
using PhlegmaticOne.MusicPlayer.Contracts.Services.UI;
using PhlegmaticOne.MusicPlayer.ViewModels.ControlViewModels.PagedList;
using PhlegmaticOne.MusicPlayer.ViewModels.ControlViewModels.Reload;
using PhlegmaticOne.MusicPlayer.ViewModels.ControlViewModels.Sort;
using PhlegmaticOne.PlayerService.Base;
using PhlegmaticOne.WPF.Core.ViewModels;
using PhlegmaticOne.WPF.Navigation;

namespace PhlegmaticOne.MusicPlayer.ViewModels.Base;

public abstract class CollectionViewModelBase<TViewModel, TCollectionItemType> : PlayerTrackableViewModel
    where TViewModel : CollectionViewModelBase<TViewModel, TCollectionItemType>
    where TCollectionItemType : EntityBaseViewModel
{
    protected readonly IUiThreadInvokerService UiThreadInvokerService;
    protected readonly IEntityContainingViewModelsNavigationService EntityContainingViewModelsNavigationService;

    protected CollectionViewModelBase(IPlayerService<TrackBaseViewModel> playerService, 
        ILikeService likeService,
        IUiThreadInvokerService uiThreadInvokerService, 
        IEntityContainingViewModelsNavigationService entityContainingViewModelsNavigationService,
        ReloadViewModelBase<TViewModel> reloadViewModel,
        SortViewModelBase<TViewModel, TCollectionItemType> sortViewModel,
        PagedListViewModelBase<TCollectionItemType> pagedListViewModel) : base(playerService, likeService)
    {
        ReloadViewModel = reloadViewModel;
        SortViewModel = sortViewModel;
        PagedListViewModel = pagedListViewModel;
        UiThreadInvokerService = uiThreadInvokerService;
        EntityContainingViewModelsNavigationService = entityContainingViewModelsNavigationService;
        Items = new();
    }

    public ObservableCollection<TCollectionItemType> Items { get; }
    public ReloadViewModelBase<TViewModel> ReloadViewModel { get; }
    public SortViewModelBase<TViewModel, TCollectionItemType> SortViewModel { get; }
    public PagedListViewModelBase<TCollectionItemType> PagedListViewModel { get; }

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