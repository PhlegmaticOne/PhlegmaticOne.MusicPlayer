namespace PhlegmaticOne.MusicPlayer.Contracts.Services.UI;

public interface IUiThreadInvokerService
{
    public Task InvokeAsync(Action action);
}