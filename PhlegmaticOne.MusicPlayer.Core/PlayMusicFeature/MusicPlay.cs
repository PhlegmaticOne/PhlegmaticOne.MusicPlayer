using NAudio.Wave;

namespace PhlegmaticOne.MusicPlayer.Core.PlayMusicFeature;

public class MusicPlay
{
    public static void PlayFromUri(string uri)
    {
        Task.Run(() =>
        {
            using var mf = new MediaFoundationReader(uri);
            using var wo = new WasapiOut();
            wo.Init(mf);
            wo.Play();
            wo.Volume = 0.5f;
            while (wo.PlaybackState == PlaybackState.Playing)
            {
                Thread.Sleep(1000);
            }
        });
    }
}