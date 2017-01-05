using UnityEngine;
using System.Collections;
using Tactics_CoreGameEngine;

public class OngoingHourglassMonitor : MonoBehaviour {

	public Character character;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Player P = MainDriver.MAINTABLE.P1;
		int r = 0;
		for (int p = 0; p < 2; p++) {
			for (int i = 0; i < 3; i++) {
				if (P.TRAPS [i] != null) {
					if (P.TRAPS [i].HandAbility.GetType ().IsSubclassOf (typeof(OnGoing))) {
						Transform t = transform.Find ("" + r + i);
						t.GetComponent<HourglassMonitor> ().ready = true;
						t.gameObject.SetActive (true);
					}
				}
			}
			P = P.ENEMY;
			r = 1;
		}
	}

}
