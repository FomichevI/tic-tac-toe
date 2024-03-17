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

    private void Awake()
    {
        if (Instance == null)        
            Instance = this;        
    }

}
