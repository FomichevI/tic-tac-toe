using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PopupType {Simple}

public class PopupManager : MonoBehaviour
{
    public static PopupManager instance;

    [SerializeField] private SimplePopup _simplePopupPrefab;
    [SerializeField] private MultyButtonsPopup _multyButtonsPopupPrefab;
    [SerializeField] private CanvasGroup _backGroundCg;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void ShowTestPopup()
    {
        ShowBackGround();
        SimplePopup popup = Instantiate<SimplePopup>(_simplePopupPrefab, this.transform);
        popup.SetOutput("��� �������� ���������", "��� �������� ��������. ��� �������� ��������. ��� �������� ��������.");
        popup.SetInput(true);
        popup.Show();
        popup.OnPopupClosed.AddListener(HideBackGround);
    }

    /// <summary>
    /// �������� ����� � ������� ������ ���������
    /// </summary>
    public void ShowComplexityPopup(Action onSimpleChoose, Action onNormalChoose, Action onHardChoose)
    {
        ShowBackGround();
        MultyButtonsPopup popup = Instantiate<MultyButtonsPopup>(_multyButtonsPopupPrefab, this.transform);
        popup.SetOutput("�������� ������� ���������");
        popup.SetInput(onSimpleChoose, "�����", onNormalChoose, "���������", onHardChoose, "������", true);
        popup.Show();
        popup.OnPopupClosed.AddListener(HideBackGround);
    }

    private void ShowBackGround()
    {
        _backGroundCg.DOKill();
        _backGroundCg.blocksRaycasts = true;
        _backGroundCg.DOFade(1, 0.3f);
    }

    private void HideBackGround()
    {
        _backGroundCg.DOKill();
        _backGroundCg.DOFade(0, 0.3f).OnComplete(() => _backGroundCg.blocksRaycasts = false);
    }

}
