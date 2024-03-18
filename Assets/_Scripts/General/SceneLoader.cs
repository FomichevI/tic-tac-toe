using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Синглтон, отвечающий за переключение между сценами
/// </summary>
public class SceneLoader : MonoBehaviour
{
    [SerializeField] private LoadingScreen _loadingScreen;
    [SerializeField] private string _gameplaySceneName;
    [SerializeField] private string _menuSceneName;

    void Start()
    {
        LoadGameplayScene();
    }

    public void LoadGameplayScene()
    { 
        if (SceneManager.GetActiveScene().name == _gameplaySceneName)
            return;
        SceneManager.LoadSceneAsync(1);
    }

}
