namespace PhlegmaticOne.MusicPlayer.WPF.Core.Commands;

internal class DelegateCommand : IDelegateCommand
{
    private readonly Action<object?> _action;
    private readonly Predicate<object?>? _canExecute;

    public DelegateCommand(Action<object?> action, Predicate<object?>? canExecute = null)
    {
        _action = action;
        _canExecute = canExecute;
    }
    public bool CanExecute(object? parameter) => _canExecute is not null && _canExecute.Invoke(parameter);

    public void Execute(object? parameter) => _action.Invoke(parameter);

    public event EventHandler? CanExecuteChanged;
    public void RaiseCanExecute() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
}