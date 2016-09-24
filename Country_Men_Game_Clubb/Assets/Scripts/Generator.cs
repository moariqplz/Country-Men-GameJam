using UnityEngine;
using System.Collections;

public class Generator : MonoBehaviour {
	public bool isActive;
	public GameObject door;
	// Use this for initialization
	void Start () {
		isActive = false;
	}
	void onTriggerStay(Collider other)
	{
		if (other.tag == "Player" && Input.GetButtonDown (KeyCode.E))
		{
			isActive = true;
			door.GetComponent<DoorLightSwitcher> ().checkGenerator ();
		}
	}
}
