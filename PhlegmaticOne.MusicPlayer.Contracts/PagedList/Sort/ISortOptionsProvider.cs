using PhlegmaticOne.WPF.Core.ViewModels;

namespace PhlegmaticOne.MusicPlayer.Contracts.PagedList.Sort;

public interface ISortOptionsProvider<T> where T : EntityBaseViewModel
{
    IDictionary<string, Func<T, object>> GetSortOptions();
}