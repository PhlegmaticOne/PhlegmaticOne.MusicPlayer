namespace PhlegmaticOne.MusicPlayer.Contracts.Services.UI;

public interface IUIThreadInvokerService
{
    public Task InvokeAsync(Action action);
}