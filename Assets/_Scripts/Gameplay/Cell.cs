using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public enum CellValue { X, O, Empty}

[RequireComponent(typeof(UiAnimator))]
public class Cell : MonoBehaviour
{
    [HideInInspector] public UnityEvent<Cell> OnClick; // �������, ���������� ��� ����� �� ������. �� ���� ������������� ����� Gameplay
    [HideInInspector] public UnityEvent<Cell> OnValueChanged; // �������, ���������� ��� ��������� �������� ������. �� ���� ������������� ����� Gameplay

    [SerializeField] private GameObject _xImage;
    [SerializeField] private GameObject _oImage;

    private UiAnimator _animator; // � ������� ����� ������������ ����� ������ ��������
    private CellValue _cellValue = CellValue.Empty; public CellValue CellValue {  get { return _cellValue; } }
    private bool _isUsed = false; public bool IsUsed { get { return _isUsed; } }

    private void Awake()
    {
        _animator = GetComponent<UiAnimator>();
    }

    private void OnDisable()
    {
        OnClick.RemoveAllListeners();
        OnValueChanged.RemoveAllListeners();
    }

    public void ButtonClick()
    {
        OnClick?.Invoke(this); // ���������� � Gameplay ������ � ������� �� ���������� ������.
                               // ����� ���� ������ Gameplay ����������, ����� �� ��������� ������ �������� ������
    }

    public void ChangeValue(bool isFirstPlayer)
    {
        _animator.ShowWithRise(0.3f);

        if (isFirstPlayer)
        {
            _xImage.SetActive(true);
            _cellValue = CellValue.X;
        }
        else
        {
            _oImage.SetActive(true);
            _cellValue = CellValue.O;
        }

        _isUsed = true;
        OnValueChanged?.Invoke(this); // ���������� ���� ���������� ���������� � ����� ��������
    }
}
