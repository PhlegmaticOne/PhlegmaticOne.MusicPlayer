using PhlegmaticOne.MusicPlayer.WPF.Core.ViewModels;

namespace PhlegmaticOne.MusicPlayer.Contracts.EntityViewModels.Base;

public class ArtistBaseViewModel : EntityBaseViewModel
{
    public string Name { get; set; }

    public override string ToString() => Name;
}