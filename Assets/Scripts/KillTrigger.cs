using UnityEngine;

public class KillTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D theObject)
    {
        if (theObject.tag == "Player") //Debug.Log ("el conejo ha tocado la muerte");//desencadena la muerte cuando toca la zona de muerte
            PlayerController.sharedInstance.KillPlayer();
    }
}