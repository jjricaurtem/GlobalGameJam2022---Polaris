using System;
using UnityEngine;
using UnityEngine.UI;

public class ViewGameOver : MonoBehaviour
{
    [SerializeField] private ScoreRow scoreRowPrefab;
    [SerializeField] private GameObject scoreBoardView;
    [SerializeField] private GameObject saveScorePanel;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text playerNameInput;
    private Scoreboard _scoreboard;

    private void Awake()
    {
        _scoreboard ??= GetComponent<Scoreboard>();
    }

    private void Start()
    {
        GameManager.sharedInstance.OnGameStateChangeEvent += OnGameStateChangeEvent;
    }

    private void OnDestroy()
    {
        GameManager.sharedInstance.OnGameStateChangeEvent -= OnGameStateChangeEvent;
    }

    public void OnSaveScore()
    {
        UpdateScores();
        saveScorePanel.SetActive(false);
        scoreBoardView.SetActive(true);
    }
    
    private void OnGameStateChangeEvent(GameState newGameState)
    {
        if (newGameState == GameState.gameOver)
        {
            var finalScore = (int)Math.Ceiling(PlayerController.sharedInstance.GetDistance());
            saveScorePanel.SetActive(true);
            scoreText.text = finalScore.ToString();
            scoreBoardView.SetActive(false);
        }
    }

    private void UpdateScores()
    {
        var finalScore = (int)Math.Ceiling(PlayerController.sharedInstance.GetDistance());
        StartCoroutine(Scoreboard.UpdateScore(finalScore, playerNameInput.text, () =>
        {
            StartCoroutine(Scoreboard.RetrieveScores((root) =>
            {
                foreach (var score in root.answer) Instantiate(scoreRowPrefab, scoreBoardView.transform).Initialize(score);
            }));
        }));
    }
}