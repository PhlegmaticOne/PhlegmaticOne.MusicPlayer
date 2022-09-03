using Calabonga.UnitOfWork;
using MediatR;
using PhlegmaticOne.WPF.Core.ViewModels;

namespace PhlegmaticOne.MusicPlayer.Contracts.Mediatr.Queries;

public record GenericGetPagedListQuery<T>(int PageSize, int PageIndex, 
    Func<T, object>? SortingFunc = null, Func<T, bool>? SelectingFunc = null) :
    IRequest<IPagedList<T>> where T : EntityBaseViewModel;