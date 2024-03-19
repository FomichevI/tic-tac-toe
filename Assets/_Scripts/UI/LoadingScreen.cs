using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private Animator _animator;
    [SerializeField] private TextMeshProUGUI _loadingText;
    [SerializeField] private Image _progressBarImage;

    public void LoadScene(string sceneName)
    {
        // Устанавливаем необходимый текст или другую необходимую кастомизацию
        SetVisual(sceneName);
        // Моментально показываем окно загрузки
        _canvasGroup.DOKill();
        _canvasGroup.blocksRaycasts = true;
        _canvasGroup.DOFade(1, 0);
        // Запускаем основную анимацию загрузочного экрана
        _animator.enabled = true;
        _animator.Play("LoadingAnimation", -1 , 0.0f);
        // Начинаем процесс загрузки сцены
        StartCoroutine(Loading(sceneName));
    }

    private void SetVisual(string sceneName)
    {
        if (sceneName.Contains("Gameplay"))
            _loadingText.text = "Идет загрузка уровня...";

        else if (sceneName.Contains("Menu"))
            _loadingText.text = "Идет загрузка меню...";
    }

    IEnumerator Loading(string sceneName)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);
        async.allowSceneActivation = false;
        while (!async.isDone)
        {
            _progressBarImage.fillAmount = async.progress;
            if (async.progress > 0.8)
            {
                yield return new WaitForSeconds(Random.Range(1.5f, 3f));
                async.allowSceneActivation = true;
            }
            yield return null;
        }
        Hide();
    }

    private void Hide()
    {
        // Плавно скрываем окно
        _canvasGroup.DOKill();
        _canvasGroup.DOFade(0, 0.5f).OnComplete(() => _canvasGroup.blocksRaycasts = false);
        // Останавливаем основную анимацию загрузочного экрана
        _animator.enabled = false;
    }
}
