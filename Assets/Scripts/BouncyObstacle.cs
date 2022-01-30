using UnityEngine;

public class BouncyObstacle : MonoBehaviour
{
    [SerializeField] private float yDifferenceRank;
    [SerializeField] private float bouncyAmount;

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag.Equals("Frontal"))
        {
            Debug.Log("Hits in the front " + transform.name + "-" + transform.parent.name + " with " + collider.name);
            var temp = PlayerController.sharedInstance.transform;
            temp.position = new Vector3(temp.position.x - bouncyAmount, temp.position.y);
            PlayerController.sharedInstance.bounce = true;
        }
    }
}