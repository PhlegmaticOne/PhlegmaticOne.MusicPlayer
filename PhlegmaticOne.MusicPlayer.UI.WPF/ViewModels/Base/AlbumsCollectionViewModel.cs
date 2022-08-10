using System;
using System.Collections.Generic;
using PhlegmaticOne.MusicPlayer.Contracts.ViewModels;
using PhlegmaticOne.MusicPlayer.UI.WPF.Controls.Reload;
using PhlegmaticOne.MusicPlayer.UI.WPF.Controls.Sort;
using PhlegmaticOne.MusicPlayer.UI.WPF.Navigation;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels.Base;

public class AlbumsCollectionViewModel : CollectionViewModelBase<AlbumEntityViewModel, AlbumsCollectionViewModel>
{
    public AlbumsCollectionViewModel(ReloadViewModelBase<AlbumsCollectionViewModel> reloadViewModel,
        SortViewModelBase<AlbumsCollectionViewModel> sortViewModelBase,
        MusicNavigationBase<AlbumEntityViewModel> musicNavigationBase) :
        base(reloadViewModel, sortViewModelBase, musicNavigationBase)
    {
    }

    protected override Dictionary<string, Func<AlbumEntityViewModel, object>> GetSupportedSorts()
    {
        throw new NotImplementedException();
    }
}