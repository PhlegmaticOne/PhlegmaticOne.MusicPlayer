using PhlegmaticOne.HandMapper;
using PhlegmaticOne.MusicPlayer.Contracts.ViewModels;
using PhlegmaticOne.MusicPlayer.Entities;

namespace PhlegmaticOne.MusicPlayer.Contracts.HandMappers;

public class AlbumsToPreviewViewModelMapper : HandMapperBase<IEnumerable<Album>, IEnumerable<AlbumPreviewViewModel>>
{
    public override IEnumerable<AlbumPreviewViewModel>? Map(IEnumerable<Album> from) =>
        from.Select(album => new AlbumPreviewViewModel()
        {
            Artists = album.Artists.Select(x => x.Name).ToList(),
            Title = album.Title,
            Cover = album.AlbumCover,
            Id = album.Id,
            IsDownloaded = false,
            IsDownloading = false,
            IsFavorite = album.IsFavorite,
            DateAdded = album.DateAdded,
            YearReleased = album.YearReleased
        });
}