using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// �����, ���������� �� ������ UI �� ����� � ���������
/// </summary>
// ���������� ����������� ����������� �������, ��� ��� �� ��������, ����� ������ ����� �������� ���������
public class UiGameplay : MonoBehaviour
{
    [HideInInspector] public UnityEvent OnFirstPlayerChosen; // �������, ���������� ����� ����������� ���������� � ���� ������� ������
    [SerializeField] private UiPlayerFild _playerFild;
    [SerializeField] private UiPlayerFild _opponentFild;
    [SerializeField] private TextMeshProUGUI _currentTurnTmp;
    [SerializeField] private FinishPannel _finishPannel;
    [SerializeField] private FirstTurnPannel _firstTurnPannel;
    private Gameplay _gameplay;

    public void Initialize(Gameplay gameplay)
    {
        _gameplay = gameplay;
        _gameplay.OnFirstTurnDefined.AddListener(SetStartProperties);
        _gameplay.OnTernChanged.AddListener(ChangeCurrentTurnVisual);
        _gameplay.OnMatchFinished.AddListener(ShowFinishPannel);
    }

    private void OnDisable()
    {
        _gameplay.OnFirstTurnDefined.RemoveListener(SetStartProperties);
        _gameplay.OnTernChanged.RemoveListener(ChangeCurrentTurnVisual);
        _gameplay.OnMatchFinished.RemoveListener(ShowFinishPannel);
    }

    /// <summary>
    /// �������� ���������� ���������� �������� ���� ��� ����� ����
    /// </summary>
    private void ChangeCurrentTurnVisual(bool isPlayerTurn)
    {
        _currentTurnTmp.text = isPlayerTurn ? "��� ���" : "��� ���������";
    }

    /// <summary>
    /// ���������� ������ � ������������ � ���, ��� ����� ������
    /// </summary>
    private void SetStartProperties(bool isFirstTurnOfPlayer)
    {
        // �������� ���������� ���������� ����� ����������� ���� ������� ������ 
        _playerFild.SetValues(true, isFirstTurnOfPlayer);
        _opponentFild.SetValues(false, !isFirstTurnOfPlayer);
        // ����� ������ ������ �������� ����
        ChangeCurrentTurnVisual(isFirstTurnOfPlayer);
        // ��������� �������� ������ ������� ������
        AnimateFirstTurnChoise(isFirstTurnOfPlayer);
    }

    /// <summary>
    /// ��������� ���������� ����������� ������ ������� ������
    /// </summary>
    private void AnimateFirstTurnChoise(bool isFirstTurnOfPlayer)
    {
        if (_firstTurnPannel != null)
            _firstTurnPannel.StartAnimation(_gameplay.StartGame, isFirstTurnOfPlayer);
        else
            _gameplay.StartGame();
    }

    private void ShowFinishPannel()
    {
        StartCoroutine(ShowFinishPannelWithDelay());

        IEnumerator ShowFinishPannelWithDelay()
        {
            yield return new WaitForSeconds(1f);
            _finishPannel.Show();
        }
    }

    public void OnPauseButtonClick()
    {
        _gameplay.StopGame();
        PopupManager.Instance.ShowPause(_gameplay.ContinueGame);
    }

    public void OnInfoButtonClick() 
    {
        PopupManager.Instance.ShowLevelDescription();
    }
}
