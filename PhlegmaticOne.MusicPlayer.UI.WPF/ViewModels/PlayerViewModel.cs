using System;
using System.Linq;
using PhlegmaticOne.MusicPlayer.Contracts.ViewModels;
using PhlegmaticOne.MusicPlayer.Players.Player;
using PhlegmaticOne.MusicPlayer.UI.WPF.Infrastructure;
using PhlegmaticOne.MusicPlayer.UI.WPF.Navigation;
using PhlegmaticOne.MusicPlayer.UI.WPF.PlayerHelpers;
using PhlegmaticOne.MusicPlayer.UI.WPF.Properties;
using PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels.Base;
using PhlegmaticOne.MusicPlayer.UI.WPF.ViewModelsFactories.Queue;
using PhlegmaticOne.MusicPlayer.WPF.Core;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels;

public class PlayerViewModel : PlayerTrackableViewModel, IDisposable
{
    private double _volume;
    private readonly ISongQueueViewModelFactory _songQueueViewModelFactory;
    private readonly INavigator _navigator;
    public TimeSpan CurrentTime { get; set; }
    public double Volume
    {
        get => _volume;
        set
        {
            _volume = value;
            Player.Volume = (float)value;
            OnPropertyChanged();
        }
    }

    public PlayerViewModel(IPlayer player, 
        IObservableQueue<SongEntityViewModel> songsQueue, 
        IValueProvider<SongEntityViewModel> songValueProvider, 
        IValueProvider<AlbumEntityViewModel> albumValueProvider,
        ISongQueueViewModelFactory songQueueViewModelFactory,
        INavigator navigator) : base(player, songsQueue, songValueProvider, albumValueProvider)
    {
        _songQueueViewModelFactory = songQueueViewModelFactory;
        _navigator = navigator;

        Player.Volume = Settings.Default.SavedVolume == 0 ? 0.2f : (float)Settings.Default.SavedVolume;
        Volume = Player.Volume;

        RewindCommand = new(Rewind, _ => true);
        OpenSongsQueueCommand = new(OpenQueue, _ => true);
        MoveNextCommand = new(MoveNextAction, _ => true);
        MovePreviousCommand = new(MovePreviousAction, _ => true);
        ChangeQueueRepeatTypeCommand = new(ChangeQueueRepeatType, _ => true);
        ChangeQueueShuffleTypeCommand = new(ChangeQueueShuffleType, _ => true);
        MuteCommand = new(Mute, _ => true);
        SaveVolumeCommand = new(SaveVolume, _ => true);

        player.TimeChanged += (_, newTime) => CurrentTime = newTime;
        player.SongEnded += PlayerOnSongEnded;

        albumValueProvider.ValueChanged += (_, newAlbum) => CurrentAlbum = newAlbum;

        SetIsPausedAndIsStopped();
    }

    private void PlayerOnSongEnded(object? sender, EventArgs e)
    {
        SongsQueue.MoveNext(QueueMoveType.AccordingToRepeatType);
        var currentSong = SongsQueue.Current;
        if (currentSong is not null)
        {
            PlaySongAction(currentSong);
        }
    }

    public DelegateCommand RewindCommand { get; set; }
    public DelegateCommand OpenSongsQueueCommand { get; set; }
    public DelegateCommand MoveNextCommand { get; set; }
    public DelegateCommand MovePreviousCommand { get; set; }
    public DelegateCommand ChangeQueueRepeatTypeCommand { get; set; }
    public DelegateCommand ChangeQueueShuffleTypeCommand { get; set; }
    public DelegateCommand SaveVolumeCommand { get; set; }
    public DelegateCommand MuteCommand { get; set; }

    private void SaveVolume(object? parameter)
    {
        Settings.Default.SavedVolume = Volume;
        Settings.Default.Save();
    }

    private void Mute(object? parameter)
    {
        Volume = 0;
    }

    private void OpenQueue(object? parameter)
    {
        var queueViewModel = _songQueueViewModelFactory.CreateQueueViewModel();
        _navigator.NavigateTo(queueViewModel);
    }

    private void Rewind(object? parameter)
    {
        if (CurrentSong is null) return;

        if (parameter is double ticks)
        {
            Player.Rewind(ParseTime(ticks));
        }
    }

    private void MoveNextAction(object? parameter)
    {
        if (SongsQueue.Any())
        {
            SongsQueue.MoveNext(QueueMoveType.MoveAnyway);
            SetAndPlay(SongsQueue.Current);
        }
    }

    private void MovePreviousAction(object? parameter)
    {
        if (SongsQueue.Any())
        {
            SongsQueue.MovePrevious();
            SetAndPlay(SongsQueue.Current);
        }
    }

    private void ChangeQueueRepeatType(object? parameter)
    {
        if (parameter is bool isChecked)
        {
            SongsQueue.RepeatType = isChecked ? RepeatType.RepeatQueue : RepeatType.RepeatOff;
            return;
        }

        SongsQueue.RepeatType = RepeatType.RepeatSong;
    }

    private void ChangeQueueShuffleType(object? parameter)
    {
        if (parameter is bool isChecked)
        {
            SongsQueue.ShuffleType = isChecked ? ShuffleType.ShuffleOn : ShuffleType.ShuffleOff;
        }
    }

    private void SetAndPlay(SongEntityViewModel? song)
    {
        SongValueProvider.Set(SongsQueue.Current);
        if (song is not null)
        {
            Player.Stop();
            Player.Play(ChooseFilePath(song));
        }
    }

    private TimeSpan ParseTime(double value) => TimeSpan.FromTicks(Convert.ToInt64(value));

    public void Dispose()
    {
        Player.Dispose();
    }
}