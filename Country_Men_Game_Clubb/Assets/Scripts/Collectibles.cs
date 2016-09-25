using UnityEngine;
using System.Collections;
public class Collectibles : MonoBehaviour {
	public GameObject player;
	public AudioClip clip;
	private AudioSource audioSource;
	// Use this for initialization
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			audioSource.clip = clip;
			audioSource.Play ();
			Destroy (this.gameObject);
		}
	}
}
