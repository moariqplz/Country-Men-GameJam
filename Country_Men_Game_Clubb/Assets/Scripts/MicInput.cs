using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class MicInput : MonoBehaviour {

	// Variables for tracking loudness of recording
	public float sensitivity = 100;
	public float loudness = 0;
    public int playerVoice;

	public AudioMixerGroup mixer0;
	public AudioMixerGroup mixer1;
	public AudioMixerGroup mixer2;
	public AudioMixerGroup mixer3;

	// Private variables for playing sound, the soundclip itself and the currently used audio device
	public AudioSource audioSource; // Where we play our sample from
	private AudioClip audioSample; // Sample of what the player just said
	private string audioDevice = null; // The device we're using
	public int maxFreq;

	// Start of play
	void Start () {

		// Get our component

        //default mixer
        SetMixer(0);

        // Get a default device
        InitDevice();

        // Collect room tone once
        //StartCoroutine(CollectLevels());

        audioDevice = Microphone.devices[0];

        audioSource.clip = Microphone.Start("Built-in Microphone", true, 10, 44100);
        audioSource.loop = true;
        while (!(Microphone.GetPosition(null) > 0)) { }
        audioSource.Play();

    }
	
	// Update is called once per frame
	void Update () {
	
		// Adjust the loudness of the mic playback
		loudness = GetAverageVolume()*sensitivity;

        //audio is always active
        
        //audioSource.clip = Microphone.Start(audioDevice, false, 10, 44100);
        ////audioSource.mute = true; // Mute the sound, we don't want the player to hear it
        ////while (!(Microphone.GetPosition(audioDevice) > 0)) { } // Wait until the recording has started
        //if(!audioSource.isPlaying)
        //{
        //    audioSource.Play();
        //}


        // Start recording
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SetMixer(0);
        }
        if ( Input.GetKeyDown(KeyCode.Alpha1) )
        {
			SetMixer(1);
		}
		if( Input.GetKeyDown(KeyCode.Alpha2) )
        {
			SetMixer(2);
		}
		if( Input.GetKeyDown(KeyCode.Alpha3) )
        {
			SetMixer(3);
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

	}

	void StopRecording() {
		audioSource.Stop();
		Microphone.End(audioDevice);
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
        playerVoice = n;
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
