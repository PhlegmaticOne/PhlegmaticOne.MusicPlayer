﻿using PhlegmaticOne.MusicPlayer.Entities.Base;

namespace PhlegmaticOne.MusicPlayer.Entities;

public class Album : EntityBase
{
    public ICollection<Artist> Artists { get; set; } = null!;
    public ICollection<Song> Songs { get; set; } = null!;
    public ICollection<Genre> Genres { get; set; } = null!;
    public AlbumCover AlbumCover { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string OnlineUrl { get; set; } = null!;
    public int YearReleased { get; set; }
    public AlbumType AlbumType { get; init; }
    public override string ToString() => $"{string.Join("/", Artists.Select(x => x.Name))} - {Title} ({YearReleased})";
}