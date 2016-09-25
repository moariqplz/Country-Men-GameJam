using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class MicInput : MonoBehaviour {

	// Variables for tracking loudness of recording
	public float sensitivity = 100;
	public float loudness = 0;

	// Private variables for playing sound, the soundclip itself and the currently used audio device
	private AudioSource audioSource; // Where we play our sample from
	private AudioClip audioSample; // Sample of what the player just said
	private string audioDevice = null; // The device we're using
	private int maxFreq;

	private float getDeviceAfterDelay;

	// For room level detection
	private bool roomLevelCollected = false;
	private float roomLevel;

	// Start of play
	void Start () {

		// Get our component
		audioSource = GetComponent<AudioSource>();

		// Get a default device
		InitDevice();

		// Collect room tone once
		//StartCoroutine(CollectLevels());
	}
	
	// Update is called once per frame
	void Update () {
	
		// Adjust the loudness of the mic playback
		loudness = GetAverageVolume()*sensitivity;

		// Start recording
		if( Input.GetKeyDown(KeyCode.R) ) {
			audioSource.clip = Microphone.Start(audioDevice, true, 256, maxFreq);
			// Collect room tone once
			//StartCoroutine(CollectLevels());
			Debug.Log("keydown!");
		}

		else if( Input.GetKeyUp(KeyCode.R) ) {
			//Microphone.End(audioDevice);
		}

		if(Input.GetKeyDown(KeyCode.P)) {
			audioSource.Play();
		}
	
	}

	// Capture a microphone device from this list, return true if the first one we run into works
	void InitDevice() {

		if( Microphone.devices.Length <= 0 ) {
			Debug.Log("No valid recording devices found!");
			return;
		}

		// Assign the mic to the first device
		audioDevice = Microphone.devices[0];
		Debug.Log("Selected device: " + audioDevice);

		// Get maxfrequency
		int minFreq = 0;
		Microphone.GetDeviceCaps(null, out minFreq, out maxFreq);
		if( minFreq == 0 && maxFreq == 0 ) {
			maxFreq = 44100;
		}
	}

	// Check the loudness of input
	float GetAverageVolume() {
		float a = 0;
		float[] data = new float[256];
		audioSource.GetOutputData(data, 0);


		foreach(float s in data)
		{
			a += Mathf.Abs(s);
		}

		return a/256;
	}

	IEnumerator CollectLevels() {
		
		// Start a test recording
		audioSource.clip = Microphone.Start(audioDevice, true, 20, maxFreq);

		// Wait five seconds to collect room tone
		yield return new WaitForSecondsRealtime(5.0f);

		Microphone.End(audioDevice); // Stop recording

		//audioSource.mute = true;
		audioSource.Play();

		int numSamples = 0;

		//Wait for the clip to finish playing
		while( audioSource.isPlaying ) {
			roomLevel += Mathf.Abs(loudness);
			numSamples++;

			yield return new WaitForSeconds(0.01f);
		}

		roomLevel = roomLevel/numSamples;
		Debug.Log("Room Level: " + roomLevel);
	}
}
