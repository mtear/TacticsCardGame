using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using Tactics_CoreGameEngine;

public class GameEndedMonitor : MonoBehaviour {

	//public Transform bgblack;
	//Animator animator;
	public Transform wintray;
	Animator animator2;

	public Transform replay;

	bool b = false;
	int t = 0;
	int tmax = 30;

	// Use this for initialization
	void Start () {
		//animator = bgblack.GetComponent<Animator> ();
		animator2 = wintray.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (UnityAnimationInterface.GAMEENDED) {
			if (!b) {
				b = true;
				//bgblack.gameObject.SetActive (true);
				//animator.Play ("fadeoutanim2");
				BlackTriggerMonitor.TRIGGER = true;
				animator2.Play ("wintray");
				t = 0;
				UnityCommandInterface.TRIGGER = true;

				Text text = wintray.Find("Text").GetComponent<Text> ();
				//DRAW
				string message = MainDriver.MAINTABLE.STRINGTABLE.Get("WIN");
				if (MainDriver.MAINTABLE.WINNER != MainDriver.MAINTABLE.P1)
					message = MainDriver.MAINTABLE.STRINGTABLE.Get ("LOSE");
				if (MainDriver.MAINTABLE.WINNER == null)
					message = "<color=orange>DRAW</color>";
				text.text = message;
			} else {
				t++;
				if (t == tmax) {
					b = false;
					UnityAnimationInterface.GAMEENDED = false;
					//animator.Play ("fadeoutanim3");
					//BlackTriggerMonitor.TRIGGER = true;
					animator2.Play ("wintray2");
					replay.gameObject.SetActive (true);
				}
			}
		}
	}

	public void EndGame(){
		ActiveTriggerMonitor.TRIGGERED = false;
		AttackMonitor.HALFFLAG = false;
		BlackTriggerMonitor.TRIGGER = false;
		CharacterMonitor.DYINGCOUNT = 0;
		GameBoardMonitor.prefabs = new List<GameObject> ();
		HandMonitorUI.INDEX = -1;
		InfoPanelStateMonitor.TRIGGER = false;
		MainDriver.DRAGGING = false;
		MainDriver.MOVEDRAGGING = false;
		MainDriver.playercommands = new List<Command> ();
		MainDriver.turnover = false;
		MasterMonitor.STATUS = 0;

		TurnSwitchMonitor.SWITCHING = false;
		TrapBoardMonitor.SUMMONING = false;
		TrapSummoningMonitor.SUMMONING = false;
		TurnSwitchMonitor.SWITCHING = false;
		UnityCommandInterface.WAITING = false;
		UnityCommandInterface.TARGETING = false;

		UnityAnimationInterface.TRIGGERANIMATING = false;
		UnityAnimationInterface.TRIGGEREFFECTANIMATING = false;
		UnityAnimationInterface.ATTACKING = false;
		UnityAnimationInterface.TURNSWITCHING = false;
		UnityAnimationInterface.ATTACKSTARTING = false;
		UnityAnimationInterface.TURNENDTRIGGER = false;
		UnityAnimationInterface.GAMEENDED = false;
		UnityAnimationInterface.ANIMATING = false;

		AnimationPipeline.ENEMYCOMMAND = false;
	}

	public void Replay(){
		EndGame ();
		Application.LoadLevel(Application.loadedLevel);
	}

}
