namespace PhlegmaticOne.MusicPlayer.Core.Player;

public interface IPlayer : IDisposable
{
    public event EventHandler<TimeSpan> TimeChanged;
    public event EventHandler<bool> PauseChanged;
    public event EventHandler<bool> StopChanged;
    public event EventHandler SongEnded;
    public bool IsPaused { get; }
    public bool IsStopped { get; }
    public TimeSpan CurrentTime { get; }
    public float Volume { get; set; }
    public void Play(string fileName);
    public void PauseOrUnpause();
    public void Stop();
    public void Rewind(TimeSpan timeStamp);
}