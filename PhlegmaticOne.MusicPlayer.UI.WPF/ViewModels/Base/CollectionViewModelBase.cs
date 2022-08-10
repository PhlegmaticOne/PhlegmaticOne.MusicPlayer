using PhlegmaticOne.MusicPlayer.Contracts.Base;
using PhlegmaticOne.MusicPlayer.UI.WPF.Controls.Reload;
using PhlegmaticOne.MusicPlayer.UI.WPF.Controls.Sort;
using PhlegmaticOne.MusicPlayer.UI.WPF.Navigation;
using PhlegmaticOne.MusicPlayer.UI.WPF.Services;
using PhlegmaticOne.MusicPlayer.WPF.Core;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels.Base;

public abstract class CollectionViewModelBase<TCollectionItemType, TCollectionViewModel> : PlayerTrackableViewModel
    where TCollectionItemType : BaseViewModel, ICollectionItem
    where TCollectionViewModel : CollectionViewModelBase<TCollectionItemType, TCollectionViewModel>
{
    public ReloadViewModelBase<TCollectionViewModel> ReloadViewModel { get; }
    public SortViewModelBase<TCollectionViewModel, TCollectionItemType> SortViewModel { get; }
    public MusicNavigationBase<TCollectionItemType> MusicNavigation { get; }
    public ObservableCollection<TCollectionItemType> Items { get; }

    protected CollectionViewModelBase(ReloadViewModelBase<TCollectionViewModel> reloadViewModel,
        SortViewModelBase<TCollectionViewModel, TCollectionItemType> sortViewModelBase,
        MusicNavigationBase<TCollectionItemType> musicNavigationBase,
        IPlayerService playerService) : base(playerService)
    {
        ReloadViewModel = reloadViewModel;
        SortViewModel = sortViewModelBase;
        MusicNavigation = musicNavigationBase;
        Items = new();
    }

    internal async Task UpdateItems(IEnumerable<TCollectionItemType> newItems)
    {
        Items.Clear();
        await UIThreadInvoker.InvokeAsync(() =>
        {
            foreach (var newItem in newItems)
            {
                Items.Add(newItem);
            }
        });
    }
}