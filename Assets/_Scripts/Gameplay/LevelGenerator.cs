using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����� ��� ��������� ������ � ����������� �� ������� ������. �� ������ ������ ���������� ������ ������� ������ ������
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
        // ����� ��������� ��������� ������ �������� ������������� ��������
        _gameplay.Initialise(_gridPrefab);
    }
}
