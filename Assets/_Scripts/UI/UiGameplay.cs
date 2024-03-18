using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Класс, отвечающий за работу UI на сцене с геймплеем
/// </summary>
// Реализация интерфейчас максимально простая, так как не известно, каким именно будет конечный интерфейс
public class UiGameplay : MonoBehaviour
{
    [SerializeField] private UiPlayerFild _playerFild;
    [SerializeField] private UiPlayerFild _opponentFild;
    [SerializeField] private TextMeshProUGUI _currentTurnTmp;
    [SerializeField] private FinishPannel _finishPannel;
    private Gameplay _gameplay;

    public void Initialize(Gameplay gameplay)
    {
        _gameplay = gameplay;
        _gameplay.OnFirstTurnDefined.AddListener(SetPlayersFilds);
        _gameplay.OnTernChanged.AddListener(ChangeCurrentTurnVisual);
        _gameplay.OnMatchFinished.AddListener(ShowFinishPannel);
    }

    private void OnDisable()
    {
        _gameplay.OnFirstTurnDefined.RemoveListener(SetPlayersFilds);
        _gameplay.OnTernChanged.RemoveListener(ChangeCurrentTurnVisual);
        _gameplay.OnMatchFinished.RemoveListener(ShowFinishPannel);
    }

    private void ChangeCurrentTurnVisual(bool isPlayerTurn)
    {
        _currentTurnTmp.text = isPlayerTurn ? "Ваш ход" : "Ход соперника";
    }

    private void SetPlayersFilds(bool isPlayerFirst)
    {
        _playerFild.SetValues(true, isPlayerFirst);
        _opponentFild.SetValues(false, !isPlayerFirst);
    }

    private void ShowFinishPannel()
    {
        _finishPannel.Show();
    }
}
