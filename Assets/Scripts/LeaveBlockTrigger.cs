using UnityEngine;

public class LeaveBlockTrigger : MonoBehaviour
{
    private bool _alreadyTriggered = false;
    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (!collider.tag.Equals("Player")|| _alreadyTriggered) return;
        LevelGenerator.sharedInstance.AddNewBlock();
        LevelGenerator.sharedInstance.RemoveOldBlock();
        _alreadyTriggered = true;
    }
}