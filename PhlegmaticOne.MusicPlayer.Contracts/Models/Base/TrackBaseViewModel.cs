using System.Windows.Input;
using PhlegmaticOne.MusicPlayer.Contracts.Base;
using PhlegmaticOne.PlayerService.Base;
using PhlegmaticOne.WPF.Core.ViewModels;

namespace PhlegmaticOne.MusicPlayer.Contracts.Models.Base;

public class TrackBaseViewModel : EntityBaseViewModel, IHaveId, IIsFavorite, IHaveUrl, IIsDownloaded, IHaveDuration, IHaveTitle, IHaveTimePlayed
{
    public string Title { get; set; }
    public ICollection<ArtistLinkViewModel> ArtistLinks { get; set; }
    public CollectionLinkViewModel CollectionLink { get; set; }
    public TimeSpan Duration { get; set; }
    public TimeSpan TimePlayed { get; set; }
    public bool IsDownloading { get; set; }
    public bool IsDownloaded { get; set; }
    public bool IsFavorite { get; set; }
    public string OnlineUrl { get; set; }
    public string LocalUrl { get; set; }
    public IDictionary<string, ICommand> Actions { get; set; }
    public override string ToString() => $"{Title} - {Duration:g}";
    public override bool Equals(object? obj)
    {
        if (obj is TrackBaseViewModel trackBaseViewModel)
        {
            return Id == trackBaseViewModel.Id;
        }

        return false;
    }

    public override int GetHashCode() => Title.GetHashCode() ^ CollectionLink.Title.GetHashCode();
}