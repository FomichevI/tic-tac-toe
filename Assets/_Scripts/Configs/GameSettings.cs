using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
}
