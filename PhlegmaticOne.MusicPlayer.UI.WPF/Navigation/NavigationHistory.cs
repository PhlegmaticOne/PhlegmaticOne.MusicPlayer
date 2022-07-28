using System;
using PhlegmaticOne.MusicPlayer.UI.WPF.Base;
using PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels.Base;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.Navigation;

public class NavigationHistory : ObservableObject, INavigationHistory
{
    private bool _canMoveBack;
    public ViewModelNode? Current { get; set; }
    public event EventHandler? HistoryChanged;

    public bool CanMoveBack
    {
        get => Current?.Previous is not null;
        set => _canMoveBack = value;
    }

    public void MoveBack()
    {
        Current = Current.Previous;
        RaiseHistoryChanged();
    }

    public void Add(BaseViewModel viewModel)
    {
        if (Current is null)
        {
            Current = new ViewModelNode(viewModel);
            return;
        }

        var previous = Current;

        Current = new ViewModelNode(viewModel)
        {
            Previous = previous
        };
        RaiseHistoryChanged();
    }

    public ViewModelNode GetFirstInHistory()
    {
        var firstNode = Current;
        while (firstNode.Previous is not null)
        {
            firstNode = Current.Previous;
        }
        Reset();
        return firstNode;
    }

    public void Reset()
    {
        Current = null;
    }

    private void RaiseHistoryChanged() => HistoryChanged?.Invoke(this, EventArgs.Empty);
}

