using Calabonga.UnitOfWork;
using PhlegmaticOne.WPF.Core.ViewModels;

namespace PhlegmaticOne.MusicPlayer.Contracts.Services.PagedList;

public class EntityPagedListGetService : IEntityPagedListGetService
{
    private readonly IDictionary<Type, IEntityPagedListGet> _viewModelGetters;

    public EntityPagedListGetService(IDictionary<Type, IEntityPagedListGet> viewModelGetters)
    {
        _viewModelGetters = viewModelGetters;
    }
    public async Task<IPagedList<T>> GetPagedListAsync<T>(int pageSize, int pageIndex) where T : EntityBaseViewModel
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

        var obj = await getter.GetPagedList(pageSize, pageIndex);
        return obj as IPagedList<T>;
    }
}