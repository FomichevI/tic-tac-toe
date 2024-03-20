using DG.Tweening;
using System;
using UnityEngine;

// ���������� ���� ���� ������� �������, � ������� ���������� ���� �� ���������� ��� ������� ������/��� ������� �����/��� ����������� ����������
// �, � ����������� �� ���� ���������� ��� ��������� �����. ������, ����� ������� ��������� �� �������� ���������� �������� �
// ���� �� ����� ���������� � ����� ���������� �������. ��� ���������� ����� ������� ������������� �� ������� ������ �������, ����� ������� �� 
// ����� �����������. ������� ���� ������� ������� �������.

// !!! ��� ���������� ���������� ����� ������� ������ �������� ������� � ��������� ��� ��� ��������/�������� ����� �������.
// ��� �������� �������� ����� � ����������� ��� ����� ������� ������ �� ������ � ��� �������� ���������� ���������� �������.

public class PopupManager : MonoBehaviour
{
    public static PopupManager Instance;

    [SerializeField] private SimplePopup _simplePopupPrefab;
    [SerializeField] private MultyButtonsPopup _multyButtonsPopupPrefab;
    [SerializeField] private CanvasGroup _backGroundCg;
    [SerializeField] private PopupsConfig _config;

    private Popup _currentPopup = null; // ��������� ���� �� ���������� ������ �������� �������

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void ShowTestPopup()
    {
        SimplePopup popup = Instantiate<SimplePopup>(_simplePopupPrefab, this.transform);
        popup.SetOutput("��� �������� ���������", "��� �������� ��������. ��� �������� ��������. ��� �������� ��������.");
        popup.SetInput(true);
        ShowPopup(popup);
    }

    /// <summary>
    /// �������� ����� � ������� ������ ���������
    /// </summary>
    public void ShowComplexity(Action onSimpleChoose, Action onNormalChoose, Action onHardChoose)
    {
        MultyButtonsPopup popup = Instantiate<MultyButtonsPopup>(_multyButtonsPopupPrefab, this.transform);
        popup.SetOutput("�������� ������� ���������");
        popup.SetInput(onSimpleChoose, "�����", onNormalChoose, "���������", onHardChoose, "������", true);
        ShowPopup(popup);
    }

    /// <summary>
    /// �������� ����� � ��������� ������. �� ������ ������ �������� � �� ��������� �� �����-���� ������� ������ 
    /// </summary>
    public void ShowLevelDescription()
    {
        SimplePopup popup = Instantiate<SimplePopup>(_simplePopupPrefab, this.transform);
        popup.SetOutput("��� ������ ���������� ��������� 3 ����� ������� � ���� �����. ���� ���� �� 1 ������");
        popup.SetInput(true);
        ShowPopup(popup);
    }

    /// <summary>
    /// �������� ����� ����� � ������������ ����� � ����
    /// </summary>
    /// <param name="onBackToMenuClick">�������������� ������� ��� ������������� ������ � ����</param>
    /// <param name="onContinueClick">������� ��� �������� � ����</param>
    public void ShowPause(Action onContinueClick, Action onBackToMenuClick = null)
    {
        SimplePopup popup = Instantiate<SimplePopup>(_simplePopupPrefab, this.transform);
        popup.SetOutput("�����","������ ����� � ������� ����?");
        popup.SetInput(() =>
        {
            onBackToMenuClick?.Invoke();
            SceneLoader.Instance.LoadMenuScene();
        },
        onContinueClick, false);
        ShowPopup(popup);
    }

    /// <summary>
    /// �������� ����� ����� ������� �� ����
    /// </summary>
    public void ShowExit(Action onConfirmClick)
    {
        SimplePopup popup = Instantiate<SimplePopup>(_simplePopupPrefab, this.transform);
        popup.SetOutput(_config.SadSmileIcon,"������ ����� �� ����?");
        popup.SetInput(onConfirmClick, true);
        ShowPopup(popup);
    }

    public void ShowPurchaseResult(bool isSuccessfull)
    {
        string text = isSuccessfull ? "������� ������ �������" : "������ �������, ���������� �����"; 
        SimplePopup popup = Instantiate<SimplePopup>(_simplePopupPrefab, this.transform);
        popup.SetOutput(text);
        popup.SetInput(true);
        ShowPopup(popup);
    }

    private void ShowPopup(Popup popup)
    {
        if (_currentPopup != null)
            _currentPopup.Hide();
        ShowBackGround();
        popup.Show();
        popup.OnPopupClosed.AddListener(HideBackGround);
        _currentPopup = popup;
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
