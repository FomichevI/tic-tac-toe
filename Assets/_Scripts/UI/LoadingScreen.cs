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
        // ������������� ����������� ����� ��� ������ ����������� ������������
        SetVisual(sceneName);
        // ����������� ���������� ���� ��������
        _canvasGroup.DOKill();
        _canvasGroup.blocksRaycasts = true;
        _canvasGroup.DOFade(1, 0);
        // ��������� �������� �������� ������������ ������
        _animator.enabled = true;
        _animator.Play("LoadingAnimation", -1 , 0.0f);
        // �������� ������� �������� �����
        StartCoroutine(Loading(sceneName));
    }

    private void SetVisual(string sceneName)
    {
        if (sceneName.Contains("Gameplay"))
            _loadingText.text = "���� �������� ������...";

        else if (sceneName.Contains("Menu"))
            _loadingText.text = "���� �������� ����...";
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
        // ������ �������� ����
        _canvasGroup.DOKill();
        _canvasGroup.DOFade(0, 0.5f).OnComplete(() => _canvasGroup.blocksRaycasts = false);
        // ������������� �������� �������� ������������ ������
        _animator.enabled = false;
    }
}
