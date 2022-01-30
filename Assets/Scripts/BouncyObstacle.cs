using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyObstacle : MonoBehaviour
{
    [SerializeField]private float yDifferenceRank;
    [SerializeField] private float bouncyAmount;
    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag.Equals("Frontal") && IsInFrontOf(collider))
        {
            Debug.Log("Hits in the front "+transform.name+" with "+collider.name);
            // var temp = PlayerController.sharedInstance.transform;
            // temp.position = new Vector3(temp.position.x - bouncyAmount, temp.position.y);
            PlayerController.sharedInstance.bounce = true;
        }
    }

    public bool IsInFrontOf(Collider2D collider)
    {
        var yDiff = transform.position.y - collider.transform.position.y;
        yDiff = Math.Abs(yDiff);
        return yDiff < yDifferenceRank;
    }
}
