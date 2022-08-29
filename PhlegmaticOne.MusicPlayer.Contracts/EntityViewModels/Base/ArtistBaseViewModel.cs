using PhlegmaticOne.MusicPlayer.Entities.Base;
using PhlegmaticOne.MusicPlayer.WPF.Core.ViewModels;

namespace PhlegmaticOne.MusicPlayer.Contracts.EntityViewModels.Base;

public class ArtistBaseViewModel : EntityBaseViewModel, IHaveId, IIsFavorite
{
    public string Name { get; set; }
    public bool IsFavorite { get; set; }
    public override string ToString() => Name;
}