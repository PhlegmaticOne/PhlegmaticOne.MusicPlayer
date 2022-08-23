using System.Windows;
using System.Windows.Media;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.Controls;

public partial class ViewLink
{
    public static readonly DependencyProperty ForegroundHoverProperty = DependencyProperty.Register(
        nameof(ForegroundHover), typeof(Brush), typeof(ViewLink), new PropertyMetadata(default(Brush)));

    public Brush ForegroundHover
    {
        get => (Brush) GetValue(ForegroundHoverProperty);
        set => SetValue(ForegroundHoverProperty, value);
    }

    public static readonly DependencyProperty LinkTextProperty = DependencyProperty.Register(
        nameof(LinkText), typeof(string), typeof(ViewLink), new PropertyMetadata(default(string)));

    public string LinkText
    {
        get => (string) GetValue(LinkTextProperty);
        set => SetValue(LinkTextProperty, value);
    }
    public ViewLink()
    {
        InitializeComponent();
    }
}