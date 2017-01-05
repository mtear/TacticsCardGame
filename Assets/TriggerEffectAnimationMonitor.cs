using UnityEngine;
using System.Collections;
using Tactics_CoreGameEngine;

public class TriggerEffectAnimationMonitor : MonoBehaviour {

	int t = 0, tmax = 1;

	public Transform UnitsObj, TriggerDarkener;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (UnityAnimationInterface.TRIGGEREFFECTANIMATING) {
			if (t == 0) { //Do the animation, change to be negative edge triggered
				/*//Debug.Log ("OMG TRIGGER ANIMATING");
				if (UnityAnimationInterface.T.TP.TYPE == Trigger.TYPE.PLAYORDER) {
					MainDriver.UAI.ActiveAnimation (
						UnityCommandInterface.ACTIVEINDEX, UnityAnimationInterface.T.TP.TRUEUSER);
					tmax = 160;
				} else if (UnityAnimationInterface.T.TP.TYPE == Trigger.TYPE.TRAPACTIVATE) {
					Character USER = UnityAnimationInterface.TP.TRUEUSER;
					bool p1 = true;
					if (USER.CONTROLLER != MainDriver.MAINTABLE.P1) {
						p1 = false;
					}
					TrapAnimationScript TAS = null;
					//Look through all traps for the one
					Transform TRAPS = GameObject.Find("Traps").transform;
					foreach (Transform trans in TRAPS) {
						TrapAnimationScript tas = trans.GetComponent<TrapAnimationScript> ();
						if (tas != null) {
							if (tas.GetCha() == USER && tas.P1 == p1) {
								TAS = tas;
							}
						}
					}
					TAS.TrapActivate ();
					tmax = 160;
				}else {
					tmax = 1;
				}*/

				//Light up trigger symbol
				if (UnityAnimationInterface.T.TRIGGER != null) {
					Debug.Log ("Light up!");
					Character user = UnityAnimationInterface.T.TP.USER;
					foreach (Transform trans in UnitsObj) {
						CharacterMonitor cm = trans.GetComponent<CharacterMonitor> ();
						if (cm.character == user) {
							Debug.Log ("Found user!");
							tmax = 48;
							try {
								AudioClip vv = Resources.Load ("Sound/TRIGGERACTIVATE") as AudioClip;
								AudioSource.PlayClipAtPoint (vv, new Vector3 ());
							} catch {
							}
							cm.LightUpTrigger ();
							MainDriver.UAI.AnimationLock ();
							//darkener
							TriggerDarkener.gameObject.SetActive(true);
							Vector3 pos = trans.position;
							pos.x += 0.1f;
							pos.y -= 0.2f * (cm.x+1);
							TriggerDarkener.transform.position = pos;

						}
					}
				}
			}
			if (t++ == tmax) {
				t = 0;
				UnityAnimationInterface.TRIGGEREFFECTANIMATING = false;
				MainDriver.UAI.TriggerEffectAnimationContinue ();
				tmax = 1;
				MainDriver.UAI.AnimationUnlock ();
			}
		}
	}
}
