using System.Windows;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.Controls;

public partial class ViewTitle
{
    public static readonly DependencyProperty TitleTextProperty = DependencyProperty.Register(
        "TitleText", typeof(string), typeof(ViewTitle), new PropertyMetadata(default(string)));

    public string TitleText
    {
        get => (string) GetValue(TitleTextProperty);
        set => SetValue(TitleTextProperty, value);
    }
    public ViewTitle()
    {
        InitializeComponent();
    }
}