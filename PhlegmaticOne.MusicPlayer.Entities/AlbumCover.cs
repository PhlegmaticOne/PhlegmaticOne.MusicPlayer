﻿using System.Drawing;
using PhlegmaticOne.MusicPlayer.Entities.Base;

namespace PhlegmaticOne.MusicPlayer.Entities;

public class AlbumCover : EntityBase
{
    public Guid AlbumId { get; set; }
    public Album Album { get; set; }
    public Bitmap Cover { get; set; }
}