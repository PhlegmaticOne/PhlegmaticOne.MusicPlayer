using System.Collections.ObjectModel;
using MediatR;
using PhlegmaticOne.WPF.Core.Commands;
using PhlegmaticOne.WPF.Core.ViewModels;

namespace PhlegmaticOne.MusicPlayer.ViewModels.ControlViewModels.PagedList;

public class PagedListViewModel<TCollectionItem> where TCollectionItem : EntityBaseViewModel
{
    private readonly IMediator _mediator;

    public PagedListViewModel(IMediator mediator)
    {
        _mediator = mediator;
        Items = new();
    }
    public ObservableCollection<TCollectionItem> Items { get; }
    public IRelayCommand MoveToPageCommand { get; }

    private async Task MoveToPage(int pageIndex)
    {

    }
}