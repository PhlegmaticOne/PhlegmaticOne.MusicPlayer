using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using PhlegmaticOne.MusicPlayer.Contracts.ViewModels;
using PhlegmaticOne.MusicPlayer.Players.Player;
using PhlegmaticOne.MusicPlayer.UI.WPF.Infrastructure;
using PhlegmaticOne.MusicPlayer.UI.WPF.PlayerHelpers;
using PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels.Base;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels;

public class SongQueueViewModel : PlayerTrackableViewModel
{
    public ObservableCollection<SongEntityViewModel> Songs { get; }
    public SongQueueViewModel(IObservableQueue<SongEntityViewModel> songsQueue, IPlayer player, IValueProvider<SongEntityViewModel> songValueProvider, IValueProvider<AlbumEntityViewModel> albumValueProvider) : 
        base(player, songsQueue, songValueProvider, albumValueProvider)
    {
        Songs = new();
        SongsQueue.QueueChanged += SongsQueueOnQueueChanged;
        if (Songs.Any() == false)
        {
            AddSongs(songsQueue.Entities);
        }

        CurrentAlbum = AlbumValueProvider.Get();
        TrySetSong();
    }

    private void SongsQueueOnQueueChanged(object? sender, CollectionChangedEventArgs<SongEntityViewModel> e)
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

    private void AddSongs(IEnumerable<SongEntityViewModel> songs)
    {
        foreach (var entity in songs)
        {
            Songs.Add(entity);
        }
    }

    private void RemoveSongs(IEnumerable<SongEntityViewModel> songs)
    {
        foreach (var entity in songs)
        {
            Songs.Remove(entity);
        }
    }
}