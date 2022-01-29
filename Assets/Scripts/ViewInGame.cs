using UnityEngine;
using UnityEngine.UI;

public class ViewInGame : MonoBehaviour
{
    public static ViewInGame sharedInstance;

    public Text coinLabel;

    public Text scoreLabel;

    public Text highScoreLabel;

    private void Awake()
    {
        sharedInstance = this;
    }


    // Update is called once per frame
    private void Update()
    {
        if (GameManager.sharedInstance.currentGameState == GameState.inTheGame)
            scoreLabel.text = PlayerController.sharedInstance.GetDistance().ToString("f0");
    }

    public void UpdateHighScoreLabel()
    {
        if (GameManager.sharedInstance.currentGameState == GameState.inTheGame)
            highScoreLabel.text = PlayerPrefs.GetFloat("highScore", 0).ToString("f0");
    }

    public void UpdateCoinsLabel()
    {
        if (GameManager.sharedInstance.currentGameState == GameState.inTheGame)
            coinLabel.text = GameManager.sharedInstance.collectedCoins.ToString();
    }
}