using PhlegmaticOne.MusicPlayer.Models.Base;
using PhlegmaticOne.MusicPlayerService.Base;
using PhlegmaticOne.MusicPlayerService.Models;

namespace PhlegmaticOne.MusicPlayer.Contracts.KeyHandlers;

public class GlobalKeyHandler : IGlobalKeyHandler
{
    private const float SYSTEM_DVolume = 0.02f;
    private readonly Dictionary<string, Action> _hadlers;
    private readonly IPlayerService<TrackBaseViewModel> _player;

    public GlobalKeyHandler(IPlayerService<TrackBaseViewModel> player)
    {
        _player = player;
        _hadlers = new()
        {
            { "MediaPlayPause", HandlePause },
            { "MediaNextTrack",  HandleMoveNext },
            { "MediaPreviousTrack",  HandleMovePrevious },
            { "MediaStop",  HandleStop },
            { "VolumeUp",  HandleVolumeUp },
            { "VolumeDown",  HandleVolumeDown }
        };
    }
    public void HanleKey(string key)
    {
        if(_hadlers.TryGetValue(key, out var action))
        {
            action();
        }
    }

    private void HandlePause()
    {
        if (_player.CurrentEntityInPlayer is not null)
        {
            _player.Player.PauseOrUnpause();
        }
    }
    private void HandleMoveNext()
    {
        if (_player.Any())
        {
            _player.MoveNext(QueueMoveType.AccordingToRepeatType);
        }
    }

    private void HandleMovePrevious()
    {
        if (_player.Any())
        {
            _player.MovePrevious();
        }
    }
    private void HandleStop()
    {
        if(_player.CurrentEntityInPlayer is not null)
        {
            _player.Player.Stop();
        }
    }

    private void HandleVolumeUp()
    {
        _player.Player.Volume += SYSTEM_DVolume;
    }

    private void HandleVolumeDown()
    {
        _player.Player.Volume -= SYSTEM_DVolume;
    }
}
