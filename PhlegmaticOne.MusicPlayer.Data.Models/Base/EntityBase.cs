using PhlegmaticOne.MusicPlayer.Contracts.Abstractions;

namespace PhlegmaticOne.MusicPlayer.Data.Models.Base;

public class EntityBase : IHaveId
{
    public Guid Id { get; set; }
}