using System.Windows;
using System.Windows.Input;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.Controls;

public partial class ButtonWithPopup
{
    public static readonly DependencyProperty ButtonContentProperty = DependencyProperty.Register(
        nameof(ButtonContent), typeof(object), typeof(ButtonWithPopup), new PropertyMetadata(default(object)));

    public static readonly DependencyProperty ClickCommandProperty = DependencyProperty.Register(
        nameof(ClickCommand), typeof(ICommand), typeof(ButtonWithPopup), new PropertyMetadata(default(ICommand)));

    public ICommand ClickCommand
    {
        get => (ICommand) GetValue(ClickCommandProperty);
        set => SetValue(ClickCommandProperty, value);
    }
    public object ButtonContent
    {
        get => GetValue(ButtonContentProperty);
        set => SetValue(ButtonContentProperty, value);
    }
    public ButtonWithPopup()
    {
        InitializeComponent();
    }
}