using System;
using UnityEngine;

public class ViewGameOver : MonoBehaviour
{
    [SerializeField] private ScoreRow scoreRowPrefab;
    [SerializeField] private Transform scoreBoardView;
    private Scoreboard _scoreboard;

    private void Awake()
    {
        _scoreboard ??= GetComponent<Scoreboard>();
    }

    private void Start()
    {
        GameManager.sharedInstance.OnGameStateChangeEvent += OnGameStateChangeEvent;
    }

    private void Update()
    {
        if (GameManager.sharedInstance.currentGameState == GameState.gameOver &&
            (Input.GetKeyDown("space") || Input.GetMouseButtonDown(0)))
            GameManager.RestartGame();
    }

    private void OnDestroy()
    {
        GameManager.sharedInstance.OnGameStateChangeEvent -= OnGameStateChangeEvent;
    }

    private void OnGameStateChangeEvent(GameState newGameState)
    {
        if (newGameState == GameState.gameOver) UpdateScores();
    }

    private void UpdateScores()
    {
        var finalScore = (int)Math.Ceiling(PlayerController.sharedInstance.GetDistance());
        StartCoroutine(Scoreboard.UpdateScore(finalScore, () =>
        {
            StartCoroutine(Scoreboard.RetrieveScores((root) =>
            {
                foreach (var score in root.answer) Instantiate(scoreRowPrefab, scoreBoardView).Initialize(score);
            }));
        }));
    }
}