namespace PhlegmaticOne.MusicPlayer.Data.Models;

public class Playlist : CollectionBase, IEquatable<Playlist>
{
    public bool Equals(Playlist? other) => Title == other?.Title;

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Playlist) obj);
    }

    public override int GetHashCode() => HashCode.Combine(Title);
}