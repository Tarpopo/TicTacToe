using UnityEngine;

public static class MatchStates
{
    public static bool PlayerPlaying => _player != null;
    private static BasePlayer _player;

    public static void EnablePlayerPlayingState(BasePlayer player)
    {
        if (_player != null)
        {
            Debug.Log("isn't equal");
            return;
        }
        _player = player;
    }

    public static void DisablePlayerPlayingState(BasePlayer player)
    {
        if (_player != player)
        {
            Debug.Log("isn't equal");
            return;
        }

        _player = null;
    }
}