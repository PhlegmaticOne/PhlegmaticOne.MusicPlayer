﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Moq;
using PhlegmaticOne.MusicPlayer.Data.Context;
using PhlegmaticOne.MusicPlayer.Data.EFCore.Save;
using PhlegmaticOne.MusicPlayer.Entities;
using PhlegmaticOne.MusicPlayer.Players.HttpInfoRetrieveFeature;

namespace PhlegmaticOne.MusicPlayer.Tests;

public class SaveAlbumTests
{
    [Fact]
    public async Task Should_Not_Throw_Exceptions()
    {
        var dbContextOptionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=music-player-db;Integrated Security=True;");

        var dbContext = new ApplicationDbContext(dbContextOptionsBuilder.Options);
        var album =  await new MusifyAlbumInfoGetter()
            .GetInfoAsync(@"https://musify.club/release/vinterriket-and-paysage-dhiver-2003-205226");

        var albumSaver = new AlbumSaveService(dbContext);

        var artists = await albumSaver.GetExistingArtistsAsync(album);

        var newArtists = new List<Artist>() {artists[0].First(), album.Artists.ElementAt(1)};

        var adapted = albumSaver.AdaptWithArtists(album, newArtists);

        await albumSaver.SaveAsync(adapted);
    }

    private IHttpInfoGetter<Album> GetAlbumInfoGetter()
    {
        var artistOne = new Artist()
        {
            Name = "ArtistOne"
        };
        var artistTwo = new Artist()
        {
            Name = "ArtistTwo"
        };
        var mock = new Mock<IHttpInfoGetter<Album>>();
        mock.Setup(x => x.GetInfoAsync(string.Empty))
            .Returns(Task.FromResult(new Album()
            {
                YearReleased = 2000,
                DateAdded = DateTime.Now,
                TimePlayed = TimeSpan.Zero,
                Id = Guid.Empty,
                Title = "Title",
                IsFavorite = false,
                Artists = new List<Artist>() { artistOne, artistTwo },
                AlbumCover = new AlbumCover(),
                Genres = new List<Genre>() { new() { Name = "Genre" }},
                AlbumType = AlbumType.Compilation,
                Songs = new List<Song>()
                {
                    new()
                    {
                        Artists = new List<Artist>() { artistOne },
                        Title = "SongTitleOne"
                    },
                    new()
                    {
                        Artists = new List<Artist>() { artistOne, artistTwo },
                        Title = "SongTitleTwo"
                    },
                    new()
                    {
                        Artists = new List<Artist>() { artistTwo },
                        Title = "SongTitleThree"
                    }
                }
            }));
        return mock.Object;
    }
}