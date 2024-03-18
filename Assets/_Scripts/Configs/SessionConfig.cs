using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

public enum MatchResult {Win, Lose, Draw}

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SessionConfig", order = 1)]
public class SessionConfig : ScriptableObject
{
    public PlayerData Player;
    public PlayerData Opponent;
    public bool IsOpponentBot = false;
    public BotConfig CurrentBot;
    [Header("Match results")] // Если в будущем хотим хранить больше параметров матча, можно перенести все в отдельный класс
    public MatchResult MatchResult;
    public string OpponentName = "";
    public float Duration = 0;

    /// <summary>
    /// Сохранить параметры прошедшего матча в кэш
    /// </summary>
    public void SetLastMatchResult(MatchResult result, float duration)
    {
        MatchResult = result;
        OpponentName = Opponent.Name;
        Duration = duration;
    }

    public void SetNextOpponent(PlayerData opponent)
    {
        Opponent = opponent;
        IsOpponentBot = false;
    }

    public void SetNextOpponent(BotConfig bot)
    {
        CurrentBot = bot;
        Opponent.Name = CurrentBot.Name;
        IsOpponentBot = true;
    }
}
