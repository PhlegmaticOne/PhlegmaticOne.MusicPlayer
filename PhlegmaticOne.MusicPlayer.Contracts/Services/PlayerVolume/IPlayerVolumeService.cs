namespace PhlegmaticOne.MusicPlayer.Contracts.Services.PlayerVolume;

public interface IPlayerVolumeService
{
    float GetVolume();
    void SetVolume(float volume); 
}
