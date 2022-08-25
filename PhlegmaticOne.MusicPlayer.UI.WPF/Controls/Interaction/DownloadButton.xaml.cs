using System.Windows;
using System.Windows.Input;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.Controls;

public partial class DownloadButton
{
    public static readonly DependencyProperty ClickCommandProperty = DependencyProperty.Register(
        nameof(ClickCommand), typeof(ICommand), typeof(DownloadButton), new PropertyMetadata(default(ICommand)));

    public static readonly DependencyProperty IsDownloadedProperty = DependencyProperty.Register(
        nameof(IsDownloaded), typeof(bool), typeof(DownloadButton), new PropertyMetadata(default(bool)));

    public static readonly DependencyProperty IsDownloadingProperty = DependencyProperty.Register(
        nameof(IsDownloading), typeof(bool), typeof(DownloadButton), new PropertyMetadata(default(bool)));

    public bool IsDownloading
    {
        get => (bool) GetValue(IsDownloadingProperty);
        set => SetValue(IsDownloadingProperty, value);
    }
    public bool IsDownloaded
    {
        get => (bool) GetValue(IsDownloadedProperty);
        set => SetValue(IsDownloadedProperty, value);
    }
    public ICommand ClickCommand
    {
        get => (ICommand) GetValue(ClickCommandProperty);
        set => SetValue(ClickCommandProperty, value);
    }
    public DownloadButton()
    {
        InitializeComponent();
    }
}