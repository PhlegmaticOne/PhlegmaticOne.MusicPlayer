using PhlegmaticOne.MusicPlayer.WPF.Core;

namespace PhlegmaticOne.MusicPlayer.Contracts.ViewModels;

public class CollectionLinkViewModel : BaseViewModel
{
    public Guid Id { get; set; }
    public string Title { get; set; }
}