using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Timer : MonoBehaviour {
	public float timeLeft = 60.0f;
	private float seconds, minutes;
	public Text timerText;
	public Color normalColor, emergencyColor;
	public float textSmallestPoint, textGreatestPoint;
	public GameController gameController;
	public AudioClip[] clip;
	private AudioSource audioSource;
	// Update is called once per frame
	void Update () 
	{
		timeLeft -= Time.deltaTime;

		minutes = (int)(timeLeft / 60f);
		seconds = (int)(timeLeft % 60f);
		timerText.text = minutes.ToString ("00") + ":" + seconds.ToString ("00");
		if (timeLeft <= 60)
		{
			timerText.color = Color.Lerp (normalColor, emergencyColor, Mathf.PingPong (Time.time, 1));
			if (timeLeft <= 20.99f)
			{
				timerText.fontSize = (int)Mathf.Lerp (textSmallestPoint, textGreatestPoint, Mathf.PingPong (Time.time, .5f));
			}
			if (timeLeft <= 10.99f)
			{
				timerText.fontSize = (int)Mathf.Lerp (textSmallestPoint, textGreatestPoint + 35, Mathf.PingPong (Time.time, .25f));
			}
			switch ((int)timeLeft)
			{
				case 5:
					audioSource.clip = clip[4];
					audioSource.Play ();
					break;
				case 4:
					audioSource.clip = clip[3];
					audioSource.Play ();
					break;
				case 3:
					audioSource.clip = clip[2];
					audioSource.Play ();
					break;
				case 2:
					audioSource.clip = clip[1];
					audioSource.Play ();
					break;
				case 1:
					audioSource.clip = clip[0];
					audioSource.Play ();
					break;
				case 0:
					gameController.GetComponent<GameController> ().GameOver ();
					break;
			}


		}
	}
}
