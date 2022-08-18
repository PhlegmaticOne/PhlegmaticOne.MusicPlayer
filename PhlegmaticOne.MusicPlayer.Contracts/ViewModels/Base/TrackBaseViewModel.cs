namespace PhlegmaticOne.MusicPlayer.Contracts.ViewModels.Base;

public class TrackBaseViewModel : EntityBaseViewModel
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
    public override string ToString() => $"{Title} - {Duration:g}";
    public override bool Equals(object? obj)
    {
        if (obj is TrackBaseViewModel trackBaseViewModel)
        {
            return Title == trackBaseViewModel.Title && CollectionLink.Title == trackBaseViewModel.CollectionLink.Title;
        }

        return false;
    }

    public override int GetHashCode()
    {
        return Title.GetHashCode() ^ CollectionLink.Title.GetHashCode();
    }
}