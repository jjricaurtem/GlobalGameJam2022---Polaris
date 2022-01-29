using UnityEngine;

public class Collectable : MonoBehaviour
{
    private bool isCollected;

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.tag == "Player") CollectCoin();
    }

    private void ShowCoin()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<CircleCollider2D>().enabled = true;
        isCollected = false;
    }

    private void HideCoin()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
    }

    private void CollectCoin()
    {
        isCollected = true;
        HideCoin();
        //notifica al manager que se recogio la moneda
        GameManager.sharedInstance.CollectCoin();
    }
}