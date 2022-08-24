using System.Windows.Input;

namespace PhlegmaticOne.MusicPlayer.WPF.Core.Commands;

public interface IDelegateCommand : ICommand
{
    void RaiseCanExecute();
}