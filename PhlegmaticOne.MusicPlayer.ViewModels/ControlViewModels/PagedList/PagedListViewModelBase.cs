using System.Collections.ObjectModel;
using MediatR;
using PhlegmaticOne.MusicPlayer.Contracts.Mediatr.Queries;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Select;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Sort;
using PhlegmaticOne.MusicPlayer.Contracts.Services.UI;
using PhlegmaticOne.WPF.Core.Commands;
using PhlegmaticOne.WPF.Core.ViewModels;

namespace PhlegmaticOne.MusicPlayer.ViewModels.ControlViewModels.PagedList;

public class PagedListViewModelBase<TCollectionItem> : ApplicationBaseViewModel where TCollectionItem : EntityBaseViewModel
{
    protected readonly IMediator Mediator;
    protected readonly IUiThreadInvokerService UiThreadInvokerService;
    private Func<TCollectionItem, object>? _sortingFunc;
    private Func<TCollectionItem, bool>? _selectingFunc;
    private readonly IDictionary<string, Func<TCollectionItem, object>> _sortOptions;
    private readonly IDictionary<string, Func<TCollectionItem, bool>> _selectOptions;
    public PagedListViewModelBase(IMediator mediator,
        IUiThreadInvokerService uiThreadInvokerService,
        ISortOptionsProvider<TCollectionItem> sortOptionsProvider,
        ISelectOptionsProvider<TCollectionItem> selectOptionsProvider)
    {
        Mediator = mediator;
        UiThreadInvokerService = uiThreadInvokerService;
        _sortOptions = sortOptionsProvider.GetSortOptions();
        _selectOptions = selectOptionsProvider.GetSelectOptions();

        SortOptions = new(_sortOptions.Keys);
        SelectOptions = new(_selectOptions.Keys);
        Items = new();
        MovePreviousPageCommand = RelayCommandFactory.CreateAsyncCommand(MovePreviousPage, _ => CanMoveBack);
        MoveNextPageCommand = RelayCommandFactory.CreateAsyncCommand(MoveNextPage, _ => CanMoveForward);
        MoveToFirstPageCommand = RelayCommandFactory.CreateAsyncCommand(MoveToFirstPage, _ => CanMoveBack);
        MoveToLastPageCommand = RelayCommandFactory.CreateAsyncCommand(MoveToLastPage, _ => CanMoveForward);
        ChooseSelectCommand = RelayCommandFactory.CreateRequiredParameterCommand<string>(ChooseSelect, _ => true);
        ChooseSortCommand = RelayCommandFactory.CreateRequiredParameterCommand<string>(ChooseSort, _ => true);
        LoadCommand = RelayCommandFactory.CreateAsyncCommand(Loaded, _ => true);

        PageSize = 30;
    }
    public ObservableCollection<TCollectionItem> Items { get; }
    public ObservableCollection<string> SortOptions { get; }
    public ObservableCollection<string> SelectOptions { get; }
    public int TotalItems { get; private set; }
    public int PageSize { get; private set; }
    public int PageIndex { get; private set; }
    public bool CanMoveBack { get; private set; }
    public bool CanMoveForward { get; private set; }
    public IRelayCommand MovePreviousPageCommand { get; }
    public IRelayCommand MoveNextPageCommand { get; }
    public IRelayCommand MoveToFirstPageCommand { get; }
    public IRelayCommand MoveToLastPageCommand { get; }
    public IRelayCommand ChooseSortCommand { get; }
    public IRelayCommand ChooseSelectCommand { get; }
    public IRelayCommand LoadCommand { get; }

    internal async Task GetPage(int pageIndex)
    {
        await Task.Run(async () =>
        {
            var newItems = await Mediator
                .Send(new GenericGetPagedListQuery<TCollectionItem>(PageSize, pageIndex, _sortingFunc, _selectingFunc));
            await UpdateItems(newItems.Items);
        });
    }
    private async Task MovePreviousPage(object? parameter)
    {
        PageIndex--;

        if (PageIndex == 0)
        {
            SetCanMoveBack(false);
        }

        if (CanMoveForward == false)
        {
            SetCanMoveForward(true);
        }

        await GetPage(PageIndex);
    }

    private async Task MoveNextPage(object? parameter)
    {
        PageIndex++;
        if ((PageIndex + 1) * PageSize >= TotalItems)
        {
            SetCanMoveForward(false);
        }
        if (CanMoveBack == false)
        {
            SetCanMoveBack(true);
        }

        await GetPage(PageIndex);
    }

    private async Task MoveToFirstPage(object? parameter)
    {
        PageIndex = 0;
        SetCanMoveBack(false);

        if (CanMoveForward == false)
        {
            SetCanMoveForward(true);
        }

        await GetPage(PageIndex);
    }

    private async Task MoveToLastPage(object? parameter)
    {
        var index = TotalItems / PageSize;
        PageIndex = TotalItems % PageSize == 0 ? index - 1 : index;
        SetCanMoveForward(false);
        if (CanMoveBack == false)
        {
            SetCanMoveBack(true);
        }

        await GetPage(PageIndex);
    }

    private async Task Loaded(object? parameter)
    {
        TotalItems = 5;

        if (TotalItems > PageSize)
        {
            SetCanMoveForward(true);
        }

        await GetPage(0);
    }

    private void ChooseSort(string key)
    {
        _sortingFunc = _sortOptions[key];
    }

    private void ChooseSelect(string key)
    {
        _selectingFunc = _selectOptions[key];
    }

    private async Task UpdateItems(IEnumerable<TCollectionItem> newItems)
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

    private void SetCanMoveForward(bool value)
    {
        CanMoveForward = value;
        MoveToLastPageCommand.RaiseCanExecute();
        MoveNextPageCommand.RaiseCanExecute();
    }

    private void SetCanMoveBack(bool value)
    {
        CanMoveBack = value;
        MoveToFirstPageCommand.RaiseCanExecute();
        MovePreviousPageCommand.RaiseCanExecute();
    }
}