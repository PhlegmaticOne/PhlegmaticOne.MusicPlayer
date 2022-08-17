namespace PhlegmaticOne.MusicPlayer.Contracts.Services.Player;

public interface IPlayerVolumeService
{
    public float GetVolume();
    public void SetVolume(float volume);
}