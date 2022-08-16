using System.Windows;
using System.Windows.Input;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.Controls;

public partial class TrackInListing
{
    public static readonly DependencyProperty IsCurrentProperty = DependencyProperty.Register(
        nameof(IsCurrent), typeof(bool), typeof(TrackInListing), new PropertyMetadata(default(bool)));

    public bool IsCurrent
    {
        get => (bool) GetValue(IsCurrentProperty);
        set => SetValue(IsCurrentProperty, value);
    }

    public static readonly DependencyProperty PlayTrackCommandProperty = DependencyProperty.Register(
        nameof(PlayTrackCommand), typeof(ICommand), typeof(TrackInListing), new PropertyMetadata(default(ICommand)));

    public static readonly DependencyProperty IsPausedProperty = DependencyProperty.Register(
        nameof(IsPaused), typeof(bool), typeof(TrackInListing), new PropertyMetadata(default(bool)));

    public bool IsPaused
    {
        get => (bool) GetValue(IsPausedProperty);
        set => SetValue(IsPausedProperty, value);
    }

    public static readonly DependencyProperty IsStoppedProperty = DependencyProperty.Register(
        nameof(IsStopped), typeof(bool), typeof(TrackInListing), new PropertyMetadata(default(bool)));

    public bool IsStopped
    {
        get => (bool) GetValue(IsStoppedProperty);
        set => SetValue(IsStoppedProperty, value);
    }
    public ICommand PlayTrackCommand
    {
        get => (ICommand) GetValue(PlayTrackCommandProperty);
        set => SetValue(PlayTrackCommandProperty, value);
    }

    public static readonly DependencyProperty PlayPauseCommandProperty = DependencyProperty.Register(
        nameof(PlayPauseCommand), typeof(ICommand), typeof(TrackInListing), new PropertyMetadata(default(ICommand)));

    public ICommand PlayPauseCommand
    {
        get => (ICommand) GetValue(PlayPauseCommandProperty);
        set => SetValue(PlayPauseCommandProperty, value);
    }
    public TrackInListing()
    {
        InitializeComponent();
    }
}