using System.Drawing;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using PhlegmaticOne.MusicPlayer.Entities;
using PhlegmaticOne.MusicPlayer.Players.Helpers;

namespace PhlegmaticOne.MusicPlayer.Players.HttpInfoRetrieveFeature;

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

        var cover = await GetAlbumCover(domDocument);
        var (title, yearReleased) = GetRepresentationInfo(domDocument);
        var albumCover = new AlbumCover() {Cover = cover};
        var albumType = GetAlbumType(domDocument);

        var songs = GetSongs(domDocument).ToList();
        var genres = GetGenres(domDocument).ToList();
        var artists = GetArtists(domDocument).ToList();

        return new Album()
        {
            Artists = artists,
            Title = title,
            YearReleased = yearReleased,
            AlbumType = albumType,
            Songs = songs,
            Genres = genres,
            AlbumCover = albumCover,
            OnlineUrl = uri
        };
    }

    private static (string, int) GetRepresentationInfo(IHtmlDocument htmlDocument)
    {
        var header = htmlDocument
            .QuerySelectorAll("h1")
            .Select(x => x.InnerHtml)
            .First();
        if (header.Contains('-'))
        {
            var result = header.Split(new[] { " - ", "(", ")" }, StringSplitOptions.RemoveEmptyEntries);
            return (result[1], int.Parse(result[2]));
        }
        else
        {
            var result = header.Split(new[] { "(", ")" }, StringSplitOptions.RemoveEmptyEntries);
            return (result[0], int.Parse(result[1]));
        }
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

    private static IEnumerable<Artist> GetArtists(IHtmlDocument htmlDocument) =>
        htmlDocument
            .QuerySelectorAll("a")
            .Where(s => s.HasAttribute("rel"))
            .Select(x => x.InnerHtml)
            .Where(i => i != string.Empty && i != "Flash plugin")
            .Distinct()
            .Select(x => new Artist() {Name = x});

    private static IEnumerable<Song> GetSongs(IHtmlDocument htmlDocument)
    {
        var songNames = htmlDocument
            .QuerySelectorAll("a")
            .Where(s => s.ClassName == "strong")
            .Select(x => x.InnerHtml);

        var durations = htmlDocument.QuerySelectorAll("div")
            .Where(d => d.ClassName == "track__details hidden-xs-down" && d.FirstElementChild?.ClassName == "text-muted")
            .Select(x => x.FirstElementChild?.InnerHtml)
            .Select(TimeHelper.ToTimeSpan!);

        var onlineUrls = htmlDocument.QuerySelectorAll("a")
            .Where(p => p.HasAttribute("download"))
            .Select(x => "https://musify.club" + x.GetAttribute("href"));

        foreach (var songInfo in songNames.Zip(durations).Zip(onlineUrls))
        {
            yield return new Song()
            {
                Duration = songInfo.First.Second,
                Title = songInfo.First.First,
                OnlineUrl = songInfo.Second
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