using System.Collections.ObjectModel;
using PhlegmaticOne.MusicPlayer.Contracts.ApplicationQueue;
using PhlegmaticOne.MusicPlayer.Contracts.ApplicationViewModels.Base;
using PhlegmaticOne.MusicPlayer.Contracts.EntityViewModels.Base;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Player;

namespace PhlegmaticOne.MusicPlayer.Contracts.ApplicationViewModels;

public class SongQueueViewModel : PlayerTrackableViewModel
{
    public ObservableCollection<TrackBaseViewModel> Songs { get; }
    public SongQueueViewModel(IPlayerService playerService) : base(playerService)
    {
        Songs = new();
        PlayerService.QueueChanged += SongsQueueOnQueueChanged;
        PlayerService.RaiseEvents();
        TrySetSong();
    }

    private void SongsQueueOnQueueChanged(object? sender, CollectionChangedEventArgs<TrackBaseViewModel> e)
    {
        switch (e.CollectionChangedType)
        {
            case CollectionChangedType.Added:
                {
                    AddSongs(e.Entities);
                    break;
                }
            case CollectionChangedType.Removed:
                {
                    RemoveSongs(e.Entities);
                    break;
                }
        }
    }

    private void AddSongs(IEnumerable<TrackBaseViewModel> songs)
    {
        foreach (var entity in songs)
        {
            Songs.Add(entity);
        }
    }

    private void RemoveSongs(IEnumerable<TrackBaseViewModel> songs)
    {
        foreach (var entity in songs)
        {
            Songs.Remove(entity);
        }
    }
}