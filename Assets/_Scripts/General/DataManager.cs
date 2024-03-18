using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  ласс, отвечающий за сохранение и загрузку данных
/// </summary>
public class DataManager : MonoBehaviour
{
    private string _currentScoreKey = "CurrentScoreKey";

    public int GetCurrentScore()
    {
        return PlayerPrefs.GetInt(_currentScoreKey, 0);
    }

    public void RiseScore(int scoreDelta)
    {
        int current = PlayerPrefs.GetInt(_currentScoreKey, 0);
        current += scoreDelta;
        PlayerPrefs.SetInt(_currentScoreKey, current > 0? current : 0);
    }

}
