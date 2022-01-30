using System;
using UnityEngine;

public class ShadowHand : MonoBehaviour
{
    public float runningSpeed = 3.0f;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag.Equals("Player"))
        {
            GameManager.sharedInstance.currentGameState = GameState.Dying;
            PlayerController.sharedInstance.KillPlayer();
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (GameManager.sharedInstance.currentGameState is GameState.inTheGame or GameState.Dying)
        {
            var newXPosition = transform.position.x + runningSpeed * Time.deltaTime;
            transform.position = new Vector2(newXPosition, transform.position.y);
        }
    }
}
