namespace PhlegmaticOne.MusicPlayer.Players;

internal class HttpClientSingleton
{
    private static HttpClient? _httpClient;
    internal static HttpClient Instance => _httpClient ??= new HttpClient();
}