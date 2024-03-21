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

    /// <summary>
    /// —крыть попап без событий при его закрытии. Ќеобходимо дл€ избежани€ открыти€ нескольких попапов друг над другом
    /// </summary>
    public void Hide()
    {
        _animator.HideWithPulse(0.5f, () =>
        {
            Destroy(this.gameObject);
        });
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
