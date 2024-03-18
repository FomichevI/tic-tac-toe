using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreTmp;

    private void Start()
    {
        SetScore();
    }

    private void SetScore()
    {
        _scoreTmp.text = GameManager.Instance.DataManager.GetCurrentScore().ToString();
    }

    public void OnPlayWithBotClick()
    {
        PopupManager.instance.ShowComplexityPopup(() => PlayWithBot(BotType.Easy), 
            () => PlayWithBot(BotType.Normal), () => PlayWithBot(BotType.Hard));
    }

    /// <summary>
    /// ��������� ���� � ����� ���������� ������ ���������
    /// </summary>
    private void PlayWithBot(BotType type)
    {
        BotConfig bot = GameManager.Instance.GameSettings.GetBotConfig(type);
        GameManager.Instance.SessionConfig.SetNextOpponent(bot);
        SceneLoader.Instance.LoadGameplayScene();
    }

    public void OnTwoPlayersClick()
    {
        // ��������� ���� �� ���� ������� �� ����� �������
        PlayerData opponent = new PlayerData("����� 2");
        GameManager.Instance.SessionConfig.SetNextOpponent(opponent);
        SceneLoader.Instance.LoadGameplayScene();
    }
}
