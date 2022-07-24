using System.Drawing;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using PhlegmaticOne.MusicPlayer.Core.Helpers;
using PhlegmaticOne.MusicPlayer.Entities;

namespace PhlegmaticOne.MusicPlayer.Core.HttpInfoRetrieveFeature;

public class MusifyAlbumInfoGetter : IHttpInfoGetter<Album>, IDisposable
{
    private readonly HttpClient _httpClient;
    public MusifyAlbumInfoGetter()
    {
        _httpClient = new HttpClient();
    }
    public async Task<Album> GetInfoAsync(string uri)
    {
        var source = await _httpClient.GetStringAsync(uri);
        var parser = new HtmlParser();
        var domDocument = await parser.ParseDocumentAsync(source);

        var (artist, title, yearReleased) = GetRepresentationInfo(domDocument);
        var artists = new List<Artist>() {artist};
        var cover = await GetAlbumCover(domDocument);
        var albumCover = new AlbumCover() {Cover = cover};
        var albumType = GetAlbumType(domDocument);

        var songs = GetSongs(domDocument).ToList();
        var genres = GetGenres(domDocument).ToList();

        return new Album
        {
            Artists = artists,
            Title = title,
            YearReleased = yearReleased,
            AlbumType = albumType,
            Songs = songs,
            Genres = genres,
            AlbumCover = albumCover
        };
    }

    private static (Artist, string, int) GetRepresentationInfo(IHtmlDocument htmlDocument)
    {
        var result = htmlDocument
            .QuerySelectorAll("h1")
            .Select(x => x.InnerHtml)
            .First()
            .Split(new[] { " - ", "(", ")" }, StringSplitOptions.RemoveEmptyEntries); ;
        return (new Artist() { Name = result[0] }, result[1], int.Parse(result[2]));
    }

    private static AlbumType GetAlbumType(IHtmlDocument htmlDocument)
    {
        var albumType = htmlDocument
            .QuerySelectorAll("i")
            .Where(s => s.ClassName == "zmdi zmdi-collection-music zmdi-hc-fw")
            .Select(x => x.ParentElement!.TextContent)
            .First()
            .Trim();
        return ParseStingToAlbumType(albumType);
    }

    private static IEnumerable<Song> GetSongs(IHtmlDocument htmlDocument)
    {
        var songNames = htmlDocument
            .QuerySelectorAll("a")
            .Where(s => s.ClassName == "strong")
            .Select(x => x.InnerHtml);

        var durations = htmlDocument.QuerySelectorAll("div")
            .Where(d => d.ClassName == "track__details hidden-xs-down" && d.ChildElementCount == 2)
            .Select(x => x.FirstElementChild?.InnerHtml)
            .Select(TimeHelper.ToTimeSpan!);

        foreach (var songInfo in songNames.Zip(durations))
        {
            yield return new Song()
            {
                Duration = songInfo.Second,
                Title = songInfo.First
            };
        }
    }

    private static AlbumType ParseStingToAlbumType(string albumType) => albumType switch
    {
        "Демо" => AlbumType.Demo,
        "EP" => AlbumType.EP,
        "Студийный альбом" => AlbumType.LP,
        "Split" => AlbumType.Split,
        "Сборник исполнителя" => AlbumType.Compilation,
        _ => AlbumType.Other
    };

    private static IEnumerable<Genre> GetGenres(IHtmlDocument htmlDocument)
    {
        return htmlDocument
            .QuerySelectorAll("p")
            .First(p => p.ClassName == "genre__labels")
            .Children
            .Select(x => x.InnerHtml.Trim('#'))
            .Select(x => new Genre()
            {
                Name = x
            });
    }

    private async Task<Bitmap> GetAlbumCover(IHtmlDocument htmlDocument)
    {
        var coverUrl = htmlDocument.QuerySelectorAll("img")
            .First(p => p.ClassName == "album-img lozad").Attributes.First(s => s.Name == "data-src").Value;
        await using var imageStream = await _httpClient.GetStreamAsync(coverUrl);
        return (Bitmap) Image.FromStream(imageStream);
    }
    public void Dispose()
    {
        _httpClient.Dispose();
    }
}