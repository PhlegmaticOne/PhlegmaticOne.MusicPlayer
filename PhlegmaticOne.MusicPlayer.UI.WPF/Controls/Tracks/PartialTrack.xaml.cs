using System.Windows;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.Controls.Tracks;

public partial class PartialTrack
{
    public static readonly DependencyProperty IsCurrentProperty = DependencyProperty.Register(
        nameof(IsCurrent), typeof(bool), typeof(PartialTrack), new PropertyMetadata(default(bool)));

    public bool IsCurrent
    {
        get => (bool) GetValue(IsCurrentProperty);
        set => SetValue(IsCurrentProperty, value);
    }
    public PartialTrack()
    {
        InitializeComponent();
    }
}