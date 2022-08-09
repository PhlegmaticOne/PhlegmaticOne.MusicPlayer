using AngleSharp.Html.Parser;

namespace PhlegmaticOne.MusicPlayer.Players.DownloadSongsFeature
{
    public class Helpers
    {
        static async Task<IEnumerable<(string, string)>> Get(string url)
        {
            var client = new HttpClient();
            var source = await client.GetStringAsync(url);
            var parser = new HtmlParser();
            var domDocument = await parser.ParseDocumentAsync(source);
            var uri = domDocument.QuerySelectorAll("a")
                .Where(p => p.HasAttribute("download"))
                .Select(x => ("https://musify.club" + x.GetAttribute("href"), x.GetAttribute("download")));
            return uri;
        }

        static async Task DownloadSong(string uri, string name)
        {
            var httpClient = new HttpClient();
            var songBytes = await httpClient.GetByteArrayAsync(uri);
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            var localPath = Path.Combine(documentsPath, name);
            await File.WriteAllBytesAsync(localPath, songBytes);
        }
    }
}
