public static class MatchStates
{
    public static bool Free => _matchState == MatchState.Free;
    private static BasePlayer _player;
    private static MatchState _matchState = MatchState.Free;
    private static int _playerIndex;

    public static void SetPlayerDrawingState(BasePlayer player)
    {
        if (Free == false) return;
        _matchState = MatchState.PlayerDrawing;
        _playerIndex = player.GetHashCode();
    }

    public static void DisablePlayerPlayingState(BasePlayer player)
    {
        if (_playerIndex.CheckHashCode(player) == false) return;
        _matchState = MatchState.Free;
    }

    private static bool CheckHashCode(this int code, BasePlayer player) => player.GetHashCode().Equals(code);
}

public enum MatchState
{
    Free,
    PlayerDrawing,
    MatchEnded
}