using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Считаю, что магазин не нужно реализовывать как отдельную сцену, так как в мобильных игр большинство покупок осуществляются во 
// время игрового процесса, поэтому должна быть возможность вызвать магазин из любой сцены игры

// В данной реализации на этом классе лежит слишком много областей ответственности, в будущем лучше разбить на несколько классов,
// например, TabManager, отвечающий только за свою вкладку
// На данный момент не вижу смысла распыляться на множество классов, каждый из которых будет состоять из нескольких строк

public class StoreManager : MonoBehaviour
{
    public static StoreManager Instance;

    [SerializeField] private StoreContentLoader _loader;
    [SerializeField] private PurchaseProcessScreen _processScreen;
    [SerializeField] private CanvasGroup _canvasGroup;
    [Header("Interface")]
    [SerializeField] private GameObject _currencyField;
    [SerializeField] private GameObject _packsField;
    [SerializeField] private Transform _currencyFieldContent;
    [SerializeField] private Transform _packsFieldContent;
    [SerializeField] private Button _currencyButton;
    [SerializeField] private Button _packsButton;
    [Space][Header("Prefabs")]
    [SerializeField] private SimpleStoreItem _currencyStoreItemPrefab;
    [SerializeField] private PackStoreItem _packsItemPrefab;

    private List<StoreItemInfo> _currencyItems = new List<StoreItemInfo>();
    private List<StoreItemInfo> _packItems = new List<StoreItemInfo>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        // Загружаем данные
        LoadAllItems();
        // Генерируем все страницы магазина
        GenerateShop();
        // Открываем первую страницу
        OnCurrencyClick();
    }

    /// <summary>
    /// Создание всех элементов магазина
    /// </summary>
    public void GenerateShop()
    {
        // Создаем элементы во вкладке "Валюта"
        for (int i = 0; i < _currencyItems.Count; i++)
        {
            SimpleStoreItem currencyItem = Instantiate(_currencyStoreItemPrefab, _currencyFieldContent);
            currencyItem.Initial(_currencyItems[i]);
            string id = _currencyItems[i].key;
            currencyItem.SetClickAction(() => ByeItem(id));
        }

        // Создаем элементы во вкладке "Наборы"
        for (int i = 0; i < _packItems.Count; i++)
        {
            PackStoreItem packItem = Instantiate(_packsItemPrefab, _packsFieldContent);
            packItem.Initial(_packItems[i]);
            string id = _packItems[i].key;
            packItem.SetClickAction(() => ByeItem(id));
        }
    }

    private void LoadAllItems()
    {
        StoreItemInfo[] allItems = _loader.GetItems();
        foreach (StoreItemInfo item in allItems)
        {
            if (item.type == "currency")
                _currencyItems.Add(item);
            else if (item.type == "pack")
                _packItems.Add(item);
            else
                Debug.LogError("Unknown product type: " + item.type);
        }
    }

    public void ByeItem(string itemId)
    {
        Debug.Log("Покупка: " +  itemId);
        _processScreen.Show();
    }

    public void Show()
    {
        _canvasGroup.DOKill();
        _canvasGroup.DOFade(1, 0.3f);
        _canvasGroup.blocksRaycasts = true;
    }

    public void Hide()
    {
        _canvasGroup.DOKill();
        _canvasGroup.DOFade(0, 0.3f).OnComplete(() => _canvasGroup.blocksRaycasts = false);
    }

    public void OnCurrencyClick()
    {
        _currencyField.SetActive(true);
        _packsField.SetActive(false);
        _currencyButton.interactable = false;
        _packsButton.interactable = true;
    }

    public void OnPacksClick()
    {
        _currencyField.SetActive(false);
        _packsField.SetActive(true);
        _currencyButton.interactable = true;
        _packsButton.interactable = false;
    }
}
