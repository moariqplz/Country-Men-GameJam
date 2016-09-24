using UnityEngine;
using System.Collections;
public class Collectibles : MonoBehaviour {
	private Transform collectibleTransform;
	[SerializeField] private float rotationSpeed;
	public GameObject player;
	public int bobbingAmount;
	public float timeToCompleteBobbing;
	// Use this for initialization
	void Start () 
	{
		collectibleTransform = GetComponent<Transform> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		collectibleTransform.Rotate (Vector3.up * (rotationSpeed * Time.deltaTime));
		this.transform.position = Vector3.Lerp(new Vector3(transform.position.x, 2, transform.position.z), new Vector3(transform.position.x, transform.position.y + bobbingAmount, transform.position.z), Mathf.PingPong(Time.deltaTime, timeToCompleteBobbing));

	}
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			Destroy (this.gameObject);
		}
	}
}
