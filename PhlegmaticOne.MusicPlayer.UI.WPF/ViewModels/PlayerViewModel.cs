using PhlegmaticOne.MusicPlayer.Contracts.ViewModels;
using PhlegmaticOne.MusicPlayer.UI.WPF.Navigation;
using PhlegmaticOne.MusicPlayer.UI.WPF.PlayerHelpers;
using PhlegmaticOne.MusicPlayer.UI.WPF.Properties;
using PhlegmaticOne.MusicPlayer.UI.WPF.Services;
using PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels.Base;
using PhlegmaticOne.MusicPlayer.UI.WPF.ViewModelsFactories.Queue;
using PhlegmaticOne.MusicPlayer.WPF.Core;
using System;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels;

public class PlayerViewModel : PlayerTrackableViewModel, IDisposable
{
    private double _volume;
    private readonly IPlayerService _playerService;
    private readonly ISongQueueViewModelFactory _songQueueViewModelFactory;
    private readonly INavigator _navigator;
    public TimeSpan CurrentTime { get; set; }
    public double Volume
    {
        get => _volume;
        set
        {
            _volume = value;
            _playerService.Volume = (float)value;
            OnPropertyChanged();
        }
    }

    public PlayerViewModel(IPlayerService playerService, ISongQueueViewModelFactory songQueueViewModelFactory, INavigator navigator) :
        base(playerService)
    {
        _playerService = playerService;
        _songQueueViewModelFactory = songQueueViewModelFactory;
        _navigator = navigator;

        _playerService.ValueProvider<CollectionBaseViewModel>()!.ValueChanged += (_, newAlbum) => CurrentAlbum = newAlbum;
        _playerService.TimeChanged += (_, newTime) => CurrentTime = newTime;

        _playerService.Volume = Settings.Default.SavedVolume == 0 ? 0.2f : (float)Settings.Default.SavedVolume;
        Volume = _playerService.Volume;

        RewindCommand = new(Rewind, _ => true);
        OpenSongsQueueCommand = new(OpenQueue, _ => true);
        MoveNextCommand = new(MoveNextAction, _ => true);
        MovePreviousCommand = new(MovePreviousAction, _ => true);
        ChangeQueueRepeatTypeCommand = new(ChangeQueueRepeatType, _ => true);
        ChangeQueueShuffleTypeCommand = new(ChangeQueueShuffleType, _ => true);
        MuteCommand = new(Mute, _ => true);
        SaveVolumeCommand = new(SaveVolume, _ => true);

        SetIsPausedAndIsStopped();
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
            _playerService.Rewind(ParseTime(ticks));
        }
    }

    private void MoveNextAction(object? parameter)
    {
        _playerService.MoveNext(QueueMoveType.MoveAnyway);
    }

    private void MovePreviousAction(object? parameter)
    {
        _playerService.MovePrevious();
    }

    private void ChangeQueueRepeatType(object? parameter)
    {
        RepeatType repeatType;
        if (parameter is bool isChecked)
        {
            repeatType = isChecked ? RepeatType.RepeatQueue : RepeatType.RepeatOff;
            _playerService.ChangeRepeatType(repeatType);
            return;
        }

        repeatType = RepeatType.RepeatSong;
        _playerService.ChangeRepeatType(repeatType);
    }

    private void ChangeQueueShuffleType(object? parameter)
    {
        if (parameter is bool isChecked)
        {
            var shuffleType = isChecked ? ShuffleType.ShuffleOn : ShuffleType.ShuffleOff;
            _playerService.ChangeShuffleType(shuffleType);
        }
    }

    private TimeSpan ParseTime(double value) => TimeSpan.FromTicks(Convert.ToInt64(value));

    public void Dispose()
    {
        _playerService.Dispose();
    }
}