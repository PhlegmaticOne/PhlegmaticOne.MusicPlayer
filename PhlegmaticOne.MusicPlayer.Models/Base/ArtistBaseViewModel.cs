using PhlegmaticOne.MusicPlayer.Contracts.Abstractions;
using PhlegmaticOne.WPF.Core.ViewModels;

namespace PhlegmaticOne.MusicPlayer.Models.Base;

public class ArtistBaseViewModel : EntityBaseViewModel, IHaveId, IIsFavorite, IHaveTitle
{
    public string Title { get; set; }
    public bool IsFavorite { get; set; }
    public override string ToString() => Title;
}