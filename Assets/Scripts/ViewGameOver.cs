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
        _scoreboard.UpdateScore((int)Math.Ceiling(PlayerController.sharedInstance.GetDistance()));
        var scores = _scoreboard.RetrieveScores();
        foreach (var score in scores) Instantiate(scoreRowPrefab, scoreBoardView).Initialize(score.document);
    }
}