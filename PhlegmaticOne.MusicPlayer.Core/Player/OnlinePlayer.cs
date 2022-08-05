using NAudio.Wave;

namespace PhlegmaticOne.MusicPlayer.Core.Player;

public class OnlinePlayer : IPlayer
{
    private readonly TimeSpan _updateTimelineTime;
    private readonly float _startVolume = 0.2f;
    private bool _isDisposed;
    private bool _isUserStopped;
    public OnlinePlayer()
    {
        _updateTimelineTime = TimeSpan.FromMilliseconds(500);
        IsPaused = true;
        IsStopped = true;
        _isDisposed = true;
    }
    public event EventHandler<TimeSpan>? TimeChanged;
    public event EventHandler<bool>? PauseChanged;
    public event EventHandler<bool>? StopChanged;
    public event EventHandler? SongEnded;

    public float Volume
    {
        get => _wasapiOut.Volume;
        set
        {
            if (_isDisposed == false && value <= 1)
            {
                _wasapiOut.Volume = value;
            }
        }
    }

    public TimeSpan CurrentTime { get; private set; }
    public bool IsPaused { get; private set; }
    public bool IsStopped { get; private set; }

    
    private MediaFoundationReader _mediaFoundationReader;
    private WasapiOut _wasapiOut;
    private Task? _songTask;
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
        Volume = _startVolume;

        SetPause(false);
        SetStop(false);

        _wasapiOut.Init(_mediaFoundationReader);
        _wasapiOut.Play();

        while (_wasapiOut.PlaybackState is PlaybackState.Playing or PlaybackState.Paused)
        {
            if (_wasapiOut.PlaybackState == PlaybackState.Paused) continue;

            Thread.Sleep(_updateTimelineTime);
            SetTime(CurrentTime += _updateTimelineTime);
        }

        TryDispose();

        SetTime(TimeSpan.Zero);
        SetPause(true);
        SetStop(true);
    }
    public void PauseOrUnpause()
    {
        if (IsPaused)
        {
            SetPause(false);
            _wasapiOut.Play();
        }
        else
        {
            SetPause(true);
            _wasapiOut.Pause();
        }
    }
    public void Stop()
    {
        try
        {   
            _wasapiOut.Stop();
            SetStop(true);
            _isUserStopped = true;
        }
        catch
        {
            // ignored
        }
    }

    public void Rewind(TimeSpan timeStamp)
    {
        var position = (_mediaFoundationReader.Length * timeStamp.Ticks) / _mediaFoundationReader.TotalTime.Ticks;
        _mediaFoundationReader.Position = position;
        SetTime(timeStamp);
    }
    public void Dispose() => TryDispose();

    private void SetTime(TimeSpan newTime)
    {
        CurrentTime = newTime;
        InvokeTimeChanged();
    }

    private void SetPause(bool value)
    {
        IsPaused = value;
        InvokePauseChanged();
    }

    private void SetStop(bool value)
    {
        IsStopped = value;
        InvokeStopChanged();
    }
    private void InvokeTimeChanged() => TimeChanged?.Invoke(this, CurrentTime);
    private void InvokePauseChanged() => PauseChanged?.Invoke(this, IsPaused);
    private void InvokeStopChanged() => StopChanged?.Invoke(this, IsStopped);
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