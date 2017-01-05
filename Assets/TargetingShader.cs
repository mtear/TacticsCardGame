using UnityEngine;
using System.Collections;
using Tactics_CoreGameEngine;

public class TargetingShader : MonoBehaviour {

	//Transform outline;

	// Use this for initialization
	void Start () {
		//outline = transform.Find ("outline");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//outline.gameObject.SetActive (MainDriver.DRAGGING);
		SetAll ("P2", MainDriver.DRAGGING);
		if (MainDriver.DRAGGING) {
			Character c = MainDriver.MAINTABLE.P1.HAND.Get (HandUIScript.dragindex);
			for (int i = 0; i < TableTop.ROWS; i++) {
				for (int a = 0; a < TableTop.COLUMNS; a++) {
					Set ("P1/" + a + i, !MainDriver.MAINTABLE.P1.GAMEBOARD.ValidRecruit (c, a, i));
				}
			}
			if (c.HandAbility == null) {
				Set ("P1/02", true);
				Set ("P1/12", true);
				Set ("P1/22", true);
			}
		}else if (MainDriver.MOVEDRAGGING){
			Character c = MoveMonitor.movecharacter;
			for (int i = 0; i < TableTop.ROWS; i++) {
				for (int a = 0; a < TableTop.COLUMNS; a++) {
					Set ("P1/" + a + i, !MainDriver.MAINTABLE.P1.GAMEBOARD.ValidMove(c, a, i, false, false));
				}
			}
		} else {
			SetAll ("P1", false);
		}
	}

	void SetMainBoard(string name, bool value){
		Transform t = transform.Find (name);
		t.Find ("00").gameObject.SetActive (value);
		t.Find ("10").gameObject.SetActive (value);
		t.Find ("20").gameObject.SetActive (value);
		t.Find ("01").gameObject.SetActive (value);
		t.Find ("11").gameObject.SetActive (value);
		t.Find ("21").gameObject.SetActive (value);
	}

	void SetAll(string name, bool value){
		Transform trans = transform.Find (name);
		foreach (Transform t in trans) {
			t.gameObject.SetActive (value);
		}
	}

	void Set(string name, bool value){
		transform.Find (name).gameObject.SetActive (value);
	}
}
