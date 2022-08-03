using System;
using PhlegmaticOne.MusicPlayer.Core.Player;
using PhlegmaticOne.MusicPlayer.Entities;
using PhlegmaticOne.MusicPlayer.UI.WPF.Base;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.Players;

public class ReactivePlayer : ObservableObject, IPlayer
{
    private readonly IPlayer _player;
    public ReactivePlayer(IPlayer player)
    {
        _player = player;
        _player.DurationChanged += PlayerOnDurationChanged;
    }

    private async void PlayerOnDurationChanged(object? sender, TimeSpan e)
    {
        await UIThreadInvoker.InvokeAsync(() => CurrentDuration = e);
    }

    public event EventHandler<TimeSpan>? DurationChanged;

    public Song CurrentSong
    {
        get => _player.CurrentSong;
        set => _player.CurrentSong = value;
    }

    public Album CurrentAlbum
    {
        get => _player.CurrentAlbum;
        set => _player.CurrentAlbum = value;
    }

    public TimeSpan CurrentDuration { get; set; }

    public void Play() => _player.Play();

    public void Pause() => _player.Pause();
}