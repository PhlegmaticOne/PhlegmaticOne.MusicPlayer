namespace PhlegmaticOne.MusicPlayer.Data.EntityFramework.Other;

internal class HttpClientSingleton
{
    private static HttpClient? _httpClient;
    internal static HttpClient Instance => _httpClient ??= new();
}