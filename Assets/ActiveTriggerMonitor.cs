using UnityEngine;
using System.Collections;
using Tactics_CoreGameEngine;

public class ActiveTriggerMonitor : MonoBehaviour {

	public static bool TRIGGERED = false;
	public static Character C;

	public GameObject activepanel;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (TRIGGERED) {
			//GameObject activepanel = transform.root.Find("Canvas").Find ("ActivatePanel").gameObject;
			activepanel.GetComponent<ActivatePanelMonitor> ().character = C;
			activepanel.SetActive (true);

			TRIGGERED = false;
		}
	}
}
