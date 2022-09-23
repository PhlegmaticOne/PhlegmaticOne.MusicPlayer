using Microsoft.Extensions.DependencyInjection;
using PhlegmaticOne.HandMapper;
using PhlegmaticOne.HandMapper.Extensions;
using PhlegmaticOne.MusicPlayer.Data.Common.HandMappers;
using PhlegmaticOne.MusicPlayer.Data.Models;
using PhlegmaticOne.MusicPlayer.Models;

namespace PhlegmaticOne.MusicPlayer.Tests;

public class HandMappersTests
{
    [Fact]
    public void Test()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddHandMappers(typeof(AlbumPreviewToActiveViewModelMapper).Assembly);
        var services = serviceCollection.BuildServiceProvider();

        var mapper = services.GetRequiredService<IHandMapperService>();
        var album = mapper.AddParameter("CollectionSongs", new List<Song>())
            .Map<ActiveAlbumViewModel>(new AlbumPreviewViewModel()
            {
                AlbumType = AlbumType.Demo.ToString(),
                Artists = new List<ArtistLinkViewModel>(),
            });

        Assert.NotNull(album);
    }
}
