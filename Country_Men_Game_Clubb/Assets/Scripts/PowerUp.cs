using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour {
	private Transform collectibleTransform;
	[SerializeField] private float rotationSpeed;
	public GameObject player;
	public int bobbingAmount;
	public float timeToCompleteBobbing;
	private GameObject gameController;
	private GameController gameControllerScript;
	private bool playerHasPowerUp;
	public AudioClip clip;
	private AudioSource audioSource;
	// Use this for initialization
	void Start () 
	{
		gameController = GameObject.Find ("Game Controller");
		gameControllerScript = gameController.GetComponent<GameController> ();
		collectibleTransform = GetComponent<Transform> ();
		playerHasPowerUp = false;
		audioSource.GetComponent<AudioSource> ();

	}

	// Update is called once per frame
	void Update () 
	{
		this.transform.Rotate (Vector3.up * (rotationSpeed * Time.deltaTime));
		this.transform.position = Vector3.Lerp(new Vector3(transform.position.x, 2, transform.position.z), new Vector3(transform.position.x, transform.position.y + bobbingAmount, transform.position.z), Mathf.PingPong(Time.deltaTime, timeToCompleteBobbing));

	}
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			audioSource.clip = clip;
			audioSource.Play ();
			playerHasPowerUp = true;
			gameControllerScript.SetPlayerHasPowerUp (playerHasPowerUp);
			Destroy (this.gameObject);
		}
	}
}
