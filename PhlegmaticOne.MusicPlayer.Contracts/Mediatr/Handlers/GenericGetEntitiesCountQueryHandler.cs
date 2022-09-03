using MediatR;
using PhlegmaticOne.MusicPlayer.Contracts.Mediatr.Queries;
using PhlegmaticOne.WPF.Core.ViewModels;

namespace PhlegmaticOne.MusicPlayer.Contracts.Mediatr.Handlers;

public class GenericGetEntitiesCountQueryHandler<T> : IRequestHandler<GenericGetEntitiesCountQuery<T>, int>
    where T : EntityBaseViewModel
{
    public GenericGetEntitiesCountQueryHandler()
    {
        
    }
    public Task<int> Handle(GenericGetEntitiesCountQuery<T> request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}