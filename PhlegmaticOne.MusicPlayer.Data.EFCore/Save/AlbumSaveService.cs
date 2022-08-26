using Microsoft.EntityFrameworkCore;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Save;
using PhlegmaticOne.MusicPlayer.Data.Context;
using PhlegmaticOne.MusicPlayer.Entities;

namespace PhlegmaticOne.MusicPlayer.Data.EFCore.Save;

public class AlbumSaveService : IAlbumSaveService
{
    private readonly ApplicationDbContext _dbContext;
    private List<Artist> _existingArtists = null!;
    private List<Genre> _existingGenres = null!;
    public AlbumSaveService(ApplicationDbContext dbContext) => 
        _dbContext = dbContext;

    public async Task<IList<IGrouping<string, Artist>>> GetExistingArtistsAsync(Album album)
    {
        var artistNames = album.Artists.Select(x => x.Name);
        var artists = await _dbContext.Set<Artist>()
            .Include(x => x.Albums)
            .ThenInclude(x => x.Genres)
            .Include(x => x.Albums)
            .ThenInclude(x => x.AlbumCover)
            //.Include(x => x.Songs)
            .Where(x => artistNames.Contains(x.Name))
            .ToListAsync();

        _existingArtists = artists;
        _existingGenres = artists.SelectMany(x => x.Albums).SelectMany(x => x.Genres).Distinct().ToList();

        return artists.GroupBy(x => x.Name).ToList();
    }

    public Album AdaptWithArtists(Album album, ICollection<Artist> artists)
    {
        foreach (var artist in artists)
        {
            if (_existingArtists.Contains(artist))
            {
                var artistSongsInAlbum = album.Songs
                    .Where(x => x.Artists.Any(n => n.Name == artist.Name))
                    .ToList();

                ReplaceArtistInSongs(artistSongsInAlbum, artist);

                ReplaceArtistInAlbum(album, artist);
            }
        }

        foreach (var existingGenre in _existingGenres)
        {
            if (album.Genres.Any(x => x.Name == existingGenre.Name))
            {
                ReplaceGenreInAlbum(album, existingGenre);
            }
        }

        return album;
    }

    public async Task SaveAsync(Album album)
    {
        album.DateAdded = DateTime.Now;
        await _dbContext.Set<Album>().AddAsync(album);
        await _dbContext.SaveChangesAsync();
    }

    private static void ReplaceArtistInSongs(List<Song> songs, Artist artist)
    {
        foreach (var song in songs)
        {
            var existingArtistInSong = song.Artists.First(x => x.Name == artist.Name);
            song.Artists.Remove(existingArtistInSong);
            song.Artists.Add(artist);
        }
    }

    private static void ReplaceArtistInAlbum(Album album, Artist artist)
    {
        var existingArtistInAlbum = album.Artists.First(x => x.Name == artist.Name);
        album.Artists.Remove(existingArtistInAlbum);
        album.Artists.Add(artist);
    }
    private static void ReplaceGenreInAlbum(Album album, Genre genre)
    {
        var existingGenreInAlbum = album.Genres.First(x => x.Name == genre.Name);
        album.Genres.Remove(existingGenreInAlbum);
        album.Genres.Add(genre);
    }
}