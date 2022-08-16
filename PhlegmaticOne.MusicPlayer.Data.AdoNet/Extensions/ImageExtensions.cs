using System.Drawing;

namespace PhlegmaticOne.MusicPlayer.Data.AdoNet.Extensions;

public static class ImageExtensions
{
    public static Bitmap ToBitmap(this byte[] array)
    {
        using var ms = new MemoryStream(array);
        return new Bitmap(ms);
    }
}