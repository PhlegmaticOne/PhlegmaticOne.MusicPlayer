using System.Drawing;
using PhlegmaticOne.MusicPlayer.Contracts.Abstractions;
using PhlegmaticOne.WPF.Core.ViewModels;

namespace PhlegmaticOne.MusicPlayer.Models;

public class CollectionLinkViewModel : EntityBaseViewModel, IHaveTitle
{
    public string Title { get; set; }
    public Bitmap Cover { get; set; }
}