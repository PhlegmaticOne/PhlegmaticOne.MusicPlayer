using PhlegmaticOne.MusicPlayer.Contracts.Services.Like;
using PhlegmaticOne.MusicPlayer.Contracts.Services.PlayerVolume;
using PhlegmaticOne.MusicPlayer.Models.Base;
using PhlegmaticOne.MusicPlayer.ViewModels.Base;
using PhlegmaticOne.MusicPlayerService.Base;
using PhlegmaticOne.MusicPlayerService.Models;
using PhlegmaticOne.WPF.Core.Commands;
using PhlegmaticOne.WPF.Navigation;

namespace PhlegmaticOne.MusicPlayer.ViewModels;

public class PlayerViewModel : PlayerTrackableViewModel, IDisposable
{
    private double _volume;
    private readonly IPlayerService<TrackBaseViewModel> _playerService;
    private readonly IPlayerVolumeService _playerVolumeService;
    private readonly INavigationService _navigationService;
    public TimeSpan CurrentTime { get; set; }
    public double Volume
    {
        get => _volume;
        set
        {
            _volume = value;
            _playerService.Player.Volume = (float)value;
            OnPropertyChanged();
        }
    }

    public PlayerViewModel(IPlayerService<TrackBaseViewModel> playerService, 
        IPlayerVolumeService playerVolumeService,
        ILikeService likeService,
        INavigationService navigationService) : base(playerService, likeService)
    {
        _playerService = playerService;
        _playerVolumeService = playerVolumeService;
        _navigationService = navigationService;

        _playerService.Player.TimeChanged += OnPlayerServiceOnTimeChanged;

        var volume = _playerVolumeService.GetVolume();
        _playerService.Player.Volume = volume == 0 ? 0.2f : volume;
        Volume = _playerService.Player.Volume;

        RewindCommand = RelayCommandFactory.CreateRequiredParameterCommand<double>(Rewind);
        OpenSongsQueueCommand = RelayCommandFactory.CreateEmptyCommand(OpenQueue);

        MoveNextCommand = RelayCommandFactory.CreateEmptyCommand(MoveNextAction);
        MovePreviousCommand = RelayCommandFactory.CreateEmptyCommand(MovePreviousAction);

        ChangeQueueRepeatTypeCommand = RelayCommandFactory.CreateCommand(ChangeQueueRepeatType);
        ChangeQueueShuffleTypeCommand = RelayCommandFactory.CreateRequiredParameterCommand<bool>(ChangeQueueShuffleType);

        MuteCommand = RelayCommandFactory.CreateEmptyCommand(Mute);
        SaveVolumeCommand = RelayCommandFactory.CreateEmptyCommand(SaveVolume);

        SetIsPausedAndIsStopped();
    }

    public IRelayCommand RewindCommand { get; set; }
    public IRelayCommand OpenSongsQueueCommand { get; set; }
    public IRelayCommand MoveNextCommand { get; set; }
    public IRelayCommand MovePreviousCommand { get; set; }
    public IRelayCommand ChangeQueueRepeatTypeCommand { get; set; }
    public IRelayCommand ChangeQueueShuffleTypeCommand { get; set; }
    public IRelayCommand SaveVolumeCommand { get; set; }
    public IRelayCommand MuteCommand { get; set; }

    private void SaveVolume() => _playerVolumeService.SetVolume((float)Volume);

    private void Mute() =>  Volume = 0;

    private void OpenQueue() => _navigationService.NavigateTo<SongQueueViewModel>();

    private void Rewind(double ticks)
    {
        if (CurrentSong is null) return;

        _playerService.Player.Rewind(ParseTime(ticks));
    }

    private void MoveNextAction() => _playerService.MoveNext(QueueMoveType.MoveAnyway);

    private void MovePreviousAction() => _playerService.MovePrevious();

    private void ChangeQueueRepeatType(object? parameter)
    {
        RepeatType repeatType;

        if (parameter is bool isChecked)
        {
            repeatType = isChecked ? RepeatType.RepeatQueue : RepeatType.RepeatOff;
            _playerService.RepeatType = repeatType;
            return;
        }

        repeatType = RepeatType.RepeatSong;
        _playerService.RepeatType = repeatType;
    }

    private void ChangeQueueShuffleType(bool isChecked)
    {
        var shuffleType = isChecked ? ShuffleType.ShuffleOn : ShuffleType.ShuffleOff;
        _playerService.ShuffleType = shuffleType;
    }

    private TimeSpan ParseTime(double value) => TimeSpan.FromTicks(Convert.ToInt64(value));
    private void OnPlayerServiceOnTimeChanged(object _, TimeSpan newTime) =>
        CurrentTime = newTime;
    public void Dispose()
    {
        _playerService.Player.TimeChanged -= OnPlayerServiceOnTimeChanged;
        _playerService.Dispose();
    }
}