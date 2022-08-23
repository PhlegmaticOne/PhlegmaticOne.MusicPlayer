using PhlegmaticOne.MusicPlayer.Contracts.ApplicationQueue;
using PhlegmaticOne.MusicPlayer.Contracts.ApplicationViewModels.Base;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Player;
using PhlegmaticOne.MusicPlayer.WPF.Core;

namespace PhlegmaticOne.MusicPlayer.Contracts.ApplicationViewModels;

public class PlayerViewModel : PlayerTrackableViewModel, IDisposable
{
    private double _volume;
    private readonly IPlayerService _playerService;
    private readonly IPlayerVolumeService _playerVolumeService;
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

    public PlayerViewModel(IPlayerService playerService, IPlayerVolumeService playerVolumeService) : base(playerService)
    {
        _playerService = playerService;
        _playerVolumeService = playerVolumeService;

        _playerService.TimeChanged += (_, newTime) => CurrentTime = newTime;

        var volume = _playerVolumeService.GetVolume();
        _playerService.Volume = volume == 0 ? 0.2f : volume;
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
        _playerVolumeService.SetVolume((float)Volume);
    }

    private void Mute(object? parameter)
    {
        Volume = 0;
    }

    private void OpenQueue(object? parameter)
    {
        //_songQueueNavigation.NavigateToMusicCommand.Execute(new object());
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