using AngleSharp.Dom;
using PhlegmaticOne.HandMapper.Lib;
using PhlegmaticOne.MusicPlayer.Contracts.ViewModels;
using PhlegmaticOne.MusicPlayer.Contracts.ViewModels.Base;
using PhlegmaticOne.MusicPlayer.Entities;

namespace PhlegmaticOne.MusicPlayer.Contracts.HandMappers;

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
            IsDownloaded = false,
            IsDownloading = false,
            IsFavorite = isFavorite,
            AlbumType = albumType,
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