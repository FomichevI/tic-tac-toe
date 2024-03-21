using UnityEngine;

public enum MatchResult {Win, Lose, Draw}
/// <summary>
/// �����, �������� � ���� ������������ ��� ����
/// </summary>
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SessionConfig", order = 1)]
public class SessionConfig : ScriptableObject
{
    [SerializeField] private PlayerInfo _player; public PlayerInfo Player { get { return _player; } } // ����,�������� � ���� ���������� �� ������
    private PlayerInfo _opponent; public PlayerInfo Opponent { get { return _opponent; } } // ����,�������� � ���� ���������� � ���������
    private bool _isOpponentBot; public bool IsOpponentBot { get { return _isOpponentBot; } }
    private BotConfig _currentBot; public BotConfig CurrentBot { get { return _currentBot; } }

    [Header("Match results")] // ���� � ������� ����� ������� ������ ���������� �����, ����� ��������� ��� � ��������� �����
    private MatchResult _matchResult; public MatchResult MatchResult { get { return _matchResult; } }
    private string _opponentName = ""; public string OpponentName { get { return _opponentName; } }
    private float _duration = 0; public float Duration { get { return _duration; } }

    /// <summary>
    /// ��������� ��������� ���������� ����� � ���
    /// </summary>
    public void SetLastMatchResult(MatchResult result, float duration)
    {
        _matchResult = result;
        _opponentName = Opponent.Name;
        _duration = duration;
    }

    /// <summary>
    /// ��������� ���������� � ��������� ����������
    /// </summary>
    public void SetNextOpponent(PlayerInfo opponent)
    {
        _opponent = opponent;
        _isOpponentBot = false;
    }

    /// <summary>
    /// ��������� ���������� � ��������� ����������-����
    /// </summary>
    public void SetNextOpponent(BotConfig bot)
    {
        _currentBot = bot;
        _opponent.Name = CurrentBot.Name;
        _isOpponentBot = true;
    }
}
