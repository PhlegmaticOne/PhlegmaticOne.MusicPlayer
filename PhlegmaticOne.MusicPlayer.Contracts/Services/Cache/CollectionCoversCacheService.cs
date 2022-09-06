using System.Drawing;

namespace PhlegmaticOne.MusicPlayer.Contracts.Services.Cache;

public class CollectionCoversCacheService : ICacheService<Bitmap>
{
    private readonly Dictionary<Guid, Bitmap> _cache = new();

    public ICollection<Guid> ExistingKeys => _cache.Keys;
    public bool ContainsKey(Guid id) => _cache.ContainsKey(id);

    public bool TryGetCachedValue(Guid id, out Bitmap value) => _cache.TryGetValue(id, out value);

    public void Set(Guid id, Bitmap value) => _cache.TryAdd(id, value);
    public Bitmap Get(Guid id) => _cache[id];
}