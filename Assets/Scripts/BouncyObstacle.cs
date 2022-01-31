using UnityEngine;

public class BouncyObstacle : MonoBehaviour
{
    [SerializeField] private float yDifferenceRank;
    [SerializeField] private float bouncyAmount;

    public void OnTriggerEnter2D(Collider2D colliderObject)
    {
        if (!colliderObject.tag.Equals("Player")) return;

        var temp = PlayerController.sharedInstance.transform;
        temp.position = new Vector3(temp.position.x - bouncyAmount, temp.position.y);
    }
}