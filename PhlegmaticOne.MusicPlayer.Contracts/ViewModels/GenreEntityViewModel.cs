using PhlegmaticOne.MusicPlayer.WPF.Core;

namespace PhlegmaticOne.MusicPlayer.Contracts.ViewModels;

public class GenreEntityViewModel : BaseViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}