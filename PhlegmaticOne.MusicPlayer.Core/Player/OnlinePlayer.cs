using NAudio.Wave;
using PhlegmaticOne.MusicPlayer.Entities;

namespace PhlegmaticOne.MusicPlayer.Core.Player;

public class OnlinePlayer : IPlayer
{
    private bool _isPaused;
    public event EventHandler<TimeSpan>? DurationChanged;
    public Song CurrentSong { get; set; }
    public Album CurrentAlbum { get; set; }
    public TimeSpan CurrentDuration { get; set; }

    private MediaFoundationReader _mediaFoundationReader;
    private WasapiOut _wasapiOut;

    public void Play()
    {
        Task.Run(() =>
        {
            _mediaFoundationReader = new MediaFoundationReader(CurrentSong.OnlineUrl); 
            _wasapiOut = new WasapiOut();
            _wasapiOut.Volume = 0.5f;
            _wasapiOut.Init(_mediaFoundationReader);
            _wasapiOut.Play();
            
            while (_wasapiOut.PlaybackState == PlaybackState.Playing)
            {
                Thread.Sleep(100);
                CurrentDuration += TimeSpan.FromMilliseconds(100);
                Invoke();
            }

            _wasapiOut.Dispose();
            _mediaFoundationReader.Dispose();
            CurrentDuration = TimeSpan.Zero;
            Invoke();
        });
    }

    public void Pause()
    {
        if (_isPaused)
        {
            _isPaused = false;
        }
        else
        {
            _isPaused = true;
        }
    }

    private void Invoke() => DurationChanged?.Invoke(this, CurrentDuration);
}