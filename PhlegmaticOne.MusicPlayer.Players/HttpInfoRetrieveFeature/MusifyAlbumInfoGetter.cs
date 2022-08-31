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
        _httpClient = HttpClientSingleton.Instance;
    }
    public async Task<Album> GetInfoAsync(string uri)
    {
        var source = await _httpClient.GetStringAsync(uri);
        var parser = new HtmlParser();
        var domDocument = await parser.ParseDocumentAsync(source);

        var cover = await GetAlbumCover(domDocument);
        var (title, yearReleased) = GetRepresentationInfo(domDocument);
        var albumCover = new AlbumCover {Cover = cover};
        var albumType = GetAlbumType(domDocument);

        var songs = GetSongs(domDocument).ToList();
        var genres = GetGenres(domDocument).ToList();
        var artists = songs.SelectMany(x => x.Artists).Distinct().ToList();

        return new Album
        {
            Artists = artists,
            Title = title,
            YearReleased = yearReleased,
            AlbumType = albumType,
            Songs = songs,
            Genres = genres,
            AlbumCover = albumCover,
        };
    }

    private static (string, int) GetRepresentationInfo(IHtmlDocument htmlDocument)
    {
        var header = htmlDocument
            .QuerySelectorAll("h1")
            .Select(x => x.InnerHtml)
            .First()
            .Replace("&amp;", "&");
        var firstDashIndex = header.IndexOf('-');
        var titleInfo = header.Substring(firstDashIndex + 2, header.Length - firstDashIndex - 2);
        var splitted = titleInfo.Split(new[]{'(', ')'}, StringSplitOptions.RemoveEmptyEntries);
        return (splitted[0], int.Parse(splitted[1]));
    }

    private static AlbumType GetAlbumType(IHtmlDocument htmlDocument)
    {
        var albumType = htmlDocument
            .QuerySelectorAll("i")
            .Where(s => s.ClassName == "zmdi zmdi-collection-music zmdi-hc-fw")
            .Select(x => x.ParentElement!.TextContent)
            .First()
            .Trim();
        return ParseStringToAlbumType(albumType);
    }

    private static IEnumerable<Song> GetSongs(IHtmlDocument htmlDocument)
    {
        var songArtists = htmlDocument
            .QuerySelectorAll("a")
            .Where(x => x.Attributes.Any(y => y.Name == "rel") && x.InnerHtml != "Flash plugin")
            .Select(x => x.InnerHtml);

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

        var result = new List<Song>();
        foreach (var songInfo in (songArtists.Zip(songNames)).Zip(durations.Zip(onlineUrls)))
        {
            var song = new Song
            {
                Title = songInfo.First.Second,
                Duration = songInfo.Second.First,
                OnlineUrl = songInfo.Second.Second
            };
            var newSongArtist = songInfo.First.First;
            var existingSongWithArtist = result.FirstOrDefault(x => x.Artists.Any(y => y.Name == newSongArtist));
            if (existingSongWithArtist is not null)
            {
                song.Artists = existingSongWithArtist.Artists;
            }
            else
            {
                song.Artists = new List<Artist>()
                {
                    new()
                    {
                        Name = songInfo.First.First,
                        Songs = new List<Song>()
                    },
                };
            }
            result.Add(song);
        }

        return result;
    }



    private static AlbumType ParseStringToAlbumType(string albumType) => albumType switch
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