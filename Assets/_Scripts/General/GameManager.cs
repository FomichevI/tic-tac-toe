using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��������, �������� � ���� ��� �������� ��������� � ������������ ����.
/// </summary>
// ����� ��������� ���������� ����, ������� � ������� ����� ��������� � ��������� �����
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public SessionConfig SessionConfig;
    public ItemsConfig ItemsConfig;
    public GameSettings GameSettings;
    public DataManager DataManager;

    private void Awake()
    {
        if (Instance == null)        
            Instance = this;        
    }

    public void SetLastMatchResult(MatchResult result, float duration)
    {
        SessionConfig.SetLastMatchResult(result, duration);
        DataManager.RiseScore(GameSettings.GetReward(result));
    }

}
