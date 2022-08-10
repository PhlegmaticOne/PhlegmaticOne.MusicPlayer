using PhlegmaticOne.MusicPlayer.Contracts.Base;
using PhlegmaticOne.MusicPlayer.UI.WPF.Helpers;
using PhlegmaticOne.MusicPlayer.UI.WPF.Localization;
using PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels.Base;
using PhlegmaticOne.MusicPlayer.WPF.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.Controls.Sort;

public abstract class SortViewModelBase<TViewModel, TItemType> : BaseViewModel
    where TItemType : BaseViewModel, ICollectionItem
    where TViewModel : CollectionViewModelBase<TItemType, TViewModel>
{
    private readonly Dictionary<string, Func<IEnumerable<TItemType>, IEnumerable<TItemType>>> _availableSorts;
    protected readonly ILocalizeValuesGetter LocalizeValuesGetter;
    public SortDescription<TItemType> Current { get; set; }
    public ObservableCollection<SortDescription<TItemType>> SortOptions { get; }
    protected SortViewModelBase(ILocalizeValuesGetter localizeValuesGetter)
    {
        _availableSorts = new();
        LocalizeValuesGetter = localizeValuesGetter;
        SortOptions = new();
        SortCommand = new DelegateCommand(SortAction, _ => true);
        LoadSortOptionsCommand = new DelegateCommand(LoadSortOptions, _ => true);
        SetCurrentSortCommand = new DelegateCommand(SetCurrentSort, _ => true);
    }
    public ICommand SortCommand { get; }
    public ICommand LoadSortOptionsCommand { get; }
    public ICommand SetCurrentSortCommand { get; }
    protected abstract Dictionary<string, Func<IEnumerable<TItemType>, IEnumerable<TItemType>>> GetAvailableSorts();

    private void LoadSortOptions(object? _)
    {
        if (_availableSorts.Any() == false)
        {
            foreach (var availableSort in GetAvailableSorts())
            {
                _availableSorts.Add(availableSort.Key, availableSort.Value);
                SortOptions.Add(new(availableSort.Key, availableSort.Value));
            }
        }
    }

    private void SetCurrentSort(object? parameter)
    {
        if (parameter is SortDescription<TItemType> sortDescription)
        {
            Current = sortDescription;
        }
    }
    private async void SortAction(object? parameter)
    {
        if (parameter is TViewModel viewModel)
        {
            var sort = _availableSorts[Current.SortName];
            var sorted = sort(viewModel.Items).ToList();
            await viewModel.UpdateItems(sorted);
        }
    }
}