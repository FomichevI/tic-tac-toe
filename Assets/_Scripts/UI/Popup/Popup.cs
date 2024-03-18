using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(UiAnimator))]
public class Popup : MonoBehaviour
{
    [HideInInspector] public UnityEvent OnPopupClosed;

    private UiAnimator _animator;

    private void Awake()
    {
        OnAwake();
    }

    protected virtual void OnAwake()
    {
        _animator = GetComponent<UiAnimator>();
    }


    public void Show()
    {
        _animator.ShowWithRise(0.5f);
    }


    protected void Close()
    {
        _animator.HideWithPulse(0.5f, () =>
        {
            OnPopupClosed?.Invoke();
            Destroy(this.gameObject);
        });
    }

    private void OnDestroy()
    {
        OnPopupClosed.RemoveAllListeners();
    }
}
