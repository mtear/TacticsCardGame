using UnityEngine;
using System.Collections;

public class BoardAttackMonitor : MonoBehaviour {

	bool atking;

	// Use this for initialization
	void Start () {
		atking = UnityAnimationInterface.ATTACKING;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		/*if (MasterMonitor.STATUS != MasterMonitor.ATTACKSTART) {
			return;
		}*/
		if (atking != UnityAnimationInterface.ATTACKING) {
			atking = UnityAnimationInterface.ATTACKING;
			if (atking == false) {
				MainDriver.UAI.AttackContinue ();
			}
		}
	}
}
