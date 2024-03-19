using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����� ���������� �� ������������� ������ � ������ ����
/// </summary>
public class GameController : MonoBehaviour
{
    [SerializeField] private Gameplay _simpleGameplay;
    [SerializeField] private GameplayWithBot _gameplayWithBot;
    [SerializeField] private LevelGenerator _levelGenerator;
    [SerializeField] private UiGameplay _uiGameplay;
    private Gameplay _currentGameplay;

    void Start()
    {
        // ���������� ��� �������
        if (GameManager.Instance.SessionConfig.IsOpponentBot)
            _currentGameplay = _gameplayWithBot;
        else
            _currentGameplay = _simpleGameplay;

        // ����������� ��� ����� � ��������� ����������� �������������
        Debug.Log("������������� ������...");
        _uiGameplay.Initialize(_currentGameplay);
        _levelGenerator.Initialize(_currentGameplay);
    }

    private void TryStartGame()
    {
        bool isAllInitializeCompleted = false;
        // ���������� ��������� �������� ��������� ������������� ���������� � ��������
        StartCoroutine(TryStartGameCo());

        IEnumerator TryStartGameCo()
        {
            while (!isAllInitializeCompleted)
            {
                yield return new WaitForSeconds(0.2f);
                isAllInitializeCompleted = _currentGameplay.IsInitialized;
            }
            _currentGameplay.StartGame();
        }
    }
}
