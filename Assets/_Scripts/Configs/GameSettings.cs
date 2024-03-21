using UnityEngine;

/// <summary>
///  ласс содержит основные настройки, вли€ющие на игровой геймплей
/// </summary>
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/GameSettings", order = 1)]
public class GameSettings : ScriptableObject
{
    [SerializeField] private BotConfig[] _botConfigs; public BotConfig[] BotConfigs { get { return _botConfigs; } }
    [Header("Reward")]
    [SerializeField] private int _rewardForWin = 30;
    [SerializeField] private int _rewardForLose = -15;
    [SerializeField] private int _rewardForDraw = 5;

    public int GetReward(MatchResult matchResult)
    {
        switch (matchResult)
        {
            case MatchResult.Win:
                return _rewardForWin;
            case MatchResult.Lose:
                return _rewardForLose;
            default: 
                return _rewardForDraw;
        }
    }

    public BotConfig GetBotConfig(BotType botType)
    {
        foreach (BotConfig botConfig in BotConfigs)
        {
            if (botConfig.type == botType)
                return botConfig; 
        }
        return null;
    }
}
