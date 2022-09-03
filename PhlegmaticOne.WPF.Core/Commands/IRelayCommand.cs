using System.Windows.Input;

namespace PhlegmaticOne.WPF.Core.Commands;

public interface IRelayCommand : ICommand
{
    void RaiseCanExecute();
}