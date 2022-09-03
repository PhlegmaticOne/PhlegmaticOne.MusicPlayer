using Calabonga.UnitOfWork;
using PhlegmaticOne.WPF.Core.ViewModels;

namespace PhlegmaticOne.MusicPlayer.Contracts.Services.PagedList;

public abstract class EntityPagedListGetBase<T> : IEntityPagedListGet<T> where T : EntityBaseViewModel
{
    public abstract Task<IPagedList<T>> GetPagedListAsync(int pageSize, int pageIndex);
    public async Task<object> GetPagedList(int pageSize, int pageIndex) => await GetPagedListAsync(pageSize, pageIndex);
}