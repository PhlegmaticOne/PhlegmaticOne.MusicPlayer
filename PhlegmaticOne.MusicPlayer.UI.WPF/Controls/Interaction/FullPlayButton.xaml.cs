using System.Windows;
using System.Windows.Input;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.Controls;

public partial class FullPlayButton
{
    public static readonly DependencyProperty ClickCommandProperty = DependencyProperty.Register(
        nameof(ClickCommand), typeof(ICommand), typeof(FullPlayButton), new PropertyMetadata(default(ICommand)));

    public ICommand ClickCommand
    {
        get => (ICommand) GetValue(ClickCommandProperty);
        set => SetValue(ClickCommandProperty, value);
    }

    public FullPlayButton()
    {
        InitializeComponent();
    }
}