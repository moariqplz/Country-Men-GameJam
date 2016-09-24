using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Timer : MonoBehaviour {
	public float timeLeft = 60.0f;
	private float seconds, minutes;
	public Text timerText;
	public Color normalColor, emergencyColor;
	public float textSmallestPoint, textGreatestPoint;
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
		}
	}
}
