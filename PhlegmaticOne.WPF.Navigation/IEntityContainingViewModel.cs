using PhlegmaticOne.MusicPlayer.WPF.Core;

namespace PhlegmaticOne.WPF.Navigation;

public interface IEntityContainingViewModel<T> where T : EntityBaseViewModel
{
    T Entity { get; set; }
}