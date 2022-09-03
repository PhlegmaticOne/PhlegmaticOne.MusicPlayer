using PhlegmaticOne.MusicPlayer.UI.WPF.Properties;
using PhlegmaticOne.PlayerService.Base;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.Services.Player;

public class PlayerVolumeService : IPlayerVolumeService
{
    public float GetVolume()
    {
        return (float)Settings.Default.SavedVolume;
    }

    public void SetVolume(float volume)
    {
        Settings.Default.SavedVolume = volume;
        Settings.Default.Save();
    }
}