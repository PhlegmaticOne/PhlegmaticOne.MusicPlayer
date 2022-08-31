using System.Drawing;

namespace PhlegmaticOne.MusicPlayer.Contracts.Services.Logo;

public interface ILogoProvider
{
    Bitmap GetApplicationLogo();
}