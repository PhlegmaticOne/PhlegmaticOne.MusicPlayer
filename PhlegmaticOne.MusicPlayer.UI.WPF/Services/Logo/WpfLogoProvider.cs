using System;
using System.Drawing;
using System.IO;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Logo;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.Services.Logo;

public class WpfLogoProvider : ILogoProvider
{
    public Bitmap GetApplicationLogo()
    {
        var baseDirectory = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
        var imagesDirectory = baseDirectory.Parent.Parent.Parent;
        var imagePath = Path.Combine(imagesDirectory.FullName, "Images", "favicon.ico");
        return (Bitmap) Image.FromFile(imagePath);
    }
}