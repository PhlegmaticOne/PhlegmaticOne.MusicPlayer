namespace PhlegmaticOne.MusicPlayer.Contracts.PagedList.PageSizes;

public interface IAvailablePageSizesProvider
{
    IList<int> GetAvailablePageSizes();
    int InitialPageSize { get; }
}