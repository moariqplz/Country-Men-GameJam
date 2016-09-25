using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class MicInput : MonoBehaviour {

	// Variables for tracking loudness of recording
	public float sensitivity = 100;
	public float loudness = 0;

	public AudioMixerGroup mixer0;
	public AudioMixerGroup mixer1;
	public AudioMixerGroup mixer2;
	public AudioMixerGroup mixer3;

	// Private variables for playing sound, the soundclip itself and the currently used audio device
	private AudioSource audioSource; // Where we play our sample from
	private AudioClip audioSample; // Sample of what the player just said
	private string audioDevice = null; // The device we're using
	private int maxFreq;

	public float audioActiveTime = 3.5f;
	private float audioActiveEndTime;
	private bool audioActive = false;

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
		if( Input.GetKeyDown(KeyCode.Alpha1) ) {
			SetMixer(1);
		}
		if( Input.GetKeyDown(KeyCode.Alpha0) ) {
			SetMixer(0);
		}
		if( Input.GetKeyDown(KeyCode.Alpha2) ) {
			SetMixer(2);
		}
		if( Input.GetKeyDown(KeyCode.Alpha3) ) {
			SetMixer(3);
		}

		if( Input.GetKeyDown(KeyCode.Space) ) {
			StartRecording();
		}

		if( audioActive && audioActiveEndTime <= Time.time )
		{
			StopRecording();	
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
		Microphone.GetDeviceCaps(audioDevice, out minFreq, out maxFreq);
		if( minFreq == 0 && maxFreq == 0 ) {
			maxFreq = 44100;
		}
	}

	void StartRecording() {
		// Start recording
		audioSource.clip = Microphone.Start(audioDevice, true, 20, maxFreq);
		while (!(Microphone.GetPosition(null) > 0)){}

		audioSource.loop = false;
		audioSource.Play();

		audioActiveEndTime = Time.time + audioActiveTime;
		audioActive = true;
	}

	void StopRecording() {
		audioSource.Stop();
		Microphone.End(audioDevice);
		audioActive = false;
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

	public void SetMixer(int n) {

		switch( n ) {
		case 0:
			audioSource.outputAudioMixerGroup = mixer0;	
			break;

		case 1:
			audioSource.outputAudioMixerGroup = mixer1;
			break;

		case 2:
			audioSource.outputAudioMixerGroup = mixer2;
			break;

		case 3:
			audioSource.outputAudioMixerGroup = mixer3;
			break;

		default:
			audioSource.outputAudioMixerGroup = mixer0;
			break;
		}
	}
}
