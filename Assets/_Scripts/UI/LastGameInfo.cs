using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class LastGameInfo : MonoBehaviour
{
    [SerializeField] private GameObject[] _allComponents;
    [SerializeField] private TextMeshProUGUI _opponentNameTmp;
    [SerializeField] private TextMeshProUGUI _durationTmp;
    [SerializeField] private TextMeshProUGUI _resultTmp;

    private void Start()
    {
        SetParameters();
    }

    private void SetParameters()
    {
        SessionConfig config = GameManager.Instance.SessionConfig;
        if (config.OpponentName == "")
        {
            SetVisible(false);
        }
        else
        {
            SetVisible(true);
            _opponentNameTmp.text = config.OpponentName;
            _durationTmp.text = NumbersConverter.GetTimeMinSec(config.Duration);
            switch (config.MatchResult)
            {
                case MatchResult.Win:
                    _resultTmp.text = "Победа";
                    break;

                case MatchResult.Lose:
                    _resultTmp.text = "Поражение";
                    break;

                default:
                    _resultTmp.text = "Ничья";
                    break;
            }

        }
    }

    private void SetVisible(bool isVisible)
    {
        foreach (GameObject go in _allComponents)        
            go.SetActive(isVisible);        
    }
}
