using UnityEngine;
using System.Collections;

public class LightFlicker : MonoBehaviour {
	private int randomNumber;
	public Light flickerLight;
	public float flickerTime, pauseTimeBetweenFlickers, dimLightTime;
	private float dimLight;
	// Use this for initialization
	void Start () 
	{
		flickerLight.enabled = true;
		StartCoroutine (flicker ());
	}
	IEnumerator flicker()
	{
		while (true)
		{
			randomNumber = Random.Range (1, 3);
			//dimLight = 1;
			if (randomNumber == 1)
			{
				/* need to test with lights in the game
				for (int i = 0; i <= 10; i++)
				{
					
					dimLight -= .05f;
					yield return new WaitForSeconds (dimLightTime);
					flickerLight.intensity = dimLight;
					//Debug.Log ("i = " + i.ToString ());
				}
				flickerLight.intensity = 1;*/
				flickerLight.enabled = false;
				yield return new WaitForSeconds (flickerTime);
				flickerLight.enabled = true;
			}
			else
			{	
				/* need to test with lights in the game
				for (int i = 0; i <= 10; i++)
				{
					
					dimLight -= .05f;
					yield return new WaitForSeconds (dimLightTime);
					flickerLight.intensity = dimLight;
					//Debug.Log ("i = " + i.ToString ());
				}
				flickerLight.intensity = 1;*/
				flickerLight.enabled = false;
				yield return new WaitForSeconds (flickerTime);
				flickerLight.enabled = true;
				yield return new WaitForSeconds (flickerTime);
				flickerLight.enabled = false;
				yield return new WaitForSeconds (flickerTime);
				flickerLight.enabled = true;
			}

			yield return new WaitForSeconds (pauseTimeBetweenFlickers);
		}
	}
}
