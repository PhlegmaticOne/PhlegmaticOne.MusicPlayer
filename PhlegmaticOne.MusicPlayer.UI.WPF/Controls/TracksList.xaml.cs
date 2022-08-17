using System.Windows;
using System.Windows.Input;
using PhlegmaticOne.MusicPlayer.Contracts.ViewModels.Base;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.Controls;

public partial class TracksList
{
    public static readonly DependencyProperty PlayTrackCommandProperty = DependencyProperty.Register(
        nameof(PlayTrackCommand), typeof(ICommand), typeof(TracksList), new PropertyMetadata(default(ICommand)));

    public static readonly DependencyProperty PlayPauseTrackCommandProperty = DependencyProperty.Register(
        nameof(PlayPauseTrackCommand), typeof(ICommand), typeof(TracksList), new PropertyMetadata(default(ICommand)));

    public static readonly DependencyProperty LikeTrackCommandProperty = DependencyProperty.Register(
        nameof(LikeTrackCommand), typeof(ICommand), typeof(TracksList), new PropertyMetadata(default(ICommand)));

    public static readonly DependencyProperty IsPausedProperty = DependencyProperty.Register(
        nameof(IsPaused), typeof(bool), typeof(TracksList), new PropertyMetadata(default(bool)));

    public static readonly DependencyProperty IsStoppedProperty = DependencyProperty.Register(
        nameof(IsStopped), typeof(bool), typeof(TracksList), new PropertyMetadata(default(bool)));

    public static readonly DependencyProperty CurrentTrackProperty = DependencyProperty.Register(
        nameof(CurrentTrack), typeof(TrackBaseViewModel), typeof(TracksList), new PropertyMetadata(default(TrackBaseViewModel)));

    public static readonly DependencyProperty CollectionLinkClickCommandProperty = DependencyProperty.Register(
        nameof(CollectionLinkClickCommand), typeof(ICommand), typeof(TracksList), new PropertyMetadata(default(ICommand)));

    public ICommand CollectionLinkClickCommand
    {
        get => (ICommand) GetValue(CollectionLinkClickCommandProperty);
        set => SetValue(CollectionLinkClickCommandProperty, value);
    }

    public TrackBaseViewModel CurrentTrack
    {
        get => (TrackBaseViewModel) GetValue(CurrentTrackProperty);
        set => SetValue(CurrentTrackProperty, value);
    }


    public ICommand PlayTrackCommand
    {
        get => (ICommand) GetValue(PlayTrackCommandProperty);
        set => SetValue(PlayTrackCommandProperty, value);
    }

    public ICommand PlayPauseTrackCommand
    {
        get => (ICommand) GetValue(PlayPauseTrackCommandProperty);
        set => SetValue(PlayPauseTrackCommandProperty, value);
    }

    public ICommand LikeTrackCommand
    {
        get => (ICommand) GetValue(LikeTrackCommandProperty);
        set => SetValue(LikeTrackCommandProperty, value);
    }

    public bool IsPaused
    {
        get => (bool) GetValue(IsPausedProperty);
        set => SetValue(IsPausedProperty, value);
    }

    public bool IsStopped
    {
        get => (bool) GetValue(IsStoppedProperty);
        set => SetValue(IsStoppedProperty, value);
    }



    public TracksList()
    {
        InitializeComponent();
    }

}