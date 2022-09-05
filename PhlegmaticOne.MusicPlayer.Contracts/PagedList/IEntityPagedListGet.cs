using Calabonga.UnitOfWork;
using PhlegmaticOne.WPF.Core.ViewModels;

namespace PhlegmaticOne.MusicPlayer.Contracts.PagedList;

public interface IEntityPagedListGet<T> where T : EntityBaseViewModel
{
    Task<IPagedList<T>> GetPagedListAsync(int pageSize, int pageIndex,
        Func<T, object>? sortFunc = null, Func<T, bool>? selectFunc = null);
}