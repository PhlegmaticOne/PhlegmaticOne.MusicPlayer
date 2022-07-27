using AngleSharp.Html.Parser;

var httpClient = new HttpClient();
var url = @"https://musify.club/release/ordo-templi-orientis-the-distances-of-cold-2010-105696";
var html = await httpClient.GetStringAsync(url);
var htmlDocument = new HtmlParser().ParseDocument(html);

var artistNames = htmlDocument
    .QuerySelectorAll("a")
    .Where(s => s.HasAttribute("rel"))
    .SkipLast(1)
    .Select(x => x.InnerHtml)
    .Distinct();

foreach (var artistName in artistNames)
{
    Console.WriteLine(artistName);
}
