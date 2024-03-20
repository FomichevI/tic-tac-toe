using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ������, ��� ������� �� ����� ������������� ��� ��������� �����, ��� ��� � ��������� ��� ����������� ������� �������������� �� 
// ����� �������� ��������, ������� ������ ���� ����������� ������� ������� �� ����� ����� ����

// � ������ ���������� �� ���� ������ ����� ������� ����� �������� ���������������, � ������� ����� ������� �� ��������� �������,
// ��������, TabManager, ���������� ������ �� ���� �������
// �� ������ ������ �� ���� ������ ����������� �� ��������� �������, ������ �� ������� ����� �������� �� ���������� �����

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
        // ��������� ������
        LoadAllItems();
        // ���������� ��� �������� ��������
        GenerateShop();
        // ��������� ������ ��������
        OnCurrencyClick();
    }

    /// <summary>
    /// �������� ���� ��������� ��������
    /// </summary>
    public void GenerateShop()
    {
        // ������� �������� �� ������� "������"
        for (int i = 0; i < _currencyItems.Count; i++)
        {
            SimpleStoreItem currencyItem = Instantiate(_currencyStoreItemPrefab, _currencyFieldContent);
            currencyItem.Initial(_currencyItems[i]);
            string id = _currencyItems[i].key;
            currencyItem.SetClickAction(() => ByeItem(id));
        }

        // ������� �������� �� ������� "������"
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
        Debug.Log("�������: " +  itemId);
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
