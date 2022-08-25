using System.Windows;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.Controls;

public partial class DownloadIndicator
{
    public static readonly DependencyProperty IsDownloadingProperty = DependencyProperty.Register(
        nameof(IsDownloading), typeof(bool), typeof(DownloadIndicator), new PropertyMetadata(default(bool)));

    public static readonly DependencyProperty IsDownloadedProperty = DependencyProperty.Register(
        nameof(IsDownloaded), typeof(bool), typeof(DownloadIndicator), new PropertyMetadata(default(bool)));

    public bool IsDownloaded
    {
        get => (bool) GetValue(IsDownloadedProperty);
        set => SetValue(IsDownloadedProperty, value);
    }
    public bool IsDownloading
    {
        get => (bool) GetValue(IsDownloadingProperty);
        set => SetValue(IsDownloadingProperty, value);
    }
    
    public DownloadIndicator()
    {
        InitializeComponent();
    }
}