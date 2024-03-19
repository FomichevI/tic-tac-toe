using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Класс, отвечающий за визуализацию определения первого игрока
/// </summary>
// Стоит подумать над улучшением реализации
public class FirstTurnPannel : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [Header("UI elements")]
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private TextMeshProUGUI _playerNameTmp;
    [SerializeField] private TextMeshProUGUI _opponentNameTmp;

    private Action _onAnimateCompleted;
    private bool _firstTurnOfPlayer = false;

    private void Start()
    {
        _canvasGroup.blocksRaycasts = true;
        _canvasGroup.DOKill();
        _canvasGroup.DOFade(1, 0);
        SetNames();
    }

    public void StartAnimation(Action onCompleted, bool isFirstTurnOfPlayer)
    {
        _onAnimateCompleted = onCompleted;
        _firstTurnOfPlayer = isFirstTurnOfPlayer;
        _animator.enabled = true;
    }

    private void SetNames()
    {
        _playerNameTmp.text = GameManager.Instance.SessionConfig.Player.Name;
        _opponentNameTmp.text = GameManager.Instance.SessionConfig.Opponent.Name;
    }

    /// <summary>
    /// Отключить отображение имени НЕ первого игрока во время анимации
    /// </summary>
    public void DeactivateNeedlessName()
    {
        _playerNameTmp.gameObject.SetActive(_firstTurnOfPlayer);
        _opponentNameTmp.gameObject.SetActive(!_firstTurnOfPlayer);
    }

    /// <summary>
    /// Вызывается в конце анимации
    /// </summary>
    public void OnAnimationCompleted()
    {
        _canvasGroup.DOKill();
        _canvasGroup.DOFade(0, 0.5f).OnComplete(() =>
        {
            _canvasGroup.blocksRaycasts = false;
            _onAnimateCompleted?.Invoke();
        });
    }
}
