using PhlegmaticOne.MusicPlayer.Contracts.Base;
using PhlegmaticOne.MusicPlayer.UI.WPF.Controls.Reload;
using PhlegmaticOne.MusicPlayer.UI.WPF.Controls.Sort;
using PhlegmaticOne.MusicPlayer.UI.WPF.Services;
using PhlegmaticOne.MusicPlayer.WPF.Core;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels.Base;

public abstract class CollectionViewModelBase<TCollectionViewModel, TCollectionItemType> : PlayerTrackableViewModel
    where TCollectionItemType : BaseViewModel, ICollectionItem
    where TCollectionViewModel : CollectionViewModelBase<TCollectionViewModel, TCollectionItemType>
{
    public ReloadViewModelBase<TCollectionViewModel> ReloadViewModel { get; }
    public SortViewModelBase<TCollectionViewModel, TCollectionItemType> SortViewModel { get; }
    public ObservableCollection<TCollectionItemType> Items { get; }

    protected CollectionViewModelBase(IPlayerService playerService,
        ReloadViewModelBase<TCollectionViewModel> reloadViewModel,
        SortViewModelBase<TCollectionViewModel, TCollectionItemType> sortViewModelBase) : base(playerService)
    {
        ReloadViewModel = reloadViewModel;
        SortViewModel = sortViewModelBase;
        Items = new();
    }

    internal async Task UpdateItems(IEnumerable<TCollectionItemType> newItems)
    {
        await UIThreadInvoker.InvokeAsync(() =>
        {
            Items.Clear();
            foreach (var newItem in newItems)
            {
                Items.Add(newItem);
            }
        });
    }
}