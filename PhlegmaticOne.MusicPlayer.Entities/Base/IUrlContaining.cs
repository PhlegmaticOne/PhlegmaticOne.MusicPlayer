namespace PhlegmaticOne.MusicPlayer.Entities.Base;

public interface IUrlContaining
{
    string LocalUrl { get; set; }
    string OnlineUrl { get; set; }
}