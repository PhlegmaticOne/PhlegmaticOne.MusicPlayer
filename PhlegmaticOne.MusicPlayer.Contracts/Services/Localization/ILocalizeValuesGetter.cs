namespace PhlegmaticOne.MusicPlayer.Contracts.Services.Localization;

public interface ILocalizeValuesGetter
{
    public string? GetLocalizedValue(string key);
}