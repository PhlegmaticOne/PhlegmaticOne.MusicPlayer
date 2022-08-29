﻿using PhlegmaticOne.MusicPlayer.Contracts.Helpers;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Localization;
using PhlegmaticOne.MusicPlayer.WPF.Core.Commands;
using PhlegmaticOne.MusicPlayer.WPF.Core.ViewModels;
using System.Collections.ObjectModel;

namespace PhlegmaticOne.MusicPlayer.Contracts.ControlViewModels.Sort;

public abstract class SortViewModelBase<TViewModel, TCollectionItemType> : BaseViewModel, IDisposable
    where TViewModel : ApplicationBaseViewModel
    where TCollectionItemType : EntityBaseViewModel
{
    private readonly Dictionary<string, Func<IEnumerable<TCollectionItemType>, IEnumerable<TCollectionItemType>>> _availableSorts;

    protected readonly ILocalizationService LocalizationService;
    
    protected SortViewModelBase(ILocalizationService localizationService)
    {
        LocalizationService = localizationService;
        _availableSorts = new();
        SortOptions = new();

        SortCommand = DelegateCommandFactory.CreateCommand(SortAction, _ => true);
        LoadSortOptionsCommand = DelegateCommandFactory.CreateCommand(LoadSortOptions, _ => true);
        SetCurrentSortCommand = DelegateCommandFactory.CreateCommand(SetCurrentSort, _ => true);

        LocalizationService.LanguageChanged += LocalizationServiceOnLanguageChanged;
    }

    public SortDescription<TCollectionItemType> Current { get; set; } = null!;
    public ObservableCollection<SortDescription<TCollectionItemType>> SortOptions { get; }
    public IDelegateCommand SortCommand { get; }
    public IDelegateCommand LoadSortOptionsCommand { get; }
    public IDelegateCommand SetCurrentSortCommand { get; }

    protected abstract Dictionary<string, Func<IEnumerable<TCollectionItemType>, IEnumerable<TCollectionItemType>>> GetAvailableSorts();
    protected abstract Task SortViewModelAsync(TViewModel viewModel, Func<IEnumerable<TCollectionItemType>, IEnumerable<TCollectionItemType>> sortFunc);
    private void LoadSortOptions(object? _)
    {
        if (_availableSorts.Any() == false)
        {
            UpdateSortOptions();
        }
    }

    private void UpdateSortOptions()
    {
        _availableSorts.Clear();
        SortOptions.Clear();
        foreach (var availableSort in GetAvailableSorts())
        {
            _availableSorts.Add(availableSort.Key, availableSort.Value);
            SortOptions.Add(new(availableSort.Key, availableSort.Value));
        }
    }

    private void SetCurrentSort(object? parameter)
    {
        if (parameter is SortDescription<TCollectionItemType> sortDescription)
        {
            Current = sortDescription;
        }
    }
    private async void SortAction(object? parameter)
    {
        if (parameter is TViewModel viewModel)
        {
            await Task.Run(async () =>
            {
                var sort = _availableSorts[Current.SortName];
                await SortViewModelAsync(viewModel, sort);
            });
        }
    }
    private void LocalizationServiceOnLanguageChanged(object? sender, EventArgs e) => UpdateSortOptions();

    public void Dispose()
    {
        LocalizationService.LanguageChanged -= LocalizationServiceOnLanguageChanged;
    }
}