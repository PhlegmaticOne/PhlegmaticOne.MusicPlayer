using System.Windows;
using System.Windows.Input;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.Controls;

public partial class LikeButton
{
    public static readonly DependencyProperty ClickCommandProperty = DependencyProperty.Register(
        nameof(ClickCommand), typeof(ICommand), typeof(LikeButton), new PropertyMetadata(default(ICommand)));

    public static readonly DependencyProperty IsFavoriteProperty = DependencyProperty.Register(
        nameof(IsFavorite), typeof(bool), typeof(LikeButton), new PropertyMetadata(default(bool)));

    public static readonly DependencyProperty IsActiveProperty = DependencyProperty.Register(
        nameof(IsActive), typeof(bool), typeof(LikeButton), new PropertyMetadata(default(bool)));

    public bool IsActive
    {
        get => (bool) GetValue(IsActiveProperty);
        set => SetValue(IsActiveProperty, value);
    }
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
    public LikeButton()
    {
        InitializeComponent();
    }
}