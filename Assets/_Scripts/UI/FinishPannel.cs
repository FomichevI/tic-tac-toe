using DG.Tweening;
using TMPro;
using UnityEngine;

/// <summary>
/// Класс, отвечающий за визуальное оформление панели окончания боя
/// </summary>
public class FinishPannel : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _winText;
    [SerializeField] private GameObject _loseText;
    [SerializeField] private GameObject _drawText;
    [SerializeField] private TextMeshProUGUI _scoreOnHeader; // Данный текст будет вылезать из рамок, если значение счета будет семизначным и более
    [SerializeField] private TextMeshProUGUI _reward;

    private int lastScore = 0;

    public void Show()
    {
        MatchResult result = GameManager.Instance.SessionConfig.MatchResult;
        int reward = GameManager.Instance.GameSettings.GetReward(result);
        int score = GameManager.Instance.DataManager.GetCurrentScore();
        // Включаем необходимую фразу
        switch (result)
        {
            case MatchResult.Win:
                _winText.SetActive(true);
                _loseText.SetActive(false);
                _drawText.SetActive(false);
                break;
            case MatchResult.Lose:
                _winText.SetActive(false);
                _loseText.SetActive(true);
                _drawText.SetActive(false);
                break;
            default:
                _winText.SetActive(false);
                _loseText.SetActive(false);
                _drawText.SetActive(true);
                break;
        }
        // Устанавливаем значение прошлого количества очков в шапке и значение награды
        _reward.text = reward >= 0 ? ("+" + reward) : reward.ToString();
        lastScore = score - reward;
        _scoreOnHeader.text = (lastScore).ToString();
        // Запускаем анимацию появления окна
        _animator.enabled = true;
    }

    /// <summary>
    /// Анимация начисления очков
    /// </summary>
    public void AnimateScoringPoints()
    {
        int currentScore = GameManager.Instance.DataManager.GetCurrentScore();

        DOTween.To(() => lastScore, x => lastScore = x, currentScore, 1f)
            .OnUpdate(() =>
            {
                _scoreOnHeader.text = lastScore.ToString();
            });

        //_scoreOnHeader.GetComponent<RectTransform>()
        //    .DOScale(1.5f, 0.75f)
        //    .SetLoops(2, LoopType.Yoyo);
    }

    public void OnBackToMenuClick()
    {
        _animator.enabled = false;
        SceneLoader.Instance.LoadMenuScene();
    }
}
