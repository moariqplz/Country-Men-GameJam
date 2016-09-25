using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameController : MonoBehaviour {
	[SerializeField] bool playerIsAlive, levelCompleted, restartLevel, resumeLevel, quitLevel, playerHasPowerUp, powerUpIsActive;
	private int score; 
	public Text scoreText, comformationQuitText, gameOverText, WinningText;
	public GameObject restartButton, quitButton, resumeButton,comformationNoButton, comformationYesButton, player;
	[SerializeField] private MouseLook m_MouseLook;
	public float powerUpEffectTime;
	public AudioClip clip;
	// Use this for initialization
	private AudioSource audioSource;
	void Start () 
	{
		audioSource.GetComponent<AudioSource> ();
		powerUpIsActive = false;
		playerIsAlive = false;
		gameOverText.enabled = false;
		WinningText.enabled = false;
		comformationQuitText.enabled = false;
		/*comformationNoButton = comformationNoButton.SetActive(false);
		comformationYesButton = comformationYesButton.SetActive(false);
		restartButton = restartButton = restartButton.SetActive(false);
		quitButton = quitButton.SetActive(false);
		resumeButton = resumeButton.SetActive(false);*/
		score = 0;
		scoreText.text = "Score: " + score.ToString ();
		audioSource.clip = clip;
		audioSource.Play ();
		audioSource.loop = true;
			
	}
	void Update()
	{
		if(Input.GetKeyDown (KeyCode.Escape))//Pause Menu
		{
			Pause();
		}
		if(Input.GetKeyDown (KeyCode.P)) //Using Player PowerUp
		{
			PlayerUsedPowerUp();
			Invoke ("DisablePowerUp", powerUpEffectTime);
		}


	}
	void Pause()
	{
		m_MouseLook.SetCursorLock(false);
		Time.timeScale = 0;
		restartButton.SetActive(true);
		quitButton.SetActive(true);
		resumeButton.SetActive(true);
		player.GetComponent<FirstPersonController> ().enabled = false;
	}
	public void ResumeButton()
	{
		
		Cursor.visible = true;
		Time.timeScale = 1;
		restartButton.SetActive(false);
		quitButton.SetActive(false);
		resumeButton.SetActive(false);
		player.GetComponent<FirstPersonController> ().enabled = true;
	}
	public void RestartButton()
	{
		string activeScene = SceneManager.GetActiveScene ().name;
		SceneManager.LoadScene (activeScene);
		Time.timeScale = 1;
	}
	public void QuitButton()
	{
		restartButton.SetActive(false);
		quitButton.SetActive(false);
		resumeButton.SetActive(false);
		comformationQuitText.enabled = true;
		comformationYesButton.SetActive(true);
		comformationNoButton.SetActive(true);
	}
	public void ComformationYesButton()
	{
		Application.Quit ();
	}
	public void ComformationNoButton()
	{
		restartButton.SetActive(true);
		quitButton.SetActive(true);
		resumeButton.SetActive(true);
		comformationQuitText.enabled = false;
		comformationYesButton.SetActive(false);
		comformationNoButton.SetActive(false);
	}
	public void GameOver(bool isPlayerAlive) //checking if the player died
	{
		if(isPlayerAlive == false)//if player is dead end game
		{
			Cursor.visible = true;
			Time.timeScale = 0;
			gameOverText.enabled = true;
			restartButton.SetActive(true);
			player.GetComponent<FirstPersonController> ().enabled = false;
		}
	}
	public void LevelCompletion(bool levelCompleted)
	{
		if (levelCompleted == true)
		{

			Cursor.visible = true;
			Time.timeScale = 0;
			WinningText.enabled = true;
			restartButton.SetActive(true);
			player.GetComponent<FirstPersonController> ().enabled = false;
		}

	}

	public void AddPoints()
	{

		score += 100;
		scoreText.text = "Score: " + score.ToString ();
	}
	public void SetPlayerHasPowerUp(bool playerHasPowerUp)
	{
		print ("PowerUp: " + playerHasPowerUp);
		this.playerHasPowerUp = playerHasPowerUp;
		print ("GameController: " + playerHasPowerUp);
	}
	public void PlayerUsedPowerUp()
	{
		playerHasPowerUp = false;
		powerUpIsActive = true;

	}
	public bool GetIfPowerUpActive()
	{
		return powerUpIsActive;
	}
	void DisablePowerUp()
	{
		powerUpIsActive = false;
	}
}
