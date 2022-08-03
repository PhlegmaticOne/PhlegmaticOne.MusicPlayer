using NAudio.Wave;


using var mr = new MediaFoundationReader(@"https://musify.club/track/dl/393057/austere-down.mp3");
using var ws = new WasapiOut();
ws.Volume = 0.5f;
ws.Init(mr);
ws.Play();

while (ws.PlaybackState == PlaybackState.Playing)
{
    Console.ReadKey();
    mr.Position = 0;
}