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
    /// ���������� ������� ���� ������ �� ������� ������
    /// </summary>
    // � ������� ����� ��������� � ��������� �����
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
    /// ��������� ���� � ����� ���������� ������ ���������
    /// </summary>
    private void PlayWithBot(BotType type)
    {
        BotConfig bot = GameManager.Instance.GameSettings.GetBotConfig(type);
        GameManager.Instance.SessionConfig.SetNextOpponent(bot);
        SceneLoader.Instance.LoadGameplayScene();
    }

    /// <summary>
    /// ��������� ���� �� ���� ������� �� ����� �������
    /// </summary>
    public void OnTwoPlayersClick()
    {
        PlayerInfo opponent = new PlayerInfo("����� 2");
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
