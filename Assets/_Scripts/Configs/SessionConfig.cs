using UnityEngine;

public enum MatchResult {Win, Lose, Draw}
/// <summary>
/// Класс, хранящей в себе своеобразный КЭШ игры
/// </summary>
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SessionConfig", order = 1)]
public class SessionConfig : ScriptableObject
{
    [SerializeField] private PlayerInfo _player; public PlayerInfo Player { get { return _player; } } // Поле,хранящее в себе информацию об игроке
    private PlayerInfo _opponent; public PlayerInfo Opponent { get { return _opponent; } } // Поле,хранящее в себе информацию о сопернике
    private bool _isOpponentBot; public bool IsOpponentBot { get { return _isOpponentBot; } }
    private BotConfig _currentBot; public BotConfig CurrentBot { get { return _currentBot; } }

    [Header("Match results")] // Если в будущем хотим хранить больше параметров матча, можно перенести все в отдельный класс
    private MatchResult _matchResult; public MatchResult MatchResult { get { return _matchResult; } }
    private string _opponentName = ""; public string OpponentName { get { return _opponentName; } }
    private float _duration = 0; public float Duration { get { return _duration; } }

    /// <summary>
    /// Сохранить параметры прошедшего матча в кэш
    /// </summary>
    public void SetLastMatchResult(MatchResult result, float duration)
    {
        _matchResult = result;
        _opponentName = Opponent.Name;
        _duration = duration;
    }

    /// <summary>
    /// Сохранить информацию о следующем противнике
    /// </summary>
    public void SetNextOpponent(PlayerInfo opponent)
    {
        _opponent = opponent;
        _isOpponentBot = false;
    }

    /// <summary>
    /// Сохранить информацию о следующем противнике-боте
    /// </summary>
    public void SetNextOpponent(BotConfig bot)
    {
        _currentBot = bot;
        _opponent.Name = CurrentBot.Name;
        _isOpponentBot = true;
    }
}
