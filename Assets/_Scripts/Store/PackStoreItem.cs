using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Класс, управляющий предметом магазина типа Pack, содержащий в себе несколько простых предметов
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
        // Получаем данные о предмете из тех, что есть в проекте
        ItemInfo itemInfo = GameManager.Instance.ItemsConfig.GetItemInfo(storeItemInfo.key, ItemType.Pack);
        if (itemInfo == null) return;

        // Устанавливаем визуал в соответствии с полученными данными
        if (_iconImg != null) _iconImg.sprite = itemInfo.IconSprite;
        if (_name != null) _name.text = itemInfo.Name;

        if (_price != null) _price.text = NumbersConverter.GetPrice(storeItemInfo.price, storeItemInfo.currency);
        // Создаем все предметы, которые содержатся в паке
        for (int i = 0; i < storeItemInfo.items.Length; i++)
        {
            SimpleStoreItem item = Instantiate(_itemInPackPrefab, _itemsContent);
            item.Initial(storeItemInfo.items[i]);
        }

        // Почему-то ContentSizeFitter не работает, если он сразу активирован во время добавления контента, поэтому активируем в конце
        _sizeFitter.enabled = true;
    }

    public void SetClickAction(Action onClick)
    {
        _button.onClick.AddListener(onClick.Invoke);
        // Продолжение логики
    }
}
