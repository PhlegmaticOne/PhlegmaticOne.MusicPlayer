using System.Windows.Input;
using PhlegmaticOne.WPF.Core.ViewModels;

namespace PhlegmaticOne.MusicPlayer.Contracts.Actions;

public interface IEntityActionsProvider<in T> where T: EntityBaseViewModel
{
    IDictionary<string, ICommand> GetActions(T entity);
}