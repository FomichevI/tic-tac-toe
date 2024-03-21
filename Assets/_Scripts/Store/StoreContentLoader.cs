using System;
using UnityEngine;

/// <summary>
/// Класс, отвечающий за загрузку из файла типа JSON
/// </summary>
public class StoreContentLoader : MonoBehaviour
{
    [SerializeField] private TextAsset _info;

    public StoreItemInfo[] GetItems()
    {
        return JsonHelper.FromJson<StoreItemInfo>(_info.text);
    }

}

public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.shopItems;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.shopItems = array;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.shopItems = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T[] shopItems;
    }
}

/// <summary>
/// Класс для сериализации элементов магазина из JSON-файла
/// </summary>
[Serializable]
public class StoreItemInfo
{
    public string key;
    public string type;
    public string rare;
    public int amount;
    public StoreItemInfo[] items;
    public string price;
    public string currency;
}