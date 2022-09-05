using PhlegmaticOne.WPF.Core.ViewModels;

namespace PhlegmaticOne.MusicPlayer.Contracts.PagedList.Select;

public interface ISelectOptionsProvider<T> where T : EntityBaseViewModel
{
    IDictionary<string, Func<T, bool>> GetSelectOptions();
}