using System.Windows;
using System.Windows.Input;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.Controls;

public partial class FullLikeButton
{
    public static readonly DependencyProperty ClickCommandProperty = DependencyProperty.Register(
        nameof(ClickCommand), typeof(ICommand), typeof(FullLikeButton), new PropertyMetadata(default(ICommand)));

    public static readonly DependencyProperty IsFavoriteProperty = DependencyProperty.Register(
        nameof(IsFavorite), typeof(bool), typeof(FullLikeButton), new PropertyMetadata(default(bool)));

    public bool IsFavorite
    {
        get => (bool) GetValue(IsFavoriteProperty);
        set => SetValue(IsFavoriteProperty, value);
    }
    public ICommand ClickCommand
    {
        get => (ICommand) GetValue(ClickCommandProperty);
        set => SetValue(ClickCommandProperty, value);
    }
    public FullLikeButton()
    {
        InitializeComponent();
    }
}