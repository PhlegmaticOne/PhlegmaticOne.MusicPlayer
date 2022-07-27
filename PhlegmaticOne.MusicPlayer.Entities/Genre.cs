using PhlegmaticOne.MusicPlayer.Entities.Base;

namespace PhlegmaticOne.MusicPlayer.Entities;

public class Genre : EntityBase
{
    public string Name { get; set; } = null!;
    public ICollection<Album> Albums { get; set; } = null!;
}