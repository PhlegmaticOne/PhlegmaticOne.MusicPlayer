using PhlegmaticOne.WPF.Core.ViewModels;

namespace PhlegmaticOne.MusicPlayer.Contracts.Services.Cache;

public class CacheService : ICacheService
{
    private readonly Dictionary<ViewModelDescriptor, BaseViewModel> _viewModels;
    public CacheService()
    {
        _viewModels = new();
    }

    public bool ContainsKey<T>(Guid id)
    {
        var viewModelDescriptor = new ViewModelDescriptor(id, typeof(T));
        return _viewModels.ContainsKey(viewModelDescriptor);
    }

    public bool TryGetCachedValue<T>(Guid id, out T viewModel) where T : BaseViewModel
    {
        var viewModelDescriptor = new ViewModelDescriptor(id, typeof(T));
        if (_viewModels.TryGetValue(viewModelDescriptor, out var result))
        {
            viewModel = (T)result;
            return true;
        }

        viewModel = null;
        return false;
    }

    public void Set<T>(Guid id, T viewModel) where T : BaseViewModel
    {
        var viewModelDescriptor = new ViewModelDescriptor(id, typeof(T));
        _viewModels.TryAdd(viewModelDescriptor, viewModel);
    }

    public ICollection<T> GetAllCached<T>() where T : BaseViewModel
    {
        return _viewModels.Where(x => x.Key.Type == typeof(T)).Select(x => (T)x.Value).ToList();
    }

    private class ViewModelDescriptor : IEquatable<ViewModelDescriptor>
    {
        internal readonly Guid Id;
        internal readonly Type Type;

        public ViewModelDescriptor(Guid id, Type type)
        {
            Id = id;
            Type = type;
        }

        public bool Equals(ViewModelDescriptor? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id.Equals(other.Id) && Type == other.Type;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ViewModelDescriptor) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Type);
        }
    }
}