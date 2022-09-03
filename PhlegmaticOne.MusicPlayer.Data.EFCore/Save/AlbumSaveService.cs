using Microsoft.EntityFrameworkCore;
using PhlegmaticOne.MusicPlayer.Data.Context;
using PhlegmaticOne.MusicPlayer.Data.Models;
using PhlegmaticOne.MusicPlayer.Data.Save;

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
        var artistNames = album.Artists.Select(x => x.Title);
        var genreNames = album.Genres.Select(x => x.Title);
        var artists = await _dbContext.Set<Artist>()
            .Include(x => x.Albums)
            .ThenInclude(x => x.AlbumCover)
            .Where(x => artistNames.Contains(x.Title))
            .ToListAsync();

        _existingArtists = artists;
        _existingGenres = await _dbContext.Set<Genre>()
            .Where(x => genreNames.Contains(x.Title))
            .ToListAsync();

        return artists.GroupBy(x => x.Title).ToList();
    }

    public Album AdaptWithArtists(Album album, ICollection<Artist> artists)
    {
        foreach (var artist in artists)
        {
            if (_existingArtists.Contains(artist))
            {
                var artistSongsInAlbum = album.Songs
                    .Where(x => x.Artists.Any(n => n.Title == artist.Title))
                    .ToList();

                ReplaceArtistInSongs(artistSongsInAlbum, artist);

                ReplaceArtistInAlbum(album, artist);
            }
        }

        foreach (var existingGenre in _existingGenres)
        {
            if (album.Genres.Any(x => x.Title == existingGenre.Title))
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
            var existingArtistInSong = song.Artists.First(x => x.Title == artist.Title);
            song.Artists.Remove(existingArtistInSong);
            song.Artists.Add(artist);
        }
    }

    private static void ReplaceArtistInAlbum(Album album, Artist artist)
    {
        var existingArtistInAlbum = album.Artists.First(x => x.Title == artist.Title);
        album.Artists.Remove(existingArtistInAlbum);
        album.Artists.Add(artist);
    }
    private static void ReplaceGenreInAlbum(Album album, Genre genre)
    {
        var existingGenreInAlbum = album.Genres.First(x => x.Title == genre.Title);
        album.Genres.Remove(existingGenreInAlbum);
        album.Genres.Add(genre);
    }
}