namespace PhlegmaticOne.MusicPlayer.Entities;

public class Album : CollectionBase
{
    public ICollection<Genre> Genres { get; set; } = null!;
    public ICollection<Artist> Artists { get; set; } = null!;
    public string OnlineUrl { get; set; } = null!;
    public int YearReleased { get; set; }
    public AlbumType AlbumType { get; init; }
    public override string ToString() => $"{string.Join("/", Artists.Select(x => x.Name))} - {Title} ({YearReleased})";

}