namespace PhlegmaticOne.WPF.Core.Commands;

internal class AsyncRelayGenericCommand<T> : IRelayCommand
{
    private bool _isExecuting;
    private readonly Func<T?, Task> _action;
    private readonly Predicate<object?> _canExecute;
    private readonly Action<Exception>? _onException;
    private readonly bool _isRequired;

    internal AsyncRelayGenericCommand(Func<T?, Task> action, Predicate<object?> canExecute,
        Action<Exception>? onException = null, bool isRequired = false)
    {
        _action = action;
        _canExecute = canExecute;
        _onException = onException;
        _isRequired = isRequired;
    }
    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter) => !_isExecuting && _canExecute.Invoke(parameter);

    public async void Execute(object? parameter)
    {
        if (parameter is not T && _isRequired)
        {
            return;
        }
        var generic = (T)parameter!;
        SetIsExecuting(true);
        try
        {
            await _action.Invoke(generic);
        }
        catch (Exception exception) when (_onException is not null)
        {
            _onException.Invoke(exception);
        }
        SetIsExecuting(false);
        
    }

    public void RaiseCanExecute() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    private void SetIsExecuting(bool value)
    {
        _isExecuting = value;
        RaiseCanExecute();
    }
}