using MediatR;
using PhlegmaticOne.MusicPlayer.Contracts.Mediatr.Queries;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Count;
using PhlegmaticOne.WPF.Core.ViewModels;

namespace PhlegmaticOne.MusicPlayer.Contracts.Mediatr.Handlers;

public class GenericGetEntitiesCountQueryHandler<T> : IRequestHandler<GenericGetEntitiesCountQuery<T>, int>
    where T : EntityBaseViewModel
{
    private readonly IGetEntitiesCountGetService<T> _getEntitiesCountGetService;

    public GenericGetEntitiesCountQueryHandler(IGetEntitiesCountGetService<T> getEntitiesCountGetService)
    {
        _getEntitiesCountGetService = getEntitiesCountGetService;
    }
    public async Task<int> Handle(GenericGetEntitiesCountQuery<T> request, CancellationToken cancellationToken)
    {
        return await _getEntitiesCountGetService.GetEntitiesCountAsync(request.SelectFunc);
    }
}