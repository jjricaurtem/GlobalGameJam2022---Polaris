using UnityEngine;
using System.Collections;

public class LeaveBlockTrigger : MonoBehaviour {

	public void OnTriggerEnter2D(Collider2D collider){
		LevelGenerator.sharedInstance.AddNewBlock ();
		LevelGenerator.sharedInstance.RemoveOldBlock ();

	} 
}
