using PhlegmaticOne.MusicPlayer.Contracts.ViewModels.Base;

namespace PhlegmaticOne.MusicPlayer.Contracts.Services.ViewModelGet;

public class ViewModelGetService : IViewModelGetService
{
    private readonly IDictionary<Type, IViewModelGet> _viewModelGetters;

    public ViewModelGetService(IDictionary<Type, IViewModelGet> viewModelGetters)
    {
        _viewModelGetters = viewModelGetters;
    }
    public async Task<T> GetViewModelAsync<T>(Guid id) where T : EntityBaseViewModel
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

        var obj = await getter.Get(id);
        return obj as T;
    }
}