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
        PopupManager.Instance.ShowComplexity(() => PlayWithBot(BotType.Easy), 
            () => PlayWithBot(BotType.Normal), () => PlayWithBot(BotType.Hard));
    }

    /// <summary>
    /// Запустить игру с ботом выбранного уровня сложности
    /// </summary>
    private void PlayWithBot(BotType type)
    {
        BotConfig bot = GameManager.Instance.GameSettings.GetBotConfig(type);
        GameManager.Instance.SessionConfig.SetNextOpponent(bot);
        SceneLoader.Instance.LoadGameplayScene();
    }

    public void OnTwoPlayersClick()
    {
        // Запустить игру на двух игроков на одном девайсе
        PlayerInfo opponent = new PlayerInfo("Игрок 2");
        GameManager.Instance.SessionConfig.SetNextOpponent(opponent);
        SceneLoader.Instance.LoadGameplayScene();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PopupManager.Instance.ShowExit(Application.Quit);
        }
    }
}
