using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SimplePopup : Popup
{
    [Header("GameObjects to activate")]
    [SerializeField] private GameObject _closeButtonRoot;
    [SerializeField] private GameObject _iconRoot;
    [SerializeField] private GameObject _headerRoot;
    [SerializeField] private GameObject _descriptionRoot;
    [SerializeField] private GameObject _yesNoButtonsRoot;
    [SerializeField] private GameObject _confirmButtonRoot;
    [Space]
    [Header("Buttons")]
    [SerializeField] private Button _closeButton;
    [SerializeField] private Button _yesButton;
    [SerializeField] private Button _noButton;
    [SerializeField] private Button _confirmButton;
    [Space]
    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI _headerTmp;
    [SerializeField] private TextMeshProUGUI _descriptionTmp;
    [SerializeField] private TextMeshProUGUI _yesButtonTmp;
    [SerializeField] private TextMeshProUGUI _noButtonTmp;
    [SerializeField] private TextMeshProUGUI _confirmButtonTmp;
    [Space]
    [Header("Image")]
    [SerializeField] private Image _iconImg;

    protected override void OnAwake()
    {
        base.OnAwake();
        _confirmButton.onClick.AddListener(Close);
        _yesButton.onClick.AddListener(Close);
        _noButton.onClick.AddListener(Close);
        _closeButton.onClick.AddListener(Close);
    }

    #region Output
    /// <summary>
    /// Установить систему вывода
    /// </summary>
    public void SetOutput(string description)
    {
        _iconRoot.SetActive(false);
        _headerRoot.SetActive(false);
        _descriptionRoot.SetActive(true);
        _descriptionTmp.text = description;
    }
   
    public void SetOutput(string header,string description)
    {
        _iconRoot.SetActive(false);
        _headerRoot.SetActive(true);
        _headerTmp.text = header;
        _descriptionRoot.SetActive(true);
        _descriptionTmp.text = description;
    }
   
    public void SetOutput(Sprite icon, string header, string description)
    {
        _iconRoot.SetActive(true);
        _iconImg.sprite = icon;
        _headerRoot.SetActive(true);
        _headerTmp.text = header;
        _descriptionRoot.SetActive(true);
        _descriptionTmp.text = description;
    }
    
    public void SetOutput(Sprite icon, string description)
    {
        _iconRoot.SetActive(true);
        _iconImg.sprite = icon;
        _headerRoot.SetActive(false);
        _descriptionRoot.SetActive(true);
        _descriptionTmp.text = description;
    }
    #endregion

    #region Input

    /// <summary>
    /// Установить систему пользовательского вывода
    /// </summary>
    public void SetInput(bool withCloseButton)
    {
        _yesNoButtonsRoot.SetActive(false);
        _closeButtonRoot.SetActive(withCloseButton);
        _confirmButtonRoot.SetActive(false);
    }

    public void SetInput(bool withCloseButton, Action onClose)
    {
        _yesNoButtonsRoot.SetActive(false);
        _closeButtonRoot.SetActive(withCloseButton);
        _closeButton.onClick.AddListener(onClose.Invoke);
        _confirmButtonRoot.SetActive(false);
    }

    public void SetInput(Action onConfirmClick, bool withCloseButton)
    {
        _yesNoButtonsRoot.SetActive(false);
        _closeButtonRoot.SetActive(withCloseButton);
        _confirmButtonRoot.SetActive(true);
        _confirmButton.onClick.AddListener(onConfirmClick.Invoke);
    }

    public void SetInput(Action onConfirmClick, string confirmButtonText, bool withCloseButton)
    {
        _yesNoButtonsRoot.SetActive(false);
        _closeButtonRoot.SetActive(withCloseButton);
        _confirmButtonRoot.SetActive(true);
        _confirmButton.onClick.AddListener(onConfirmClick.Invoke);
        _confirmButtonTmp.text = confirmButtonText;
    }

    public void SetInput(Action onYesClick, Action onNoClick, bool withCloseButton)
    {
        _yesNoButtonsRoot.SetActive(true);
        _closeButtonRoot.SetActive(withCloseButton);
        _confirmButtonRoot.SetActive(false);
        _yesButton.onClick.AddListener(onYesClick.Invoke);
        _noButton.onClick.AddListener(onNoClick.Invoke);
    }

    public void SetInput(Action onYesClick, Action onNoClick, string yesButtonText, string noButtonText, bool withCloseButton)
    {
        _yesNoButtonsRoot.SetActive(true);
        _closeButtonRoot.SetActive(withCloseButton);
        _confirmButtonRoot.SetActive(false);
        _yesButton.onClick.AddListener(onYesClick.Invoke);
        _noButton.onClick.AddListener(onNoClick.Invoke);
        _yesButtonTmp.text = yesButtonText;
        _noButtonTmp.text = noButtonText;
    }
    #endregion

}
