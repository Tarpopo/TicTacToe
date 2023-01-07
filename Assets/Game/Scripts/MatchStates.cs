public static class MatchStates
{
    public static bool Free => _matchState == MatchState.Free;
    private static MatchState _matchState = MatchState.Free;
    private static int _playerId;

    public static void SetMatchEnded() => _matchState = MatchState.MatchEnded;
    public static void SetMatchFree() => _matchState = MatchState.Free;

    public static void SetPlayerDrawingState(BasePlayer player)
    {
        if (Free == false) return;
        _matchState = MatchState.PlayerDrawing;
        _playerId = player.GetHashCode();
    }

    public static void DisablePlayerDrawingState(BasePlayer player)
    {
        if (player.GetHashCode().Equals(_playerId) == false || _matchState != MatchState.PlayerDrawing) return;
        _matchState = MatchState.Free;
        _playerId = 0;
    }
}

public enum MatchState
{
    Free,
    PlayerDrawing,
    MatchEnded
}