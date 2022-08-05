using System;
using System.Collections.Generic;
using PhlegmaticOne.MusicPlayer.Entities;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.PlayerHelpers;

public interface ISongsQueue
{
    public IReadOnlyCollection<Song> Songs { get; }
    public event EventHandler<IEnumerable<Song>> QueueChanged; 
    public void Enqueue(Song song);
    public void Enqueue(IEnumerable<Song> songs);
    public void Remove(Song song);
    public Song? Current { get; set; }
    public void MoveNext();
    public void Clear();
}