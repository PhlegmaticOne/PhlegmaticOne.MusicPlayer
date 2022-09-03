using PhlegmaticOne.WPF.Core.ViewModels;

namespace PhlegmaticOne.MusicPlayer.Contracts.Services.Sort;

public interface ISortOptionsProvider<T> where T : EntityBaseViewModel
{
    IDictionary<string, Func<T, object>> GetSortOptions();
}