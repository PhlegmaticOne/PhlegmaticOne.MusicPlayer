namespace PhlegmaticOne.MusicPlayer.UI.WPF.Helpers;

public class SortDescription
{
    public SortDescription(SortType sortType, string sortName)
    {
        SortType = sortType;
        SortName = sortName;
    }

    public SortType SortType { get; set; }
    public string SortName { get; set; }
}