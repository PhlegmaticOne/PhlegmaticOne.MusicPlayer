namespace PhlegmaticOne.MusicPlayer.Entities.Base;

public class EntityBase
{
    public Guid Id { get; set; }
    public override bool Equals(object? obj)
    {
        if (obj is EntityBase entityBase)
        {
            return entityBase.Id == Id;
        }

        return false;
    }

    public override int GetHashCode() => Id.GetHashCode();
}