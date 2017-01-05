using UnityEngine;
using System.Collections;
using Tactics_CoreGameEngine;

public class TriggerAnimationMonitor : MonoBehaviour {

	int t = 0, tmax = 120;

	public Transform UnitsObj;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (UnityAnimationInterface.TRIGGERANIMATING) {
			if (t == 0) { //Do the animation, change to be negative edge triggered
				//Debug.Log ("OMG TRIGGER ANIMATING");
				if (UnityAnimationInterface.TP.TYPE == Trigger.TYPE.PLAYORDER) {
//					Debug.Log ("ORDER TRIGGER!");
					MainDriver.UAI.ActiveAnimation (
						UnityCommandInterface.ACTIVEINDEX, UnityAnimationInterface.TP.TRUEUSER);
					tmax = 160;
				} else if (UnityAnimationInterface.TP.TYPE == Trigger.TYPE.TRAPACTIVATE) {
					//Debug.Log ("ACTIVATE TRAP");
					Character USER = UnityAnimationInterface.TP.TRUEUSER;
					bool p1 = true;
					if (USER.CONTROLLER != MainDriver.MAINTABLE.P1) {
						p1 = false;
					}
					TrapAnimationScript TAS = null;
					//Look through all traps for the one
					//Search through P1
					Transform TRAPS = GameObject.Find ("Traps/P1").transform;
					foreach (Transform trans in TRAPS) {
						TrapAnimationScript tas = trans.GetComponent<TrapAnimationScript> ();
						if (tas != null) {
							if (tas.GetCha () == USER && tas.P1 == p1) {
								TAS = tas;
							}
						}
					}
					//Search through P2
					TRAPS = GameObject.Find ("Traps/P2").transform;
					foreach (Transform trans in TRAPS) {
						TrapAnimationScript tas = trans.GetComponent<TrapAnimationScript> ();
						if (tas != null) {
							if (tas.GetCha () == USER && tas.P1 == p1) {
								TAS = tas;
							}
						}
					}

					if (TAS != null) {
						TAS.TrapActivate ();
						tmax = 160;
					} else {
						tmax = 1;
					}
				}else if(UnityAnimationInterface.TP.TYPE == Trigger.TYPE.PLAYONGOING){
					tmax = 160;
				}else {
					tmax = 1;
				}
			}
			if (t++ == tmax) {
				t = 0;
				UnityAnimationInterface.TRIGGERANIMATING = false;
				MainDriver.UAI.TriggerAnimationContinue ();
				tmax = 120;
			}
		} else
			tmax = 120;
	}
}
