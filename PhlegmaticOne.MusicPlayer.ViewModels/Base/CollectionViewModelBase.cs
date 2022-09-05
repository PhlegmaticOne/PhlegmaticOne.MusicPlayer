using System.Collections.ObjectModel;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Like;
using PhlegmaticOne.MusicPlayer.Models.Base;
using PhlegmaticOne.MusicPlayer.ViewModels.PagedList;
using PhlegmaticOne.PlayerService.Base;
using PhlegmaticOne.WPF.Core.ViewModels;
using PhlegmaticOne.WPF.Navigation;

namespace PhlegmaticOne.MusicPlayer.ViewModels.Base;

public abstract class CollectionViewModelBase<TViewModel, TCollectionItemType> : PlayerTrackableViewModel
    where TViewModel : CollectionViewModelBase<TViewModel, TCollectionItemType>
    where TCollectionItemType : EntityBaseViewModel
{
    protected readonly IEntityContainingViewModelsNavigationService EntityContainingViewModelsNavigationService;

    protected CollectionViewModelBase(IPlayerService<TrackBaseViewModel> playerService, ILikeService likeService,
        IEntityContainingViewModelsNavigationService entityContainingViewModelsNavigationService,
        PagedListViewModelBase<TCollectionItemType> pagedListViewModel) : base(playerService, likeService)
    {
        PagedListViewModel = pagedListViewModel;
        EntityContainingViewModelsNavigationService = entityContainingViewModelsNavigationService;
        Items = new();
    }

    public ObservableCollection<TCollectionItemType> Items { get; }
    public PagedListViewModelBase<TCollectionItemType> PagedListViewModel { get; }
}