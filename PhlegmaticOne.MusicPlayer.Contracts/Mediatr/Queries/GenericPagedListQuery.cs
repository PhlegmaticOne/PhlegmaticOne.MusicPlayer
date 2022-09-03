using Calabonga.UnitOfWork;
using MediatR;
using PhlegmaticOne.WPF.Core.ViewModels;

namespace PhlegmaticOne.MusicPlayer.Contracts.Mediatr.Queries;

public record GenericPagedListQuery<T>(int PageSize, int PageIndex) : IRequest<IPagedList<T>> where T : EntityBaseViewModel;