using Calabonga.UnitOfWork;
using MediatR;
using PhlegmaticOne.MusicPlayer.Contracts.Mediatr.Queries;
using PhlegmaticOne.MusicPlayer.Contracts.PagedList;
using PhlegmaticOne.WPF.Core.ViewModels;

namespace PhlegmaticOne.MusicPlayer.Contracts.Mediatr.Handlers;

public class GenericGetPagedListQueryHandler<T> : IRequestHandler<GenericGetPagedListQuery<T>, IPagedList<T>> where T : EntityBaseViewModel
{
    private readonly IEntityPagedListGet<T> _entityPagedListGetService;

    public GenericGetPagedListQueryHandler(IEntityPagedListGet<T> entityPagedListGetService)
    {
        _entityPagedListGetService = entityPagedListGetService;
    }
    public async Task<IPagedList<T>> Handle(GenericGetPagedListQuery<T> request, CancellationToken cancellationToken)
    {
        var result = await _entityPagedListGetService
            .GetPagedListAsync(request.PageSize, request.PageIndex, request.SortingFunc, request.SelectingFunc);
        return result;
    }
}