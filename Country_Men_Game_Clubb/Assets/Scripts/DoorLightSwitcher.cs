using UnityEngine;
using System.Collections;

public class DoorLightSwitcher : MonoBehaviour {
	public Light greenLight, redLight;
	public GameObject[] generator;
	public Animator doorAnimator;
	public int whichDoorAnimation;
	private bool changeLight, doorIsActive;
	// Use this for initialization
	void Start () {
		redLight.enabled = true;
		greenLight.enabled = false;
	}
	public void checkGenerator()
	{
		for (int i = 0; i <= generator.Length; i++)
		{
			if (generator [i].GetComponent<Generator> ().isActive == true)
			{
				changeLight = true;
			}
			else
			{
				changeLight = false;
				break;
			}
		}
		if (changeLight == true)
		{
			greenLight.enabled = true;
			redLight.enabled = false;
			doorIsActive = true;
		}
		else
		{
			redLight.enabled = true;
			greenLight.enabled = false;
		}
	}
	void onTriggerEnter(Collider other)
	{
		if (other.tag == "Player" && doorIsActive == true)
		{
			doorAnimator.SetInteger ("whichAnimationOpen", whichDoorAnimation);
			doorAnimator.SetTrigger ("doorOpen");
		}
	}
	void onTriggerExit(Collider other)
	{
		if (other.tag == "Player" && doorIsActive == true)
		{
			doorAnimator.SetInteger ("whichAnimationClose", whichDoorAnimation);
			doorAnimator.SetTrigger ("doorClose");
		}
	}

}
