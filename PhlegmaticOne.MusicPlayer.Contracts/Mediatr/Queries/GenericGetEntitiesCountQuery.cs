using MediatR;
using PhlegmaticOne.WPF.Core.ViewModels;

namespace PhlegmaticOne.MusicPlayer.Contracts.Mediatr.Queries;

public record GenericGetEntitiesCountQuery<T>(Func<T, bool>? SelectFunc = null) : IRequest<int> where T : EntityBaseViewModel;