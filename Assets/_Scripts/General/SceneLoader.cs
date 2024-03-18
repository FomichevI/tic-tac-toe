using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public void LoadStartScene()
    {
        //LoadGameplayScene();
    }

    public void LoadGameplayScene()
    { 
        SceneManager.LoadSceneAsync(_gameplaySceneName);
    }

    public void LoadMenuScene()
    {
        SceneManager.LoadSceneAsync(_menuSceneName);
    }
}
