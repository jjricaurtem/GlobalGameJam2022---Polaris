using UnityEngine;
using UnityEngine.UI;

public class ViewGameOver : MonoBehaviour
{
    public static ViewGameOver sharedInstance;

    public Text coinLabel;

    public Text scoreLabel;

    private void Awake()
    {
        sharedInstance = this;
    }


    // Update is called once per frame
    private void Update()
    {
    }


    public void UpdateUI()
    {
        if (GameManager.sharedInstance.currentGameState == GameState.gameOver)
        {
            coinLabel.text = GameManager.sharedInstance.collectedCoins.ToString();
            scoreLabel.text = PlayerController.sharedInstance.GetDistance().ToString("f0");
        }
    }
}