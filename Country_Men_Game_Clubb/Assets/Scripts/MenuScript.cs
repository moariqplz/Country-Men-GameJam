using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {

	public Canvas quitMenu;
	public Button newGameButt;
	public Button quitGameButt;

	public float zoomTime = 1.5f;
	private float zoomEnd;
	private bool zooming;
	// Use this for initialization
	void Start () {

		quitMenu = quitMenu.GetComponent<Canvas>();
		newGameButt = newGameButt.GetComponent<Button>();
		quitGameButt = quitGameButt.GetComponent<Button>();

		quitMenu.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	
		if( zooming ) {
			if( zoomEnd >= Time.time ) {
				Camera.main.fieldOfView -= 0.09f;
			}
			else {
				zooming = false;
				SceneManager.LoadScene(1);
			}
		}


	}

	public void ExitPressed() {
		quitMenu.enabled = true;
		newGameButt.enabled = false;
		quitGameButt.enabled = false;
	}

	public void NoPressed() {
		quitMenu.enabled = false;
		newGameButt.enabled = true;
		quitGameButt.enabled = true;
	}

	public void StartGame() {
		zooming = true;
		zoomEnd = Time.time + zoomTime;
		Canvas menu = GetComponent<Canvas>();
		menu.enabled = false;

	}

	public void ExitGame() {
		Application.Quit();
	}
}
