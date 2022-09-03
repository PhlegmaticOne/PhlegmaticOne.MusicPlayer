using PhlegmaticOne.MusicPlayer.Contracts.Base;
using PhlegmaticOne.WPF.Core.ViewModels;

namespace PhlegmaticOne.MusicPlayer.Contracts.Models.Base;

public class ArtistBaseViewModel : EntityBaseViewModel, IHaveId, IIsFavorite, IHaveTitle
{
    public string Title { get; set; }
    public bool IsFavorite { get; set; }
    public override string ToString() => Title;
}