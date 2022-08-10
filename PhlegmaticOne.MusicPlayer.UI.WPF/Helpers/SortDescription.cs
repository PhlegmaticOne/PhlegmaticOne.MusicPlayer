using PhlegmaticOne.MusicPlayer.WPF.Core;
using System;
using System.Collections.Generic;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.Helpers;

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