using UnityEngine;
using System.Collections;

public class InfoPanelStateMonitor : MonoBehaviour {

	public GameObject infopanel, black;
	Animator ipanimator, blackanimator;

	public static bool TRIGGER = false;

	bool shown = false;

	// Use this for initialization
	void Start () {
		ipanimator = infopanel.GetComponent<Animator> ();
		blackanimator = black.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (TRIGGER) {
			TRIGGER = false;
			ipanimator.SetTrigger ("Move");
			shown = !shown;
			if (shown) {
				Handheld.Vibrate();
			}

			black.gameObject.SetActive (true);
			blackanimator.SetTrigger ("Fade");
		}
	}

	public void Toggle(){
		if (!MainDriver.MAINTABLE.VALID)
			return;
		TRIGGER = true;
	}
		
}
