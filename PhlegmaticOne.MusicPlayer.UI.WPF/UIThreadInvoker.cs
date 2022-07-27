using System;
using System.Threading.Tasks;
using System.Windows.Threading;
using static System.Windows.Application;

namespace PhlegmaticOne.MusicPlayer.UI.WPF;

public static class UIThreadInvoker
{
    public static async Task InvokeAsync(Action action) => await Current.Dispatcher.InvokeAsync(action, DispatcherPriority.Background);
}