using PhlegmaticOne.MusicPlayer.Core.Player;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.Players;

public class PlayersFactory
{
    private readonly ReactivePlayer _player;

    public PlayersFactory(IPlayer player)
    {
        _player = new ReactivePlayer(player);
    }

    public IPlayer CreatePlayer() => _player;
}