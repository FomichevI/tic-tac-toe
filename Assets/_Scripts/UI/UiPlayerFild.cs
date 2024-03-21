using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Класс, отвечающий за отображение информации об игроке в окне геймплея
/// </summary>
public class UiPlayerFild : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nameTmp;
    [SerializeField] private Image _symbolImg;

    [Header("Resouces")]
    [SerializeField] private Sprite _xMark;
    [SerializeField] private Sprite _oMark;

    public void SetValues(bool isPlayer ,bool isFirstPlayer)
    {
        // Выставляем имя
        if (isPlayer)
            _nameTmp.text = GameManager.Instance.SessionConfig.Player.Name;
        else
            _nameTmp.text = GameManager.Instance.SessionConfig.Opponent.Name;

        // Выставляем значок возле аватара
        if (isFirstPlayer)
            _symbolImg.sprite = _xMark;
        else
            _symbolImg.sprite = _oMark;

        // В будущем сделать обе иконки одного размера
        _symbolImg.SetNativeSize();
    }

}
