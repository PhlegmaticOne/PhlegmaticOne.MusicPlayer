using PhlegmaticOne.MusicPlayer.Contracts.ApplicationQueue;
using PhlegmaticOne.MusicPlayer.Contracts.EntityViewModels.Base;
using PhlegmaticOne.MusicPlayer.Contracts.Services.ValueProviders;

namespace PhlegmaticOne.MusicPlayer.Contracts.Services.Player;

public interface IPlayerService : IDisposable
{
    public bool IsPaused { get; set; }
    public bool IsStopped { get; set; }
    public float Volume { get; set; }
    public IValueProvider<TrackBaseViewModel> TrackValueProvider { get; }
    public IObservableQueue<TrackBaseViewModel> TracksQueue { get; }

    public event EventHandler<bool> PauseChanged;
    public event EventHandler<bool> StopChanged;
    public event EventHandler<TimeSpan> TimeChanged;
    public event EventHandler<CollectionChangedEventArgs<TrackBaseViewModel>> QueueChanged;
    public void SetAndPlay(TrackBaseViewModel song);
    public void Stop();
    public void Rewind(TimeSpan rewindToTime);
    public void MoveNext(QueueMoveType queueMoveType);
    public void MovePrevious();
    public void ChangeShuffleType(ShuffleType shuffleType);
    public void ChangeRepeatType(RepeatType repeatType);
    public void Pause();
    public void Enqueue(IEnumerable<TrackBaseViewModel> songs, bool isClear);
    public void RaiseEvents();
}