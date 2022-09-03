using NAudio.Wave;
using PhlegmaticOne.Players.Base;

namespace PhlegmaticOne.Players.Models;

public class NAudioMusicPlayer : IPlayer
{
    private readonly float _startVolume = 0.2f;
    private float _volume;
    private bool _isDisposed;
    private bool _isUserStopped;
    private MediaFoundationReader _mediaFoundationReader = null!;
    private WasapiOut _wasapiOut = null!;
    private Task? _songTask;
    public NAudioMusicPlayer()
    {
        PlayerState = PlayerState.Stopped;
        _isDisposed = true;
        _volume = _startVolume;
        UpdatePlayerTimelineTime = TimeSpan.FromMilliseconds(500);
    }

    public NAudioMusicPlayer(TimeSpan updatePlayerTimelineTime) : this() => 
        UpdatePlayerTimelineTime = updatePlayerTimelineTime;


    public event EventHandler<TimeSpan>? TimeChanged;
    public event EventHandler? SongEnded;
    public event EventHandler<PlayerState>? PlayerStateChanged;

    public float Volume
    {
        get => _wasapiOut?.Volume ?? _volume;
        set
        {
            if (value <= 1)
            {
                _volume = value;
            }
            if (_isDisposed == false)
            {
                _wasapiOut.Volume = _volume;
            }
        }
    }

    public TimeSpan UpdatePlayerTimelineTime { get; set; }
    public PlayerState PlayerState { get; private set; }
    public TimeSpan CurrentTime { get; private set; }


    public void Play(string fileName)
    {
        _songTask?.Wait();
        _songTask = new Task(() => PlaySong(fileName));
        _songTask.GetAwaiter().OnCompleted(() =>
        {
            if (_isUserStopped == false)
            {
                InvokeSongEnded();
            }

            _isUserStopped = false;
        });
        _songTask.Start();
    }

    private void PlaySong(string fileName)
    {
        TryDispose();

        _mediaFoundationReader = new MediaFoundationReader(fileName);
        _wasapiOut = new WasapiOut();

        _isDisposed = false;
        Volume = _volume;

        SetState(PlayerState.Playing);

        _wasapiOut.Init(_mediaFoundationReader);
        _wasapiOut.Play();

        while (_wasapiOut.PlaybackState is PlaybackState.Playing or PlaybackState.Paused)
        {
            if (_wasapiOut.PlaybackState == PlaybackState.Paused) continue;

            Thread.Sleep(UpdatePlayerTimelineTime);
            SetTime(CurrentTime += UpdatePlayerTimelineTime);
        }

        TryDispose();

        SetTime(TimeSpan.Zero);
        SetState(PlayerState.Stopped);
    }
    public void PauseOrUnpause()
    {
        if (PlayerState == PlayerState.Paused)
        {
            _wasapiOut.Play();
            SetState(PlayerState.Playing);
        }
        else
        {
            _wasapiOut.Pause();
            SetState(PlayerState.Paused);
        }
    }
    public void Stop()
    {
        try
        {
            _wasapiOut.Stop();
            SetState(PlayerState.Stopped);
            _isUserStopped = true;
        }
        catch
        {
            // ignored
        }
    }

    public void Rewind(TimeSpan timeStamp)
    {
        var position = _mediaFoundationReader.Length * timeStamp.Ticks / _mediaFoundationReader.TotalTime.Ticks;
        _mediaFoundationReader.Position = position;
        SetTime(timeStamp);
    }

    public void Dispose() => TryDispose();

    private void SetTime(TimeSpan newTime)
    {
        CurrentTime = newTime;
        InvokeTimeChanged();
    }

    private void SetState(PlayerState playerState)
    {
        PlayerState = playerState;
        InvokeStateChanged();
    }

    private void InvokeTimeChanged() => TimeChanged?.Invoke(this, CurrentTime);
    private void InvokeStateChanged() => PlayerStateChanged?.Invoke(this, PlayerState);
    private void InvokeSongEnded() => SongEnded?.Invoke(this, EventArgs.Empty);
    private void TryDispose()
    {
        try
        {
            _mediaFoundationReader?.Dispose();
            _wasapiOut?.Dispose();
            _isDisposed = true;
        }
        catch
        {
            // ignored
            //_isDisposed = false;
        }
    }
}