using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    //estados del juego
    menu,
    Intro,
    inTheGame,
    Dying,
    gameOver
}

public class GameManager : MonoBehaviour
{
    public static GameManager sharedInstance;

    public GameState currentGameState = GameState.menu;

    public Canvas menuCanvas;
    public Canvas gameCanvas;
    public Canvas gameOverCanvas;

    public int collectedCoins;


    private void Awake()
    {
        sharedInstance = this;
    }

    private void Start()
    {
        //TODO: Check currentGameState = GameState.menu;
        menuCanvas.enabled = false;
        //TODO: Check gameCanvas.enabled = false;
        //TODO: Check gameOverCanvas.enabled = false;
    }

    public event Action<GameState> OnGameStateChangeEvent;

    public void StartGame()
    {
        PlayerController.sharedInstance.StartGame();
        LevelGenerator.sharedInstance.GenerateInitialBlocks();
        ChangeGameState(GameState.inTheGame);
        // ViewInGame.sharedInstance.UpdateHighScoreLabel();
    }

    public void GameOver()
    {
        ChangeGameState(GameState.gameOver);
    }

    public void BackToMainMenu()
    {
        ChangeGameState(GameState.menu);
    }

    private void ChangeGameState(GameState newGameState)
    {
        OnGameStateChangeEvent?.Invoke(newGameState);
        switch (newGameState)
        {
            case GameState.menu:
                menuCanvas.enabled = true;
                gameCanvas.enabled = false;
                gameOverCanvas.enabled = false;

                break;
            case GameState.inTheGame:
                menuCanvas.enabled = false;
                gameCanvas.enabled = true;
                gameOverCanvas.enabled = false;
                break;
            case GameState.gameOver:
                menuCanvas.enabled = false;
                gameCanvas.enabled = false;
                gameOverCanvas.enabled = true;
                break;
            case GameState.Intro:
                break;
            case GameState.Dying:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newGameState), newGameState, null);
        }

        currentGameState = newGameState;
    }

    public void CollectCoin()
    {
        collectedCoins++;
        ViewInGame.sharedInstance.UpdateCoinsLabel();
    }

    public static void RestartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
}