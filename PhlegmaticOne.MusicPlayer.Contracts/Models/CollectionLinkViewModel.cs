using System.Drawing;
using PhlegmaticOne.MusicPlayer.Contracts.Base;
using PhlegmaticOne.WPF.Core.ViewModels;

namespace PhlegmaticOne.MusicPlayer.Contracts.Models;

public class CollectionLinkViewModel : EntityBaseViewModel, IHaveTitle
{
    public string Title { get; set; }
    public Bitmap Cover { get; set; }
}