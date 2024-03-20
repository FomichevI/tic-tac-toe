using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Синглтон, хранящий в себе все основные настройки и конфигурации игры.
/// </summary>
// Также реализует сохранение игры, которое в будущем можно перенести в отдельный класс
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
