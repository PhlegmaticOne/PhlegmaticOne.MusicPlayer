namespace PhlegmaticOne.WPF.Core.Commands;

public static class RelayCommandFactory
{
    public static IRelayCommand CreateCommand(Action<object?> action, Predicate<object?> predicate) => 
        new RelayGenericCommand<object>(action, predicate);

    public static IRelayCommand CreateCommand<T>(Action<T?> action, Predicate<object?> predicate) => 
        new RelayGenericCommand<T>(action, predicate);
    public static IRelayCommand CreateRequiredParameterCommand<T>(Action<T> action, Predicate<object?> predicate) =>
        new RelayGenericCommand<T>(action, predicate, true);

    public static IRelayCommand CreateAsyncCommand(Func<object?, Task> action, Predicate<object?> predicate,
        Action<Exception>? onException = null) => 
        new AsyncRelayGenericCommand<object>(action, predicate, onException);

    public static IRelayCommand CreateAsyncCommand<T>(Func<T?, Task> action, Predicate<object?> predicate,
        Action<Exception>? onException = null) =>
        new AsyncRelayGenericCommand<T>(action, predicate, onException);
    public static IRelayCommand CreateRequiredParameterAsyncCommand<T>(Func<T, Task> action, Predicate<object?> predicate,
        Action<Exception>? onException = null) =>
        new AsyncRelayGenericCommand<T>(action, predicate, onException, true);
}