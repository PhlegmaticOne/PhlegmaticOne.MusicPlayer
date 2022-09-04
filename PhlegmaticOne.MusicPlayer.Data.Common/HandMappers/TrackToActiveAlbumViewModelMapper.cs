using PhlegmaticOne.HandMapper.Lib;
using PhlegmaticOne.MusicPlayer.Data.Models;
using PhlegmaticOne.MusicPlayer.Models;
using PhlegmaticOne.MusicPlayer.Models.Base;

namespace PhlegmaticOne.MusicPlayer.Data.Common.HandMappers;

public class TrackToActiveAlbumViewModelMapper : HandMapperBase<TrackBaseViewModel, ActiveAlbumViewModel>
{
    public override ActiveAlbumViewModel? Map(TrackBaseViewModel from)
    {
        var songs = Parameters["CollectionSongs"] as ICollection<Song>;
        var isFavorite = (bool)Parameters["IsFavorite"];
        var albumType = (AlbumType) Parameters["AlbumType"];
        var dateAdded = (DateTime) Parameters["DateAdded"];
        var yearReleased = (int) Parameters["YearReleased"];
        return new ActiveAlbumViewModel
        {
            Artists = from.ArtistLinks,
            Id = from.CollectionLink.Id,
            Title = from.CollectionLink.Title,
            Cover = from.CollectionLink.Cover,
            IsDownloaded = songs.All(s => string.IsNullOrEmpty(s.LocalUrl) == false),
            IsDownloading = false,
            IsFavorite = isFavorite,
            AlbumType = albumType.ToString(),
            Tracks = songs!.Select(x => new TrackBaseViewModel
            {
                ArtistLinks = from.ArtistLinks,
                CollectionLink = from.CollectionLink,
                IsDownloaded = string.IsNullOrEmpty(x.LocalUrl) == false,
                IsFavorite = x.IsFavorite,
                IsDownloading = false,
                LocalUrl = x.LocalUrl,
                Title = x.Title,
                Duration = x.Duration,
                Id = x.Id,
                OnlineUrl = x.OnlineUrl,
                TimePlayed = x.TimePlayed
            }).ToList(),
            DateAdded = dateAdded,
            YearReleased = yearReleased
        };
    }
}