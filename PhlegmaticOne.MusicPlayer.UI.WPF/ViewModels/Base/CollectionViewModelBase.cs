using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using PhlegmaticOne.MusicPlayer.Contracts.Base;
using PhlegmaticOne.MusicPlayer.UI.WPF.Controls.Reload;
using PhlegmaticOne.MusicPlayer.UI.WPF.Controls.Sort;
using PhlegmaticOne.MusicPlayer.UI.WPF.Navigation;
using PhlegmaticOne.MusicPlayer.WPF.Core;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels.Base;

public abstract class CollectionViewModelBase<TCollectionItemType, TCollectionViewModel> : BaseViewModel
    where TCollectionItemType: BaseViewModel, ICollectionItem
    where TCollectionViewModel : CollectionViewModelBase<TCollectionItemType, TCollectionViewModel>
{
    protected readonly ReloadViewModelBase<TCollectionViewModel> _reloadViewModel;
    protected readonly SortViewModelBase<TCollectionViewModel> _sortViewModelBase;
    protected readonly MusicNavigationBase<TCollectionItemType> _musicNavigationBase;
    public ObservableCollection<TCollectionItemType> Items { get; private set; }

    protected CollectionViewModelBase(ReloadViewModelBase<TCollectionViewModel> reloadViewModel,
        SortViewModelBase<TCollectionViewModel> sortViewModelBase, 
        MusicNavigationBase<TCollectionItemType> musicNavigationBase)
    {
        _reloadViewModel = reloadViewModel;
        _sortViewModelBase = sortViewModelBase;
        _musicNavigationBase = musicNavigationBase;
    }

    protected abstract Dictionary<string, Func<TCollectionItemType, object>> GetSupportedSorts();
}