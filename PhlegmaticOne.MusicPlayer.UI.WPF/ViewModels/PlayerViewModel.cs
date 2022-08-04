using System;
using PhlegmaticOne.MusicPlayer.Core.Player;
using PhlegmaticOne.MusicPlayer.Entities;
using PhlegmaticOne.MusicPlayer.UI.WPF.Commands;
using PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels.Base;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels;

public class PlayerViewModel : BaseViewModel
{
    public IPlayer Player { get; set; }
    public TimeSpan CurrentTime { get; set; }
    public Song CurrentSong { get; set; }
    public Album CurrentAlbum { get; set; }
    public bool IsPaused { get; set; } = true;
    public bool IsStopped { get; set; } = true;
    public PlayerViewModel(IPlayer player)
    {
        Player = player;
        RewindCommand = new(Rewind, _ => true);
        PlayPauseCommand = new(PlayPause, _ => true);
        player.TimeChanged += PlayerOnTimeChanged;
        player.PauseChanged += PlayerOnPauseChanged;
        player.StopChanged += PlayerOnStopChanged;
    }

    private void PlayerOnStopChanged(object? sender, bool stopped)
    {
        IsStopped = stopped;
    }

    private void PlayerOnPauseChanged(object? sender, bool paused)
    {
        IsPaused = paused;
    }

    private void PlayerOnTimeChanged(object? sender, TimeSpan newTime)
    {
        CurrentTime = newTime;
    }

    public DelegateCommand RewindCommand { get; set; }
    public DelegateCommand PlayPauseCommand { get; set; }
    private void Rewind(object? parameter)
    {
        if (parameter is double ticks)
        {
            Player.Rewind(ParseTime(ticks));
        }
    }

    private void PlayPause(object? parameter)
    {
        Player.PauseOrUnpause();
    }
    private TimeSpan ParseTime(double value) => TimeSpan.FromTicks(Convert.ToInt64(value));
}