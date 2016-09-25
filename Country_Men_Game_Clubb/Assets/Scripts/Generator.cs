using UnityEngine;
using System.Collections;

public class Generator : MonoBehaviour {
	public bool isActive;
	public bool isRunning;
	public bool isStarting;

	public AudioClip generatorStartSound;
	public AudioClip generatorLoopSound;

	private AudioSource myAudio;

	//public GameObject door;
	// Use this for initialization
	void Start () {
		isActive = true;
		isStarting = false;
		isRunning = false;

		myAudio = GetComponent<AudioSource>();
		myAudio.clip = generatorStartSound;
	}

	void Update() {

		if( isRunning && isStarting ) {
			
			// Are we finished starting?
			if( !myAudio.isPlaying ) {

				//Start looping
				myAudio.clip = generatorLoopSound;
				myAudio.loop = true;
				myAudio.Play();

				isStarting = false;
			}
		}
	}

	void OnTriggerStay(Collider other)
	{
		Debug.Log (other.tag);
		if (other.tag == "Player" && Input.GetKeyDown(KeyCode.E) )
		{
			Debug.Log ("test");

			// Don't activate if we're already active
			if( isActive && !isRunning ) {
				Debug.Log ("test");
				isRunning = true;
				isStarting = true;
				myAudio.Play();
				//door.GetComponent<DoorLightSwitcher> ().checkGenerator ();
			}
		}
	}
		
	void Deactivate() {

		isActive = false;
		isRunning = false;
		isStarting = false;

		myAudio.Stop();
	}
}
