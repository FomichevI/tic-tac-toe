using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MultyButtonsPopup : Popup
{
    [Header("GameObjects to activate")]
    [SerializeField] private GameObject _closeButtonRoot;
    [SerializeField] private GameObject _headerRoot;
    [SerializeField] private GameObject _buttonOneRoot;
    [SerializeField] private GameObject _buttonTwoRoot;
    [SerializeField] private GameObject _buttonThreeRoot;
    [SerializeField] private GameObject _buttonFourRoot;
    [Space]
    [Header("Buttons")]
    [SerializeField] private Button _closeButton;
    [SerializeField] private Button _buttonOne;
    [SerializeField] private Button _buttonTwo;
    [SerializeField] private Button _buttonThree;
    [SerializeField] private Button _buttonFour;
    [Space]
    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI _headerTmp;
    [SerializeField] private TextMeshProUGUI _buttonOneTmp;
    [SerializeField] private TextMeshProUGUI _buttonTwoTmp;
    [SerializeField] private TextMeshProUGUI _buttonThreeTmp;
    [SerializeField] private TextMeshProUGUI _buttonFourTmp;

    protected override void OnAwake()
    {
        base.OnAwake();
        _buttonOne.onClick.AddListener(Close);
        _buttonTwo.onClick.AddListener(Close);
        _buttonThree.onClick.AddListener(Close);
        _buttonFour.onClick.AddListener(Close);
        _closeButton.onClick.AddListener(Close);
    }

    #region Output
    /// <summary>
    /// Установить систему вывода
    /// </summary>
    public void SetOutput(string header)
    {
        _headerRoot.SetActive(true);
        _headerTmp.text = header;
    }
    public void SetOutput()
    {
        _headerRoot.SetActive(false);
    }
    #endregion

    #region Input

    /// <summary>
    /// Установить систему пользовательского вывода
    /// </summary>
    public void SetInput(Action onFirstButtonClick, string firstButtonText, bool withCloseButton)
    {
        _buttonOneRoot.SetActive(true);
        _buttonTwoRoot.SetActive(false);
        _buttonThreeRoot.SetActive(false);
        _buttonFourRoot.SetActive(false);
        _closeButtonRoot.SetActive(withCloseButton);

        _buttonOne.onClick.AddListener(onFirstButtonClick.Invoke);
        _buttonOneTmp.text = firstButtonText;
    }

    public void SetInput(Action onFirstButtonClick, string firstButtonText, Action onSecondButtonClick, string secondButtonText, bool withCloseButton)
    {
        _buttonOneRoot.SetActive(true);
        _buttonTwoRoot.SetActive(true);
        _buttonThreeRoot.SetActive(false);
        _buttonFourRoot.SetActive(false);
        _closeButtonRoot.SetActive(withCloseButton);

        _buttonOne.onClick.AddListener(onFirstButtonClick.Invoke);
        _buttonOneTmp.text = firstButtonText;
        _buttonTwo.onClick.AddListener(onSecondButtonClick.Invoke);
        _buttonTwoTmp.text = secondButtonText;
    }

    public void SetInput(Action onFirstButtonClick, string firstButtonText, Action onSecondButtonClick, string secondButtonText,
         Action onThirdButtonClick, string thirdButtonText, bool withCloseButton)
    {
        _buttonOneRoot.SetActive(true);
        _buttonTwoRoot.SetActive(true);
        _buttonThreeRoot.SetActive(true);
        _buttonFourRoot.SetActive(false);
        _closeButtonRoot.SetActive(withCloseButton);

        _buttonOne.onClick.AddListener(onFirstButtonClick.Invoke);
        _buttonOneTmp.text = firstButtonText;
        _buttonTwo.onClick.AddListener(onSecondButtonClick.Invoke);
        _buttonTwoTmp.text = secondButtonText;
        _buttonThree.onClick.AddListener(onThirdButtonClick.Invoke);
        _buttonThreeTmp.text = thirdButtonText;
    }

    public void SetInput(Action onFirstButtonClick, string firstButtonText, Action onSecondButtonClick, string secondButtonText,
         Action onThirdButtonClick, string thirdButtonText, Action onFourthButtonClick, string fourthButtonText, bool withCloseButton)
    {
        _buttonOneRoot.SetActive(true);
        _buttonTwoRoot.SetActive(true);
        _buttonThreeRoot.SetActive(true);
        _buttonFourRoot.SetActive(true);
        _closeButtonRoot.SetActive(withCloseButton);

        _buttonOne.onClick.AddListener(onFirstButtonClick.Invoke);
        _buttonOneTmp.text = firstButtonText;
        _buttonTwo.onClick.AddListener(onSecondButtonClick.Invoke);
        _buttonTwoTmp.text = secondButtonText;
        _buttonThree.onClick.AddListener(onThirdButtonClick.Invoke);
        _buttonThreeTmp.text = thirdButtonText;
        _buttonThree.onClick.AddListener(onFourthButtonClick.Invoke);
        _buttonThreeTmp.text = fourthButtonText;
    }

    #endregion

}
