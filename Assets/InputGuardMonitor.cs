using UnityEngine;
using System.Collections;

public class InputGuardMonitor : MonoBehaviour {

	public Transform IGUARD;
	
	// Update is called once per frame
	void FixedUpdate () {
		IGUARD.gameObject.SetActive (UnityAnimationInterface.ANIMATING
			|| MainDriver.MAINTABLE.CURRENTTURN != MainDriver.MAINTABLE.P1);
	}
}
