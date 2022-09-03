using Calabonga.UnitOfWork;
using PhlegmaticOne.WPF.Core.ViewModels;

namespace PhlegmaticOne.MusicPlayer.Contracts.Services.PagedList;

public interface IEntityPagedListGetService
{
    public Task<IPagedList<T>> GetPagedListAsync<T>(int pageSize, int pageIndex) where T : EntityBaseViewModel;
}