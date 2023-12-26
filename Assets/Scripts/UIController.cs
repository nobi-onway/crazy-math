using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject _answerUI;
    [SerializeField] private GameObject _mathUI;
    [SerializeField] private GameObject _countDownBar;
    [SerializeField] private Button _playButton;
    [SerializeField] private GameObject _titleUI;
    [SerializeField] private TextMeshProUGUI _scoreUI;
    [SerializeField] private GameObject _losePanel;
    [SerializeField] private TextMeshProUGUI _currentScore;
    [SerializeField] private TextMeshProUGUI _bestScore;
    [SerializeField] private Button _replayButton;

    private void Start()
    {
        _playButton.onClick.AddListener(() => { GameController.Instance.State = GameController.GameState.running; });
        _replayButton.onClick.AddListener(() => { GameController.Instance.State = GameController.GameState.lobby; });

        GameController.Instance.OnStateChange += state =>
        {
            ShowGamePlayUIIf(state == GameController.GameState.running);
            ShowGameLobbyIf(state == GameController.GameState.lobby);
            ShowLosePanelIf(state == GameController.GameState.over);
        };

        GameController.Instance.OnScoreChange += score =>
        {
            _currentScore.text = score.ToString();
            _bestScore.text = PlayerPrefs.GetInt("BEST_SCORE").ToString();
            UpdateScoreUI(score);
        };
    }

    private void ShowGamePlayUIIf(bool canShow)
    {
        _answerUI.SetActive(canShow);
        _mathUI.SetActive(canShow);
        _countDownBar.SetActive(canShow);
        _scoreUI.gameObject.SetActive(canShow);
    }

    private void ShowGameLobbyIf(bool canShow)
    {
        _titleUI.gameObject.SetActive(canShow);
        _playButton.gameObject.SetActive(canShow);
    }

    private void ShowLosePanelIf(bool canShow)
    {
        _losePanel.gameObject.SetActive(canShow);
    }

    private void UpdateScoreUI(int score)
    {
        _scoreUI.text = $"Score: {score}";
    }
}
