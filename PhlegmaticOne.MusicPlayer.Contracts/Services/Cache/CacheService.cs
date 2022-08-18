using PhlegmaticOne.MusicPlayer.WPF.Core;

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

    private class ViewModelDescriptor : IEquatable<ViewModelDescriptor>
    {
        private readonly Guid _id;
        private readonly Type _type;

        public ViewModelDescriptor(Guid id, Type type)
        {
            _id = id;
            _type = type;
        }

        public bool Equals(ViewModelDescriptor? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _id.Equals(other._id) && _type == other._type;
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
            return HashCode.Combine(_id, _type);
        }
    }
}