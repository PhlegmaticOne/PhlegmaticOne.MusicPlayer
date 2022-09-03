namespace PhlegmaticOne.PlayerService.Base;

public interface IHaveUrl
{
    string LocalUrl { get; }
    string OnlineUrl { get; }
}