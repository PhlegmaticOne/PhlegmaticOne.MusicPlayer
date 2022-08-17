using PhlegmaticOne.MusicPlayer.Contracts.ApplicationViewModels.Base;

namespace PhlegmaticOne.MusicPlayer.Contracts.Services.Navigation;

public interface INavigationHistory
{
    public ViewModelNode Current { get; }

    public event EventHandler HistoryChanged;
    public bool CanMoveBack { get; set; }
    public void MoveBack();
    public void Add(ApplicationBaseViewModel viewModel);
    public ViewModelNode GetFirstInHistory();
    public void Reset();
}

public class ViewModelNode
{
    public ViewModelNode? Previous { get; set; }
    public ApplicationBaseViewModel ViewModel { get; set; }

    public ViewModelNode(ApplicationBaseViewModel baseViewModel)
    {
        ViewModel = baseViewModel;
    }
}