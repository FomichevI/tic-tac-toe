using UnityEngine;

/// <summary>
/// Класс отвечающий за инициализацию уровня и запуск игры
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
        // Определяем тип гейплея
        if (GameManager.Instance.SessionConfig.IsOpponentBot)
            _currentGameplay = _gameplayWithBot;
        else
            _currentGameplay = _simpleGameplay;

        // Настраиваем все связи и запускаем необходимые инициализации
        Debug.Log("Инициализация уровня...");
        _uiGameplay.Initialize(_currentGameplay);
        _levelGenerator.Initialize(_currentGameplay);
    }
}
