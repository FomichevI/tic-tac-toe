using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MatchResult {Win, Lose, Draw}

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SessionConfig", order = 1)]
public class SessionConfig : ScriptableObject
{
    public PlayerData Player;
    public PlayerData Opponent;
    public bool IsOpponentBot = false;
    public BotConfig CurrentBot;
    [Header("Match results")]
    public MatchResult MatchResult;
    public string OpponentName;
    public float Duration;


    public void SetLastMatchResult(MatchResult result, float duration)
    {
        MatchResult = result;
        OpponentName = Opponent.Name;
    }

}
