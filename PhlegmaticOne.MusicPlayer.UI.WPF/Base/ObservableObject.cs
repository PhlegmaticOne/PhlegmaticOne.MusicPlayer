using System.ComponentModel;
using System.Runtime.CompilerServices;
using PhlegmaticOne.MusicPlayer.UI.WPF.Annotations;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.Base;

public abstract class ObservableObject : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    [NotifyPropertyChangedInvocator]
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}