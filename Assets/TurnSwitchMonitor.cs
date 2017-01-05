using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TurnSwitchMonitor : MonoBehaviour {

	bool b = false;
	int counter = 0;
	int turnmaxswitch = 65;

	public static bool SWITCHING = false;

	public GameObject gameobject;
	Text gotext;
	//public GameObject bgblack;
	Animator animator; //, animator2;

	// Use this for initialization
	void Start () {
		animator = gameobject.GetComponent<Animator> ();
		gotext = gameobject.transform.Find ("Text").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (UnityAnimationInterface.TURNENDTRIGGER) {

			if (b != UnityAnimationInterface.TURNENDTRIGGER) {
				if (MainDriver.MAINTABLE.STILLATTACKING || MasterMonitor.STATUS != MasterMonitor.IDLE)
					return;
				b = UnityAnimationInterface.TURNENDTRIGGER;
				UnityCommandInterface.TRIGGER = true;
			} else {
				if (MasterMonitor.STATUS == MasterMonitor.IDLE) {
					b = false;
					UnityAnimationInterface.TURNENDTRIGGER = false;
					MainDriver.MAINTABLE.SwitchTurn3 ();
				}
			}
		}

		if (MasterMonitor.STATUS == MasterMonitor.SWITCHTURN) {
			if (UnityAnimationInterface.TURNSWITCHING) {
				//Debug.Log ("Block 1");
				if (b != UnityAnimationInterface.TURNSWITCHING) {
					b = UnityAnimationInterface.TURNSWITCHING;
					SWITCHING = UnityAnimationInterface.TURNSWITCHING;
					counter = 0;
					//bgblack.SetActive (true);
					animator.Play ("turnswitchanimation");
					BlackTriggerMonitor.TRIGGER = true;
					//animator2.Play ("fadeoutanim1");
					string t = "<color=red>P1</color> TURN";
					if (MainDriver.MAINTABLE.CURRENTTURN == MainDriver.MAINTABLE.P2)
						t = "<color=blue>P2</color> TURN";
					gotext.text = t;
					MainDriver.playercommands.Clear ();
					MainDriver.turnover = false;
				} else {
//					Debug.Log ("Block 2");
					counter++;
					if (counter == turnmaxswitch) {
						UnityAnimationInterface.TURNSWITCHING = false;
						b = false;
						SWITCHING = false;
						//BlackTriggerMonitor.TRIGGER = true;
						//bgblack.SetActive (false);
					}
				}
			}
		}

		//attack start
		if (UnityAnimationInterface.ATTACKSTARTING) {
			if (b != UnityAnimationInterface.ATTACKSTARTING) {
				b = UnityAnimationInterface.ATTACKSTARTING;
				SWITCHING = UnityAnimationInterface.ATTACKSTARTING;
				counter = 0;
				//bgblack.SetActive (true);
				animator.Play ("turnswitchanimation");
				BlackTriggerMonitor.TRIGGER = true;
				//animator2.Play ("fadeoutanim1");
				string t = "<color=yellow>ATTACK!</color>";
				gotext.text = t;
			} else {
				counter++;
				if (counter == turnmaxswitch) {
					b = false;
					UnityAnimationInterface.ATTACKSTARTING = false;
					//BlackTriggerMonitor.TRIGGER = true;
					//bgblack.SetActive (false);
					MainDriver.MAINTABLE.FullAttackStart (MainDriver.MAINTABLE.CURRENTTURN);
				}
			}
		}
	}
}
