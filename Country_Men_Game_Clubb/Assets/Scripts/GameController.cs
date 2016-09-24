using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameController : MonoBehaviour {
	private bool playerIsAlive, levelCompleted, restartLevel;
	private int score; 
	public Image gameOverText, WinningText;


	// Use this for initialization
	void Start () {
		playerIsAlive = true;
		gameOverText.enabled = false;
		WinningText.enabled = false;
		score = 0;
	}

	void GameOver(bool isPlayerAlive) //checking if the player died
	{
		if (playerIsAlive == true) //if player is alive it is not gameover
		{ 
			return;
		}
		else // if the player is anything else than alive gameOver
		{
			gameOverText.enabled = true;
			if (restartLevel == true)
			{

				SceneManager.LoadScene ("Test");
			}
		}
	}
	void levelCompletion(bool levelCompleted)
	{
		if (levelCompleted == true)
		{
			WinningText.enabled = true;
			if (restartLevel == true)
			{

				SceneManager.LoadScene ("Test");
			}
		}
		else
		{
			return;
		}

	}
	public void restartButtonPress()
	{
		restartLevel = true;
	}
	public int addPoints()
	{
		return score += 100;
	}

}
