using TMPro;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreTmp;

    private void Start()
    {
        SetScore();
    }

    /// <summary>
    /// Отобразить текущий счет игрока на верхней панеле
    /// </summary>
    // В будущем можно перенести в отдельный класс
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

    /// <summary>
    /// Запустить игру на двух игроков на одном девайсе
    /// </summary>
    public void OnTwoPlayersClick()
    {
        PlayerInfo opponent = new PlayerInfo("Игрок 2");
        GameManager.Instance.SessionConfig.SetNextOpponent(opponent);
        SceneLoader.Instance.LoadGameplayScene();
    }

    public void OnStoreClick()
    {
        StoreManager.Instance.Show();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PopupManager.Instance.ShowExit(Application.Quit);
        }
    }
}
