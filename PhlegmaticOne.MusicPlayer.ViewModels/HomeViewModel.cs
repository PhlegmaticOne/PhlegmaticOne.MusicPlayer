using MediatR;
using PhlegmaticOne.MusicPlayer.Contracts.Mediatr.Queries;
using PhlegmaticOne.MusicPlayer.Contracts.Models.Base;
using PhlegmaticOne.WPF.Core.Commands;
using PhlegmaticOne.WPF.Core.ViewModels;

namespace PhlegmaticOne.MusicPlayer.ViewModels;

public class HomeViewModel : ApplicationBaseViewModel
{
    private readonly IMediator _mediator;

    public HomeViewModel(IMediator mediator)
    {
        _mediator = mediator;
        CreateCommand = RelayCommandFactory.CreateAsyncCommand(Create, _ => true);
    }
    public IRelayCommand CreateCommand { get; }

    private async Task Create(object? o)
    {
        var result = await _mediator
            .Send(new GenericPagedListQuery<TrackBaseViewModel>(0, 0));
        Console.WriteLine(result);
    }
}