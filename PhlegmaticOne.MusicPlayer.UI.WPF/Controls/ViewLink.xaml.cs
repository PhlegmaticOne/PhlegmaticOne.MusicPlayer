using System.Windows;
using System.Windows.Input;
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

    public static readonly DependencyProperty ClickCommandProperty = DependencyProperty.Register(
        nameof(ClickCommand), typeof(ICommand), typeof(ViewLink), new PropertyMetadata(default(ICommand)));

    public ICommand ClickCommand
    {
        get => (ICommand) GetValue(ClickCommandProperty);
        set => SetValue(ClickCommandProperty, value);
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

    private void UIElement_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        ClickCommand?.Execute(DataContext);
    }
}