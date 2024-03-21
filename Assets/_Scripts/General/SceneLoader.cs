using UnityEngine;

/// <summary>
/// Синглтон, отвечающий за переключение между сценами
/// </summary>
public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;

    [SerializeField] private LoadingScreen _loadingScreen;
    [SerializeField] private string _gameplaySceneName;
    [SerializeField] private string _menuSceneName;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
#if !UNITY_EDITOR
        LoadMenuScene();
#endif
    }

    public void LoadGameplayScene()
    {
        _loadingScreen.LoadScene(_gameplaySceneName);
    }

    public void LoadMenuScene()
    {
        _loadingScreen.LoadScene(_menuSceneName);
    }
}
