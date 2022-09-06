using System.Drawing;

namespace PhlegmaticOne.MusicPlayer.Contracts.Services.Cache;

public interface ICacheService<T>
{
    ICollection<Guid> ExistingKeys { get; }
    bool ContainsKey(Guid id);
    bool TryGetCachedValue(Guid id, out T value);
    void Set(Guid id, T value);
    Bitmap Get(Guid id);
}