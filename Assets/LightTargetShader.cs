using UnityEngine;
using System.Collections;
using Tactics_CoreGameEngine;

public class LightTargetShader : MonoBehaviour {

	//Transform outline;

	// Use this for initialization
	void Start () {
		//outline = transform.Find ("outline");
	}

	// Update is called once per frame
	void FixedUpdate () {
		//outline.gameObject.SetActive (MainDriver.DRAGGING);
		SetAll ("P2", false);
		SetAll ("P1", false);
		if (MainDriver.DRAGGING) {
			Character c = MainDriver.MAINTABLE.P1.HAND.Get (HandUIScript.dragindex);
			if (HandUIScript.RECRUITPOINT != null) {
				Point p = HandUIScript.RECRUITPOINT;
				if(MainDriver.MAINTABLE.P1.GAMEBOARD.ValidRecruit(c, p.x, p.y)
					|| p.y == 2 && c.HandAbility != null){
					Set ("P1/" + p.x + p.y, true);
					Set ("P1/LR" + p.y, true);
				}
			}
		}else if (MainDriver.MOVEDRAGGING){/*
			Character c = MoveMonitor.movecharacter;
			for (int i = 0; i < TableTop.ROWS; i++) {
				for (int a = 0; a < TableTop.COLUMNS; a++) {
					Set ("P1/" + a + i, !MainDriver.MAINTABLE.P1.GAMEBOARD.ValidMove(c, a, i, false, false));
				}
			}*/
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
