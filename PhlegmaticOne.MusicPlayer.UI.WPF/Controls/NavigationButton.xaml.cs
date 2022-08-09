using System.Windows;
using PhlegmaticOne.MusicPlayer.UI.WPF.Navigation;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.Controls;

public partial class NavigationButton
{
    public static readonly DependencyProperty ButtonContentProperty = DependencyProperty.Register(
        "ButtonContent", typeof(object), typeof(NavigationButton), new PropertyMetadata(default(object)));

    public object ButtonContent
    {
        get => GetValue(ButtonContentProperty);
        set => SetValue(ButtonContentProperty, value);
    }

    public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register(
        "Header", typeof(string), typeof(NavigationButton), new PropertyMetadata(default(string)));

    public string Header
    {
        get => (string) GetValue(HeaderProperty);
        set => SetValue(HeaderProperty, value);
    }

    public static readonly DependencyProperty ViewTypeProperty = DependencyProperty.Register(
        "ViewType", typeof(ViewType), typeof(NavigationButton), new PropertyMetadata(default(ViewType)));

    public ViewType ViewType
    {
        get => (ViewType) GetValue(ViewTypeProperty);
        set => SetValue(ViewTypeProperty, value);
    }

    public static readonly DependencyProperty IsCurrentProperty = DependencyProperty.Register(
        "IsCurrent", typeof(bool), typeof(NavigationButton), new PropertyMetadata(default(bool)));

    public bool IsCurrent
    {
        get => (bool) GetValue(IsCurrentProperty);
        set => SetValue(IsCurrentProperty, value);
    }
    public NavigationButton()
    {
        InitializeComponent();
        
    }
}