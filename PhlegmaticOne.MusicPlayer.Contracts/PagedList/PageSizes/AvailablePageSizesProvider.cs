namespace PhlegmaticOne.MusicPlayer.Contracts.PagedList.PageSizes;

public class AvailablePageSizesProvider : IAvailablePageSizesProvider
{
    public IList<int> GetAvailablePageSizes() => new List<int> {10, 20, 30, 50, 100};
    public int InitialPageSize => 30;
}