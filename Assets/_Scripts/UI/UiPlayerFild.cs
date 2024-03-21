using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �����, ���������� �� ����������� ���������� �� ������ � ���� ��������
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
        // ���������� ���
        if (isPlayer)
            _nameTmp.text = GameManager.Instance.SessionConfig.Player.Name;
        else
            _nameTmp.text = GameManager.Instance.SessionConfig.Opponent.Name;

        // ���������� ������ ����� �������
        if (isFirstPlayer)
            _symbolImg.sprite = _xMark;
        else
            _symbolImg.sprite = _oMark;

        // � ������� ������� ��� ������ ������ �������
        _symbolImg.SetNativeSize();
    }

}
