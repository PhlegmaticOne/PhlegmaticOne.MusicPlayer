using PhlegmaticOne.MusicPlayer.UI.WPF.Navigation;
using System;
using PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels.Base;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.Services;

public class NavigationService : INavigationService
{
    private readonly INavigationHistory _navigationHistory;
    public NavigationService(INavigationHistory navigationHistory)
    {
        _navigationHistory = navigationHistory;
        _navigationHistory.HistoryChanged += NavigationHistoryOnHistoryChanged;
    }

    public event EventHandler<bool>? CanMoveBackChanged;
    public event EventHandler? ViewModelChanged;
    public ApplicationBaseViewModel CurrentViewModel { get; set; }

    public void NavigateTo(ApplicationBaseViewModel viewModel, bool clearBeforeAdd)
    {
        if (clearBeforeAdd)
        {
            _navigationHistory.Reset();
        }
        _navigationHistory.Add(viewModel);
        CurrentViewModel = viewModel;

        OnViewModelChanged();
    }

    public void MoveBack()
    {
        _navigationHistory.MoveBack();
        CurrentViewModel = _navigationHistory.Current.ViewModel;
        OnViewModelChanged();
    }

    private void NavigationHistoryOnHistoryChanged(object? sender, EventArgs e) => CanMoveBackChanged?.Invoke(this, _navigationHistory.CanMoveBack);
    private void OnViewModelChanged() => ViewModelChanged?.Invoke(this, EventArgs.Empty);
}