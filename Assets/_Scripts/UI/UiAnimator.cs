using UnityEngine;
using DG.Tweening;
using System;

/// <summary>
/// Простейший аниматор для UI элементов с использованием DoTween
/// </summary>
public class UiAnimator : MonoBehaviour
{
    private Vector3 _originalScale = Vector3.one;
    private RectTransform _rectTransform;

    private void Awake()
    {
        OnAwake();
    }

    protected virtual void OnAwake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        _originalScale = _rectTransform.localScale;
    }

    #region Hide
    public void HideWithPulse(float time, Action onComplite)
    {
        _rectTransform.DOKill();
        _rectTransform.DOScale(1.1f, time / 5).OnComplete(() => _rectTransform.DOScale(0, time).OnComplete(onComplite.Invoke));
    }
    #endregion

    #region Show
    public void ShowWithRise(float time)
    {
        _rectTransform.DOKill();
        _rectTransform.localScale = new Vector2(0, 0);
        _rectTransform.DOScale(_originalScale * 1.1f, time).OnComplete(() => _rectTransform.DOScale(_originalScale, time / 5));
    }

    public void ShowWithRise(float time, Action onComplite)
    {
        _rectTransform.DOKill();
        _rectTransform.localScale = new Vector2(0, 0);
        _rectTransform.DOScale(_originalScale * 1.1f, time).OnComplete(() => _rectTransform.DOScale(_originalScale, time / 5).OnComplete(onComplite.Invoke));
    }

    #endregion
}
