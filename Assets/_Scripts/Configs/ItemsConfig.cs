using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType {None, Currency, Equipment, Consumables, Pack}
/// <summary>
/// Содержит информацию о всех предметах в игре
/// </summary>
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ItemsConfig", order = 1)]
public class ItemsConfig : ScriptableObject
{
    [SerializeField] private ItemInfo[] _currencyItems;
    [SerializeField] private ItemInfo[] _equipment;
    [SerializeField] private ItemInfo[] _consumables;
    [SerializeField] private ItemInfo[] _packs;

    public ItemInfo GetItemInfo(string id, ItemType type)
    {
        ItemInfo[] items = null;
        switch (type)
        {
            case ItemType.Currency:
                items = _currencyItems;
                break;
            case ItemType.Equipment:
                items = _equipment;
                break;
            case ItemType.Consumables:
                items = _consumables;
                break;
            case ItemType.Pack:
                items = _packs;
                break;
        }

        foreach (ItemInfo item in items)
        {
            if (item.Id == id)
            {
                return item;
            }
        }
        Debug.LogError("Item information with ID " + id + " has not found! Item type: " + type);
        return null;
    }

}


/// <summary>
/// Содержит информацию о конкретном предмете в игре
/// </summary>
[Serializable]
public class ItemInfo
{
    [SerializeField] private string _id; public string Id { get { return _id; } }
    [SerializeField] private Sprite _iconSprite; public Sprite IconSprite { get { return _iconSprite; } }
    [SerializeField] private string _name; public string Name { get { return _name; } }
}