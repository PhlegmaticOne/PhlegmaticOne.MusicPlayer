using System.Windows;
using System.Windows.Input;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.Controls;

public partial class PlayButton
{
    public static readonly DependencyProperty IsActiveProperty = DependencyProperty.Register(
        nameof(IsActive), typeof(bool), typeof(PlayButton), new PropertyMetadata(default(bool)));

    public static readonly DependencyProperty IsPausedProperty = DependencyProperty.Register(
        nameof(IsPaused), typeof(bool), typeof(PlayButton), new PropertyMetadata(default(bool)));

    public static readonly DependencyProperty IsStoppedProperty = DependencyProperty.Register(
        nameof(IsStopped), typeof(bool), typeof(PlayButton), new PropertyMetadata(default(bool)));

    public static readonly DependencyProperty PlayCommandProperty = DependencyProperty.Register(
        nameof(PlayCommand), typeof(ICommand), typeof(PlayButton), new PropertyMetadata(default(ICommand)));

    public static readonly DependencyProperty PlayPauseCommandProperty = DependencyProperty.Register(
        nameof(PlayPauseCommand), typeof(ICommand), typeof(PlayButton), new PropertyMetadata(default(ICommand)));

    public static readonly DependencyProperty IsCurrentProperty = DependencyProperty.Register(
        nameof(IsCurrent), typeof(bool), typeof(PlayButton), new PropertyMetadata(default(bool)));

    public bool IsCurrent
    {
        get => (bool) GetValue(IsCurrentProperty);
        set => SetValue(IsCurrentProperty, value);
    }
    public ICommand PlayPauseCommand
    {
        get => (ICommand) GetValue(PlayPauseCommandProperty);
        set => SetValue(PlayPauseCommandProperty, value);
    }
    public ICommand PlayCommand
    {
        get => (ICommand) GetValue(PlayCommandProperty);
        set => SetValue(PlayCommandProperty, value);
    }
    public bool IsStopped
    {
        get => (bool)GetValue(IsStoppedProperty);
        set => SetValue(IsStoppedProperty, value);
    }
    public bool IsPaused
    {
        get => (bool)GetValue(IsPausedProperty);
        set => SetValue(IsPausedProperty, value);
    }

    public bool IsActive
    {
        get => (bool) GetValue(IsActiveProperty);
        set => SetValue(IsActiveProperty, value);
    }
    
    public PlayButton()
    {
        InitializeComponent();
    }
}