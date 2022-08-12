using System;
using PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels.Base;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.Services;

public interface INavigationService
{
    public event EventHandler<bool> CanMoveBackChanged; 
    public event EventHandler ViewModelChanged; 
    public ApplicationBaseViewModel CurrentViewModel { get; }
    public void NavigateTo(ApplicationBaseViewModel viewModel, bool clearBeforeAdd = false);
    public void MoveBack();
}