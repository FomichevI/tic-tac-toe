using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �����, ����������� ��������� �������� ���� Pack, ���������� � ���� ��������� ������� ���������
/// </summary>
public class PackStoreItem : MonoBehaviour
{
    [SerializeField] ContentSizeFitter _sizeFitter;
    [SerializeField] private SimpleStoreItem _itemInPackPrefab;
    [SerializeField] private Transform _itemsContent;
    [SerializeField] private Button _button;
    [SerializeField] private Image _iconImg;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _price;

    public void Initial(StoreItemInfo storeItemInfo)
    {
        // �������� ������ � �������� �� ���, ��� ���� � �������
        ItemInfo itemInfo = GameManager.Instance.ItemsConfig.GetItemInfo(storeItemInfo.key, ItemType.Pack);
        if (itemInfo == null) return;

        // ������������� ������ � ������������ � ����������� �������
        if (_iconImg != null) _iconImg.sprite = itemInfo.IconSprite;
        if (_name != null) _name.text = itemInfo.Name;

        if (_price != null) _price.text = NumbersConverter.GetPrice(storeItemInfo.price, storeItemInfo.currency);
        // ������� ��� ��������, ������� ���������� � ����
        for (int i = 0; i < storeItemInfo.items.Length; i++)
        {
            SimpleStoreItem item = Instantiate(_itemInPackPrefab, _itemsContent);
            item.Initial(storeItemInfo.items[i]);
        }

        // ������-�� ContentSizeFitter �� ��������, ���� �� ����� ����������� �� ����� ���������� ��������, ������� ���������� � �����
        _sizeFitter.enabled = true;
    }

    public void SetClickAction(Action onClick)
    {
        _button.onClick.AddListener(onClick.Invoke);
        // ����������� ������
    }
}
