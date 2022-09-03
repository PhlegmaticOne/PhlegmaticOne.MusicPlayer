using PhlegmaticOne.HandMapper.Lib;
using PhlegmaticOne.MusicPlayer.Contracts.Models;
using PhlegmaticOne.MusicPlayer.Contracts.Models.Base;
using PhlegmaticOne.MusicPlayer.Data.Models;

namespace PhlegmaticOne.MusicPlayer.Data.HandMappers;

public class SongsToActiveArtistViewModelMapper : HandMapperBase<List<Song>, ActiveArtistViewModel>
{
    public override ActiveArtistViewModel? Map(List<Song> from)
    {
        var artistPreviewModel = Parameters["ArtistPreview"] as ArtistPreviewViewModel;
        var result = new ActiveArtistViewModel
        {
            Id = artistPreviewModel.Id,
            Title = artistPreviewModel.Title,
            Cover = artistPreviewModel.Cover,
            Albums = new List<AlbumPreviewViewModel>(),
            Tracks = new List<TrackBaseViewModel>()
        };


        foreach (var song in from)
        {
            var album = song.Album;
            var artistLinks = song.Artists.Select(x => new ArtistLinkViewModel
            {
                Id = x.Id,
                Title = x.Title
            }).ToList();

            if (result.Albums.Any(x => x.Id == album.Id) == false)
            {
                var albumArtists = song.Album.Artists.Select(x => new ArtistLinkViewModel
                {
                    Id = x.Id,
                    IsFavorite = x.IsFavorite,
                    Title = x.Title
                }).ToList();
                var albumViewModel = new AlbumPreviewViewModel
                {
                    Id = album.Id,
                    Title = album.Title,
                    Artists = albumArtists,
                    Cover = album.AlbumCover.Cover,
                    IsFavorite = album.IsFavorite,
                    IsDownloaded = false,
                    AlbumType = album.AlbumType.ToString(),
                    IsDownloading = false,
                    DateAdded = album.DateAdded,
                    YearReleased = album.YearReleased
                };
                result.Albums.Add(albumViewModel);
            }
            var trackViewModel = new TrackBaseViewModel
            {
                Id = song.Id,
                ArtistLinks = artistLinks,
                CollectionLink = new CollectionLinkViewModel
                {
                    Id = album.Id,
                    Cover = album.AlbumCover.Cover,
                    Title = album.Title
                },
                IsFavorite = song.IsFavorite,
                IsDownloaded = string.IsNullOrEmpty(song.LocalUrl) == false,
                IsDownloading = false,
                LocalUrl = song.LocalUrl,
                Title = song.Title,
                Duration = song.Duration,
                OnlineUrl = song.OnlineUrl,
                TimePlayed = song.TimePlayed
            };
            result.Tracks.Add(trackViewModel);
        }

        return result;
    }
}