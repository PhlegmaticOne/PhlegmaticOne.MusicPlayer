using PhlegmaticOne.MusicPlayer.UI.WPF.PlayerHelpers;
using PhlegmaticOne.MusicPlayer.UI.WPF.Services;
using PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels.Base;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using PhlegmaticOne.MusicPlayer.Contracts.ViewModels.Base;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels;

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