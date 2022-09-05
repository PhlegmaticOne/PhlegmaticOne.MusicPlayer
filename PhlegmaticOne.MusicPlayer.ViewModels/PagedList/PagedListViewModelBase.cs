using System.Collections.ObjectModel;
using MediatR;
using PhlegmaticOne.MusicPlayer.Contracts.Mediatr.Queries;
using PhlegmaticOne.MusicPlayer.Contracts.PagedList.PageSizes;
using PhlegmaticOne.MusicPlayer.Contracts.PagedList.Select;
using PhlegmaticOne.MusicPlayer.Contracts.PagedList.Sort;
using PhlegmaticOne.MusicPlayer.Contracts.Services.UI;
using PhlegmaticOne.WPF.Core.Commands;
using PhlegmaticOne.WPF.Core.ViewModels;

namespace PhlegmaticOne.MusicPlayer.ViewModels.PagedList;

public class PagedListViewModelBase<TCollectionItem> : ApplicationBaseViewModel where TCollectionItem : EntityBaseViewModel
{
    private readonly IMediator _mediator;
    private readonly IUiThreadInvokerService _uiThreadInvokerService;
    
    private readonly IDictionary<string, Func<TCollectionItem, object>> _sortOptions;
    private readonly IDictionary<string, Func<TCollectionItem, bool>> _selectOptions;

    private Func<TCollectionItem, object>? _sortingFunc;
    private Func<TCollectionItem, bool>? _selectingFunc;
    public PagedListViewModelBase(IMediator mediator,
        IUiThreadInvokerService uiThreadInvokerService,
        ISortOptionsProvider<TCollectionItem> sortOptionsProvider,
        ISelectOptionsProvider<TCollectionItem> selectOptionsProvider,
        IAvailablePageSizesProvider availablePageSizesProvider)
    {
        _mediator = mediator;
        _uiThreadInvokerService = uiThreadInvokerService;
        _sortOptions = sortOptionsProvider.GetSortOptions();
        _selectOptions = selectOptionsProvider.GetSelectOptions();
        
        SortOptions = new(_sortOptions.Keys);
        SelectOptions = new(_selectOptions.Keys);
        Items = new();
        AvailablePageSizes = availablePageSizesProvider.GetAvailablePageSizes().ToList();
        PageSize = availablePageSizesProvider.InitialPageSize;

        MovePreviousPageCommand = RelayCommandFactory.CreateAsyncCommand(MovePreviousPage, _ => CanMoveBack);
        MoveNextPageCommand = RelayCommandFactory.CreateAsyncCommand(MoveNextPage, _ => CanMoveForward);
        MoveToFirstPageCommand = RelayCommandFactory.CreateAsyncCommand(MoveToFirstPage, _ => CanMoveBack);
        MoveToLastPageCommand = RelayCommandFactory.CreateAsyncCommand(MoveToLastPage, _ => CanMoveForward);
        SelectCommand = RelayCommandFactory.CreateRequiredParameterAsyncCommand<string>(Select, _ => true);
        SortCommand = RelayCommandFactory.CreateRequiredParameterAsyncCommand<string>(Sort, _ => true);
        ReloadCurrentPageCommand = RelayCommandFactory.CreateAsyncCommand(ReloadCurrentPage, _ => true);
        RestoreCommand = RelayCommandFactory.CreateAsyncCommand(Restore, _ => true);
        ChangePageSizeCommand = RelayCommandFactory.CreateRequiredParameterAsyncCommand<int>(ChangePageSize, _ => true);
        LoadCommand = RelayCommandFactory.CreateAsyncCommand(Loaded, _ => true);
    }
    public ObservableCollection<TCollectionItem> Items { get; }
    public List<int> AvailablePageSizes { get; }
    public ObservableCollection<string> SortOptions { get; }
    public ObservableCollection<string> SelectOptions { get; }
    public int TotalItems { get; private set; }
    public int PageSize { get; private set; }
    public int PageIndex { get; private set; }
    public int FromItemIndex { get; private set; }
    public int ToItemIndex { get; private set; }
    public bool CanMoveBack { get; private set; }
    public bool CanMoveForward { get; private set; }
    public IRelayCommand MovePreviousPageCommand { get; }
    public IRelayCommand MoveNextPageCommand { get; }
    public IRelayCommand MoveToFirstPageCommand { get; }
    public IRelayCommand MoveToLastPageCommand { get; }
    public IRelayCommand SortCommand { get; }
    public IRelayCommand SelectCommand { get; }
    public IRelayCommand ReloadCurrentPageCommand { get; }
    public IRelayCommand RestoreCommand { get; }
    public IRelayCommand ChangePageSizeCommand { get; }
    public IRelayCommand LoadCommand { get; }

    internal async Task GetPage(int pageIndex, bool updateItemsCount = false)
    {
        await Task.Run(async () =>
        {
            if (updateItemsCount)
            {
                TotalItems = await _mediator.Send(new GenericGetEntitiesCountQuery<TCollectionItem>(_selectingFunc));
            }
            var newItems = await _mediator
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
        UpdateItemIndexes();
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
        UpdateItemIndexes();
    }

    private async Task MoveToFirstPage(object? parameter)
    {
        Reset();
        await GetPage(PageIndex);
        UpdateItemIndexes();
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
        UpdateItemIndexes();
    }

    private async Task Loaded(object? parameter)
    {
        await GetPage(0, true);
        Reset();
        UpdateItemIndexes();
    }

    private async Task Sort(string key)
    {
        _sortingFunc = _sortOptions[key];
        await GetPage(PageIndex);
    }

    private async Task Select(string key)
    {
        _selectingFunc = _selectOptions[key];
        await GetPage(0, true);
        Reset();
        UpdateItemIndexes();
    }

    private async Task ReloadCurrentPage(object? _) => await GetPage(PageIndex);

    private async Task Restore(object? _)
    {
        _selectingFunc = null;
        _sortingFunc = null;
        Reset();
        await GetPage(0, true);
    }

    private async Task ChangePageSize(int newPageSize)
    {
        PageSize = newPageSize;
        Reset();
        await GetPage(0, true);
        UpdateItemIndexes();
    }
    private async Task UpdateItems(IEnumerable<TCollectionItem> newItems)
    {
        await _uiThreadInvokerService.InvokeAsync(() =>
        {
            Items.Clear();
            foreach (var newItem in newItems)
            {
                Items.Add(newItem);
            }
        });
    }

    private void Reset()
    {
        PageIndex = 0;
        SetCanMoveBack(false);
        SetCanMoveForward(TotalItems > PageSize);
    }

    private void UpdateItemIndexes()
    {
        FromItemIndex = PageIndex * PageSize + 1;
        ToItemIndex = FromItemIndex + Items.Count - 1;
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