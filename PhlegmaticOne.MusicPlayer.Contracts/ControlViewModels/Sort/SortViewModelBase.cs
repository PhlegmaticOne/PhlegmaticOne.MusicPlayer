using System.Collections.ObjectModel;
using System.Windows.Input;
using PhlegmaticOne.MusicPlayer.Contracts.ApplicationViewModels.Base;
using PhlegmaticOne.MusicPlayer.Contracts.Base;
using PhlegmaticOne.MusicPlayer.Contracts.Helpers;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Localization;
using PhlegmaticOne.MusicPlayer.WPF.Core;

namespace PhlegmaticOne.MusicPlayer.Contracts.ControlViewModels.Sort;

public abstract class SortViewModelBase<TViewModel, TItemType> : BaseViewModel
    where TItemType : BaseViewModel, ICollectionItem
    where TViewModel : CollectionViewModelBase<TViewModel, TItemType>
{
    protected readonly ILocalizationService LocalizationService;
    private readonly Dictionary<string, Func<IEnumerable<TItemType>, IEnumerable<TItemType>>> _availableSorts;
    public SortDescription<TItemType> Current { get; set; }
    public ObservableCollection<SortDescription<TItemType>> SortOptions { get; }
    protected SortViewModelBase(ILocalizationService localizationService)
    {
        LocalizationService = localizationService;
        _availableSorts = new();
        SortOptions = new();
        SortCommand = new DelegateCommand(SortAction, _ => true);
        LoadSortOptionsCommand = new DelegateCommand(LoadSortOptions, _ => true);
        SetCurrentSortCommand = new DelegateCommand(SetCurrentSort, _ => true);

        LocalizationService.LanguageChanged += LocalizationServiceOnLanguageChanged;
    }

    private void LocalizationServiceOnLanguageChanged(object? sender, EventArgs e) => UpdateSortOptions();

    public ICommand SortCommand { get; }
    public ICommand LoadSortOptionsCommand { get; }
    public ICommand SetCurrentSortCommand { get; }
    protected abstract Dictionary<string, Func<IEnumerable<TItemType>, IEnumerable<TItemType>>> GetAvailableSorts();

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