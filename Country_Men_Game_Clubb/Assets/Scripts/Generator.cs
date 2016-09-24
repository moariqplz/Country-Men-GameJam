using UnityEngine;
using System.Collections;

public class Generator : MonoBehaviour {
	public bool isActive;
	//public GameObject door;
	// Use this for initialization
	void Start () {
		isActive = false;
	}
	void OnTriggerStay(Collider other)
	{
		Debug.Log (other.ToString ());
		if (other.tag == "Player"  && Input.GetKeyDown(KeyCode.E))
		{
			Debug.Log ("test");
			isActive = true;
			//door.GetComponent<DoorLightSwitcher> ().checkGenerator ();
		}
	}
}
