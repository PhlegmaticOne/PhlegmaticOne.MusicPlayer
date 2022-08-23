using System.Windows;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.Controls;

public partial class AlbumCard
{
    public static readonly DependencyProperty ImageHeightProperty = DependencyProperty.Register(
        nameof(ImageHeight), typeof(double), typeof(AlbumCard), new PropertyMetadata(default(double)));

    public double ImageHeight
    {
        get => (double) GetValue(ImageHeightProperty);
        set => SetValue(ImageHeightProperty, value);
    }
    public AlbumCard()
    {
        InitializeComponent();
    }
}