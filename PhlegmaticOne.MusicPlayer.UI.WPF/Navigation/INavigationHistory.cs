using PhlegmaticOne.MusicPlayer.WPF.Core;
using System;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.Navigation;

public interface INavigationHistory
{
    public ViewModelNode Current { get; }

    public event EventHandler HistoryChanged;
    public bool CanMoveBack { get; set; }
    public void MoveBack();
    public void Add(BaseViewModel viewModel);
    public ViewModelNode GetFirstInHistory();
    public void Reset();
}

public class ViewModelNode
{
    public ViewModelNode? Previous { get; set; }
    public BaseViewModel ViewModel { get; set; }

    public ViewModelNode(BaseViewModel baseViewModel)
    {
        ViewModel = baseViewModel;
    }
}