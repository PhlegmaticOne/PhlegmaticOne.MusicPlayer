using PhlegmaticOne.Players.Models;

namespace PhlegmaticOne.Players.Base;

public interface IPlayer : IDisposable
{
    public event EventHandler<TimeSpan> TimeChanged;
    public event EventHandler SongEnded;
    public event EventHandler<PlayerState> PlayerStateChanged;
    public float Volume { get; set; }
    public TimeSpan UpdatePlayerTimelineTime { get; set; }
    public PlayerState PlayerState { get; }
    public TimeSpan CurrentTime { get; }
    public void Play(string fileName);
    public void PauseOrUnpause();
    public void Stop();
    public void Rewind(TimeSpan timeStamp);
}