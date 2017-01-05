using UnityEngine;
using System.Collections;
using Tactics_CoreGameEngine;

public class SummoningMonitor : MonoBehaviour {
	int t = 0;
	int max = 60;

	public static bool SUMMONING = false;
	public static Character summonchar;
	
	// Update is called once per frame
	void FixedUpdate () {
		if (MasterMonitor.STATUS == MasterMonitor.SUMMONING) {
			if (!SUMMONING) {
				SUMMONING = true;
				t = 0;
			}
			t++;
			if (t == max) {
				SUMMONING = false;
				GameBoardMonitor.SUMMONING = false;


			}
		}
	}
}
