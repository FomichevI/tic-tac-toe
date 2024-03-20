using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseProcessScreen : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private float _waitingTime = 2f;
    
    public void Show()
    {
        _canvasGroup.DOKill();
        _canvasGroup.DOFade(1, 0f);
        _canvasGroup.blocksRaycasts = true;
        StartCoroutine(HideWithDelay());
    }

    IEnumerator HideWithDelay()
    {
        yield return new WaitForSeconds(_waitingTime);
        Hide();
    }

    private void Hide()
    {
        _canvasGroup.DOKill();
        _canvasGroup.DOFade(0, 0.3f);
        _canvasGroup.blocksRaycasts = false;
        PopupManager.Instance.ShowPurchaseResult(Random.Range(0, 2) == 1);
    }
}
