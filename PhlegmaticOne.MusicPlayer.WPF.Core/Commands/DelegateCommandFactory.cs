namespace PhlegmaticOne.MusicPlayer.WPF.Core.Commands;

public static class DelegateCommandFactory
{
    public static IDelegateCommand CreateCommand(Action<object?> action, Predicate<object?> predicate) => 
        new DelegateCommand(action, predicate);
}