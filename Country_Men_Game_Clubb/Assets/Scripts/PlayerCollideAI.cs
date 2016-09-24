using UnityEngine;
using System.Collections;

public class PlayerCollideAI : MonoBehaviour {

	public GameController gameController;

	// Use this for initialization
	void Start () {
		GameObject gameControllerObject = GameObject.FindGameObjectWithTag("GameController");

		if(gameController != null) {
			gameController = gameControllerObject.GetComponent<GameController>();
		}

		else {
			Debug.Log("PlayerCollideAI could not find 'GameController'!");
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
	
		if(other.tag == "Enemy1" || other.tag == "Enemy2")
		{
			Debug.Log("Player was touched by Enemy1 or Enemy2!");
		}
		else if(other.tag == "Enemy3") {
			Debug.Log("Player was touched by Enemy3, do nothing!");
		}
	}
}
