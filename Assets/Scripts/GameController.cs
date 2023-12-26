using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public enum GameState { lobby, running, over }

    #region UI
    [SerializeField] private Button _argeeButton;
    [SerializeField] private Button _disagreeButton;
    #endregion

    [SerializeField] private MathController _mathController;
    [SerializeField] private CountDownBarController _countdownBarController;

    private static GameController _instance;
    public static GameController Instance { get => _instance; }

    public event Action<GameState> OnStateChange;
    public event Action<int> OnScoreChange;

    public int Score
    {
        get => _score;
        set
        {
            _score = value;
            OnScoreChange?.Invoke(value);
        }
    }
    private int _score;

    public GameState State 
    { 
       get => _state;
       set 
       {
          _state = value;
          OnStateChange?.Invoke(value);
       } 
    }
    private GameState _state;

    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        State = GameState.lobby;

        OnStateChange += state =>
        {
            if (state == GameState.running)
                StartGame();
            if (state == GameState.over)
                StopGame();
        };
    }

    private void StartGame()
    {
        Score = 0;
        _mathController.RandomOperation();
        _countdownBarController.StartCountDown();
        _countdownBarController.OnTimeOut += Lose;
        AddEventListener();
    }

    private void StopGame()
    {
        _countdownBarController.StopCountDown();
    }

    private void AddEventListener()
    {
        RemoveEventListener();
        _argeeButton.onClick.AddListener(() => WinIf(_mathController.IsCorrectOperation()));
        _disagreeButton.onClick.AddListener(() => WinIf(!_mathController.IsCorrectOperation()));
    }

    private void RemoveEventListener()
    {
        _argeeButton.onClick.RemoveAllListeners();
        _disagreeButton.onClick.RemoveAllListeners();
    }

    private void WinIf(bool condition)
    {
        if (condition)
            Win();
        else
            Lose();
    }

    private void Win()
    {
        AddEventListener();
        Score += (int) (10 * _countdownBarController.GetCurrentProcessTime());
        _mathController.RandomOperation();
        ResetCountDown();
    }

    private void Lose()
    {
        SetMaxScore(_score);
        State = GameState.over;
    }

    private void ResetCountDown()
    {
        _countdownBarController.StopCountDown();
        _countdownBarController.StartCountDown();
    }

    private void SetMaxScore(int score)
    {
        if (score <= PlayerPrefs.GetInt("BEST_SCORE")) return;

        PlayerPrefs.SetInt("BEST_SCORE", score);
    }
}
