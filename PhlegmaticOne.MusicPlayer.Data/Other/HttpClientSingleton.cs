namespace PhlegmaticOne.MusicPlayer.Data.Other;

internal class HttpClientSingleton
{
    private static HttpClient? _httpClient;
    internal static HttpClient Instance => _httpClient ??= new();
}