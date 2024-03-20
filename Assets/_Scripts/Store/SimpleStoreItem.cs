using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SimpleStoreItem : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Image _backGroundImg;
    [SerializeField] private Image _iconImg;
    [SerializeField] private TextMeshProUGUI _nameTmp;
    [SerializeField] private TextMeshProUGUI _priceTmp;
    [SerializeField] private TextMeshProUGUI _amountTmp;

    /// <summary>
    /// ������������� ����������� ����������� � ���������� ���� � ����������� �� "rare"
    /// </summary>
    public void Initial(StoreItemInfo storeItemInfo)
    {
        // ���������� ��� ��������
        ItemType type = ItemType.None;
        switch (storeItemInfo.type)
        {
            case "currency":
                type = ItemType.Currency; break;
            case "equipment":
                type = ItemType.Equipment; break;
            case "consumables":
                type = ItemType.Consumables; break;
        }

        // �������� ������ � �������� �� ���, ��� ���� � �������
        ItemInfo itemInfo = GameManager.Instance.ItemsConfig.GetItemInfo(storeItemInfo.key, type);
        if (itemInfo == null) return;
        // ������������� ������ � ������������ � ����������� �������
        if (_iconImg != null) _iconImg.sprite = itemInfo.IconSprite;
        if (_nameTmp != null) _nameTmp.text = itemInfo.Name;
        if (_priceTmp != null) _priceTmp.text = NumbersConverter.GetPrice(storeItemInfo.price, storeItemInfo.currency);

        // ���� ���������� �������, �� �� ���������� ��� �����
        if (_amountTmp != null) 
            _amountTmp.text = storeItemInfo.amount != 0? storeItemInfo.amount.ToString() : "";

        SetBackCollor(storeItemInfo.rare);
    }

    // ����� ���������� ������ � ������� � ��������� ��������� ��� ��������� ���� �������� � �� ���� � ����
    private void SetBackCollor(string rare)
    {
        if (rare == "magic")
            _backGroundImg.color = Color.blue;
        else
            _backGroundImg.color = Color.white;
    }

    public void SetClickAction(Action onClick)
    {
        if (_button != null)
            _button.onClick.AddListener(onClick.Invoke);
        // ����������� ������
    }
}
