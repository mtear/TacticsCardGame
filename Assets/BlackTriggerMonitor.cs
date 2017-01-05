using UnityEngine;
using System.Collections;

public class BlackTriggerMonitor : MonoBehaviour {

	public static bool TRIGGER = false;

	public GameObject black;
	Animator blackanimator;

	void Start(){
		blackanimator = black.GetComponent<Animator> ();
	}

	void FixedUpdate () {
		if (TRIGGER) {
			TRIGGER = false;

			black.gameObject.SetActive (true);
			blackanimator.SetTrigger ("Fade");
		}
	}
}
