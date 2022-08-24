using PhlegmaticOne.MusicPlayer.Contracts.EntityViewModels.Collections;
using PhlegmaticOne.MusicPlayer.WPF.Core.ViewModels;

namespace PhlegmaticOne.MusicPlayer.Contracts.Services.ViewModelGet;

public class ViewModelGetService : IEntityCollectionGetService
{
    private readonly IDictionary<Type, IEntityCollectionGet> _viewModelGetters;

    public ViewModelGetService(IDictionary<Type, IEntityCollectionGet> viewModelGetters)
    {
        _viewModelGetters = viewModelGetters;
    }
    public async Task<T> GetEntityCollectionAsync<T>() where T : EntityBaseViewModel, IEntityCollection
    {
        var type = typeof(T);

        var viewModelGetter = _viewModelGetters.FirstOrDefault(x =>
        {
            var generics = x.Key.GetGenericArguments();
            var generic = generics.First();
            return type.IsAssignableTo(generic);
        });

        var getter = viewModelGetter.Value;
        if (getter is null)
        {
            throw new InvalidOperationException();
        }

        var obj = await getter.Get();
        return obj as T;
    }
}