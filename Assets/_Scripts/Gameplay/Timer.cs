using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

// Не уверен, что лучшее решение - объединать визуализацию и реализацию в одном классе. Как вариант: оставить визуал на UiGameplay, но в таком случае получается лишняя зависимость
// даже если осуществлять ее через поле _gameplay
public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timerTmp;
    private float _currentTime = 0; public float CurrentTime { get { return _currentTime; } }
    private bool isPlaying = false;

    public void StartTimer()
    {
        Debug.Log("[Timer] Таймер запущен");
        isPlaying = true;
    }

    public void StopTimer()
    {
        Debug.Log("[Timer] Таймер остановлен");
        isPlaying = false;
    }

    public void RefreshTimer()
    {
        Debug.Log("[Timer] Таймер обновлен");
        _currentTime = 0;
        isPlaying = false;
    }

    // Можно перенести реализацию в куратину и выполнять реже для экономии памяти, но это не критично для такой реализации
    private void FixedUpdate()
    {
        if (isPlaying)
        {
            _currentTime += Time.fixedDeltaTime;
            _timerTmp.text = NumbersConverter.GetTimeMinSec(_currentTime);
        }
    }
}
