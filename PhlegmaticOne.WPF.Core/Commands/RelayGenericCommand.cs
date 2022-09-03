namespace PhlegmaticOne.WPF.Core.Commands;

internal class RelayGenericCommand<T> : IRelayCommand where T : class
{
    private bool _isExecuting;
    private readonly Action<T?> _action;
    private readonly Predicate<object?> _canExecute;
    private readonly bool _isRequired;

    internal RelayGenericCommand(Action<T?> action, Predicate<object?> canExecute, bool isRequired = false)
    {
        _action = action;
        _canExecute = canExecute;
        _isRequired = isRequired;
    }
    public bool CanExecute(object? parameter) => !_isExecuting && _canExecute.Invoke(parameter);
    public void Execute(object? parameter)
    {
        var generic = parameter as T;
        if (_isRequired && generic is null)
        {
            return;
        }
        SetIsExecuting(true);
        _action.Invoke(generic);
        SetIsExecuting(false);
    }

    public event EventHandler? CanExecuteChanged;
    public void RaiseCanExecute() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    private void SetIsExecuting(bool value)
    {
        _isExecuting = value;
        RaiseCanExecute();
    }
}