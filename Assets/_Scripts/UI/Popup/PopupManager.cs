using DG.Tweening;
using System;
using UnityEngine;

// Изначально идея была создать систему, в которой достаточно было бы передавать тип системы вывода/тип системы ввода/тип визуального оформления
// и, в зависимости от этих параметров уже создавать попап. Однако, такая система требовала бы большого количества префабов и
// была бы более запутанная в плане перегрузок методов. Для проработки такой системы потребовалось бы намного больше времени, чтобы сделать ее 
// легко расширяемой. Поэтому было принято текущее решение.

// !!! Для дальнейшей разработки стоит создать список открытых попапов и проверять его при открытии/закрытии новых попапов.
// Это позволит избежать багов с бэкграундом при смене отдного попапа на другой и при открытии нескольких одинаковых попапов.

public class PopupManager : MonoBehaviour
{
    public static PopupManager Instance;

    [SerializeField] private SimplePopup _simplePopupPrefab;
    [SerializeField] private MultyButtonsPopup _multyButtonsPopupPrefab;
    [SerializeField] private CanvasGroup _backGroundCg;
    [SerializeField] private PopupsConfig _config;

    private Popup _currentPopup = null; // Временная мера до реализации списка открытых попапов

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void ShowTestPopup()
    {
        SimplePopup popup = Instantiate<SimplePopup>(_simplePopupPrefab, this.transform);
        popup.SetOutput("Это тестовый заголовок", "Это тестовое описание. Это тестовое описание. Это тестовое описание.");
        popup.SetInput(true);
        ShowPopup(popup);
    }

    /// <summary>
    /// Показать попап с выбором уровня сложности
    /// </summary>
    public void ShowComplexity(Action onSimpleChoose, Action onNormalChoose, Action onHardChoose)
    {
        MultyButtonsPopup popup = Instantiate<MultyButtonsPopup>(_multyButtonsPopupPrefab, this.transform);
        popup.SetOutput("Выберите уровень сложности");
        popup.SetInput(onSimpleChoose, "ЛЕГКО", onNormalChoose, "НОРМАЛЬНО", onHardChoose, "СЛОЖНО", true);
        ShowPopup(popup);
    }

    /// <summary>
    /// Показать попап с описанием уровня. На данный момент статично и не ссылается на какие-либо игровые данные 
    /// </summary>
    public void ShowLevelDescription()
    {
        SimplePopup popup = Instantiate<SimplePopup>(_simplePopupPrefab, this.transform);
        popup.SetOutput("Для победы необходимо выставить 3 своих символа в одну линию. Игра идет до 1 раунда");
        popup.SetInput(true);
        ShowPopup(popup);
    }

    /// <summary>
    /// Показать попап паузы с предложением выйти в меню
    /// </summary>
    /// <param name="onBackToMenuClick">Дополнительное событие при подтверждении выхода в меню</param>
    /// <param name="onContinueClick">Событие при возврате в игру</param>
    public void ShowPause(Action onContinueClick, Action onBackToMenuClick = null)
    {
        SimplePopup popup = Instantiate<SimplePopup>(_simplePopupPrefab, this.transform);
        popup.SetOutput("ПАУЗА","Хотите выйти в главное меню?");
        popup.SetInput(() =>
        {
            onBackToMenuClick?.Invoke();
            SceneLoader.Instance.LoadMenuScene();
        },
        onContinueClick, false);
        ShowPopup(popup);
    }

    /// <summary>
    /// Показать попап перед выходом из игры
    /// </summary>
    public void ShowExit(Action onConfirmClick)
    {
        SimplePopup popup = Instantiate<SimplePopup>(_simplePopupPrefab, this.transform);
        popup.SetOutput(_config.SadSmileIcon,"Хотите выйти из игры?");
        popup.SetInput(onConfirmClick, true);
        ShowPopup(popup);
    }

    public void ShowPurchaseResult(bool isSuccessfull)
    {
        string text = isSuccessfull ? "Покупка прошла успешно" : "Ошибка покупки, попробуйте снова"; 
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
