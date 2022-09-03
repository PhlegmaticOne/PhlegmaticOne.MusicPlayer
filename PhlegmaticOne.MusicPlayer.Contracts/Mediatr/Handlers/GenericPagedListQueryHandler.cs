using Calabonga.UnitOfWork;
using MediatR;
using PhlegmaticOne.MusicPlayer.Contracts.Mediatr.Queries;
using PhlegmaticOne.MusicPlayer.Contracts.Services.PagedList;
using PhlegmaticOne.WPF.Core.ViewModels;

namespace PhlegmaticOne.MusicPlayer.Contracts.Mediatr.Handlers;

public class GenericPagedListQueryHandler<T> : IRequestHandler<GenericPagedListQuery<T>, IPagedList<T>> where T : EntityBaseViewModel
{
    private readonly IEntityPagedListGetService _entityPagedListGetService;

    public GenericPagedListQueryHandler(IEntityPagedListGetService entityPagedListGetService)
    {
        _entityPagedListGetService = entityPagedListGetService;
    }
    public async Task<IPagedList<T>> Handle(GenericPagedListQuery<T> request, CancellationToken cancellationToken)
    {
        var result = await _entityPagedListGetService
            .GetPagedListAsync<T>(request.PageSize, request.PageIndex);
        return result;
    }
}