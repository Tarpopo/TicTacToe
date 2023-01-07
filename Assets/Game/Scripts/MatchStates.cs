public static class MatchStates
{
    public static bool Free => _matchState == MatchState.Free;
    private static MatchState _matchState = MatchState.Free;
    private static int _playerId;

    public static void SetMatchEnded() => _matchState = MatchState.MatchEnded;
    public static void SetMatchFree() => _matchState = MatchState.Free;
}

public enum MatchState
{
    Free,
    MatchEnded
}