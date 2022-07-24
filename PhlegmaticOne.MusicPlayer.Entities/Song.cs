﻿using PhlegmaticOne.MusicPlayer.Entities.Base;

namespace PhlegmaticOne.MusicPlayer.Entities;

public class Song : EntityBase
{
    public string Title { get; set; } = null!;
    public TimeSpan Duration { get; set; }
    public ICollection<Album> AlbumAppearances { get; set; } = null!;
    public string LocalUrl { get; set; }
    public string OnlineUrl { get; set; }
    public override string ToString() => $"{Title} - {Duration:g}";
}