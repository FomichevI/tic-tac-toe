using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс для генерации уровня в зависимости от вводных данных. На данный момент использует просто готовый префаб уровня
/// </summary>
public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private GridPrefab _gridPrefab;
    private Gameplay _gameplay;

    public void Initialize(Gameplay gameplay)
    {
        _gameplay = gameplay;
        GenerateLevel();
    }

    private void GenerateLevel()
    {
        // После окончания генерации уровня начинаем инициализацию геймплея
        _gameplay.Initialise(_gridPrefab);
    }
}
