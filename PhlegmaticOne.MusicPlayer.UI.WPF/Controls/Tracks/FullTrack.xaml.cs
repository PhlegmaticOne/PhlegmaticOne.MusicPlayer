using System.Windows;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.Controls;

public partial class FullTrack
{
    public static readonly DependencyProperty IsCurrentProperty = DependencyProperty.Register(
        nameof(IsCurrent), typeof(bool), typeof(FullTrack), new PropertyMetadata(default(bool)));

    public bool IsCurrent
    {
        get => (bool) GetValue(IsCurrentProperty);
        set => SetValue(IsCurrentProperty, value);
    }
    public FullTrack()
    {
        InitializeComponent();
    }
}