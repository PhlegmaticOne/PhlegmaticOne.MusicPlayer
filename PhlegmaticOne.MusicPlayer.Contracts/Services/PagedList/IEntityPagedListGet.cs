using Calabonga.UnitOfWork;
using PhlegmaticOne.WPF.Core.ViewModels;

namespace PhlegmaticOne.MusicPlayer.Contracts.Services.PagedList;

public interface IEntityPagedListGet<T> : IEntityPagedListGet where T : EntityBaseViewModel
{
    Task<IPagedList<T>> GetPagedListAsync(int pageSize, int pageIndex);
}

public interface IEntityPagedListGet
{
    Task<object> GetPagedList(int pageSize, int pageIndex);
}