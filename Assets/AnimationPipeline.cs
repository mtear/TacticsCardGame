using UnityEngine;
using System.Collections;

public class AnimationPipeline : MonoBehaviour {

	public static bool ENEMYCOMMAND = false;

	// Update is called once per frame
	void FixedUpdate () {
		if (!UnityAnimationInterface.ANIMATING) {

			if (ENEMYCOMMAND) {
				ENEMYCOMMAND = false;
				MainDriver.MAINTABLE.P2.GetTurnCommand ();
				return;
			}

		}
	}
}
