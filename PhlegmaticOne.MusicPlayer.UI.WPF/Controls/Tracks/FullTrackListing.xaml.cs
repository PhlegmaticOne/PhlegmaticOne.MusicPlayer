using System.Windows;
using System.Windows.Input;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.Controls;

public partial class FullTrackListing
{
    public static readonly DependencyProperty PlayTrackCommandProperty = DependencyProperty.Register(
        nameof(PlayTrackCommand), typeof(ICommand), typeof(FullTrackListing), new PropertyMetadata(default(ICommand)));

    public static readonly DependencyProperty PlayPauseTrackCommandProperty = DependencyProperty.Register(
        nameof(PlayPauseTrackCommand), typeof(ICommand), typeof(FullTrackListing), new PropertyMetadata(default(ICommand)));

    public static readonly DependencyProperty LikeTrackCommandProperty = DependencyProperty.Register(
        nameof(LikeTrackCommand), typeof(ICommand), typeof(FullTrackListing), new PropertyMetadata(default(ICommand)));

    public static readonly DependencyProperty IsPausedProperty = DependencyProperty.Register(
        nameof(IsPaused), typeof(bool), typeof(FullTrackListing), new PropertyMetadata(default(bool)));

    public static readonly DependencyProperty IsStoppedProperty = DependencyProperty.Register(
        nameof(IsStopped), typeof(bool), typeof(FullTrackListing), new PropertyMetadata(default(bool)));

    public static readonly DependencyProperty CurrentItemProperty = DependencyProperty.Register(
        nameof(CurrentItem), typeof(object), typeof(FullTrackListing), new PropertyMetadata(default(object)));

    public static readonly DependencyProperty CollectionLinkClickCommandProperty = DependencyProperty.Register(
        nameof(CollectionLinkClickCommand), typeof(ICommand), typeof(FullTrackListing), new PropertyMetadata(default(ICommand)));

    public static readonly DependencyProperty ArtistLinkClickCommandProperty = DependencyProperty.Register(
        nameof(ArtistLinkClickCommand), typeof(ICommand), typeof(FullTrackListing), new PropertyMetadata(default(ICommand)));

    public static readonly DependencyProperty OnLoadCommandProperty = DependencyProperty.Register(
        nameof(OnLoadCommand), typeof(ICommand), typeof(FullTrackListing), new PropertyMetadata(default(ICommand)));

    public static readonly DependencyProperty OnLoadCommandParameterProperty = DependencyProperty.Register(
        nameof(OnLoadCommandParameter), typeof(object), typeof(FullTrackListing), new PropertyMetadata(default(object)));

    public static readonly DependencyProperty CanExecuteOnLoadCommandProperty = DependencyProperty.Register(
        nameof(CanExecuteOnLoadCommand), typeof(bool), typeof(FullTrackListing), new PropertyMetadata(default(bool)));

    public bool CanExecuteOnLoadCommand
    {
        get => (bool) GetValue(CanExecuteOnLoadCommandProperty);
        set => SetValue(CanExecuteOnLoadCommandProperty, value);
    }
    public object OnLoadCommandParameter
    {
        get => GetValue(OnLoadCommandParameterProperty);
        set => SetValue(OnLoadCommandParameterProperty, value);
    }

    public ICommand? OnLoadCommand
    {
        get => (ICommand)GetValue(OnLoadCommandProperty);
        set => SetValue(OnLoadCommandProperty, value);
    }

    public ICommand ArtistLinkClickCommand
    {
        get => (ICommand) GetValue(ArtistLinkClickCommandProperty);
        set => SetValue(ArtistLinkClickCommandProperty, value);
    }
    public ICommand CollectionLinkClickCommand
    {
        get => (ICommand) GetValue(CollectionLinkClickCommandProperty);
        set => SetValue(CollectionLinkClickCommandProperty, value);
    }

    public object CurrentItem
    {
        get => GetValue(CurrentItemProperty);
        set => SetValue(CurrentItemProperty, value);
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

    public FullTrackListing()
    {
        InitializeComponent();
        Loaded += OnLoaded;
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        if (CanExecuteOnLoadCommand && OnLoadCommand is not null)
        {
            OnLoadCommand?.Execute(OnLoadCommandParameter);
        }
    }
}