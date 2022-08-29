using PhlegmaticOne.HandMapper.Lib;
using PhlegmaticOne.MusicPlayer.Contracts.EntityViewModels;
using PhlegmaticOne.MusicPlayer.Contracts.EntityViewModels.Base;
using PhlegmaticOne.MusicPlayer.Entities;

namespace PhlegmaticOne.MusicPlayer.Contracts.HandMappers;

public class AlbumPreviewToActiveViewModelMapper : HandMapperBase<AlbumPreviewViewModel, ActiveAlbumViewModel>
{
    public override ActiveAlbumViewModel? Map(AlbumPreviewViewModel from)
    {
        var songs = Parameters["CollectionSongs"] as List<Song>;
        return new ActiveAlbumViewModel()
        {
            Artists = from.Artists,
            Title = from.Title,
            Cover = from.Cover,
            IsFavorite = from.IsFavorite,
            IsDownloaded = from.IsDownloaded,
            IsDownloading = false,
            Id = from.Id,
            AlbumType = from.AlbumType,
            DateAdded = from.DateAdded,
            YearReleased = from.YearReleased,
            Tracks = songs!.Select(x => new TrackBaseViewModel
            {
                ArtistLinks = x.Artists.Select(a => new ArtistLinkViewModel
                {
                    Id = a.Id,
                    IsFavorite = a.IsFavorite,
                    Name = a.Name
                }).ToList(),
                CollectionLink = new CollectionLinkViewModel()
                {
                    Cover = from.Cover,
                    Id = from.Id,
                    Title = from.Title,
                },
                IsDownloaded = string.IsNullOrEmpty(x.LocalUrl) == false,
                IsFavorite = x.IsFavorite,
                IsDownloading = false,
                LocalUrl = x.LocalUrl,
                Title = x.Title,
                Duration = x.Duration,
                Id = x.Id,
                OnlineUrl = x.OnlineUrl,
                TimePlayed = x.TimePlayed
            }).ToList()
        };
    }
}