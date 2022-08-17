using PhlegmaticOne.MusicPlayer.WPF.Core;

namespace PhlegmaticOne.MusicPlayer.Contracts.Helpers;

public class SortDescription<T> where T : BaseViewModel
{
    public SortDescription(string sortName, Func<IEnumerable<T>, IEnumerable<T>> sortingAction)
    {
        SortName = sortName;
        SortingAction = sortingAction;
    }

    public string SortName { get; set; }
    public Func<IEnumerable<T>, IEnumerable<T>> SortingAction { get; }
}