using UnityEngine;

public class LeaveBlockTrigger : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collider)
    {
        LevelGenerator.sharedInstance.AddNewBlock();
        LevelGenerator.sharedInstance.RemoveOldBlock();
    }
}