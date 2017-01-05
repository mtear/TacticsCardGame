using UnityEngine;
using System.Collections;

public class TrapSummoningMonitor : MonoBehaviour {
	int t = 0;
	int max = 60;

	public static bool SUMMONING = false;

	// Update is called once per frame
	void FixedUpdate () {
		if (MasterMonitor.STATUS == MasterMonitor.TRAPSUMMONING) {
			if (!SUMMONING) {
				SUMMONING = true;
				t = 0;
			}
			t++;
			if (t == max) {
				SUMMONING = false;
				TrapBoardMonitor.SUMMONING = false;
			}
		}
	}
}
