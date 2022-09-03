using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using PhlegmaticOne.MusicPlayer.Contracts.Services.UI;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.Services.UI;

public class WpfUIThreadInvokerService : IUiThreadInvokerService
{
    public async Task InvokeAsync(Action action) => await Application.Current.Dispatcher.InvokeAsync(action, DispatcherPriority.Background);
}