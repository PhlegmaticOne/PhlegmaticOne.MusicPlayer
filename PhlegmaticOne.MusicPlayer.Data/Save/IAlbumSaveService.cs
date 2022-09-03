using PhlegmaticOne.MusicPlayer.Data.Models;

namespace PhlegmaticOne.MusicPlayer.Data.Save;

public interface IAlbumSaveService
{
    Task<IList<IGrouping<string, Artist>>> GetExistingArtistsAsync(Album album);
    Album AdaptWithArtists(Album album, ICollection<Artist> artists);
    Task SaveAsync(Album album);
}