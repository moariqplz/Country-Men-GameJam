using UnityEngine;
using System.Collections;

public class AIAudioController : MonoBehaviour {
	private int randomNumber;
	public AudioClip[] clip;
	private AudioSource audioSource;
	// Use this for initialization
	void Start()
	{
		StartCoroutine (MakeSound ());
	}
	// Update is called once per frame
	IEnumerator MakeSound() 
	{	
		yield return new WaitForSeconds(5);
		PickRandomSound	(RandomNumberGenerator ());	
	}
	int RandomNumberGenerator()
	{
		return randomNumber = Random.Range (1, 4);
	}

	void PickRandomSound(int randomNumber)
	{
		switch (randomNumber)
		{
			case 1:
				audioSource.clip = clip[0];
				audioSource.Play ();
				break;
			case 2:
				audioSource.clip = clip[1];
				audioSource.Play ();
				break;
			case 3:
				audioSource.clip = clip[2];
				audioSource.Play ();
				break;
				
		}
	}
}
