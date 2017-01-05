using UnityEngine;
using System.Collections;
using Tactics_CoreGameEngine;

public class TrapAnimationScript : MonoBehaviour {

	int counter = 0;
	float speed = 60f;
	int max = 540;

	int t = 0;
	int tmax = 8;
	bool tripped = false;

	int c = 0;
	int cmax = 30;

	public int p = 0;

	public bool P1 = true;

	Vector3 rot;

	Character cha;

	Vector3 ls;

	bool maxed = false;

	public void SetCha(Character c){
		cha = c;
		P1 = (MainDriver.MAINTABLE.P1 == c.CONTROLLER);
	}

	public Character GetCha(){
		return cha;
	}

	bool trapflipping = false;
	int zeta = 0;
	int zetamax = 0;
	float zetaspeed = 0;
	bool zetamaxed = false;
	Vector3 zetarot = new Vector3();

	// Use this for initialization
	void Start () {
		if (cha.CONTROLLER == MainDriver.MAINTABLE.P1) {
			transform.Find("Back").Find("GameObject").GetComponent<SpriteRenderer>().sprite = Resources.Load <Sprite> (MainDriver.P1CB);
		} else {
			transform.Find("Back").Find("GameObject").GetComponent<SpriteRenderer>().sprite = Resources.Load <Sprite> (MainDriver.P2CB);
		}

		if (cha.HandAbility.GetType ().IsSubclassOf (typeof(Trigger))) {
			speed = 0;
			max = 0;
			try {
				AudioClip vv = Resources.Load ("Sound/SETTRAP") as AudioClip;
				AudioSource.PlayClipAtPoint (vv, new Vector3 ());
			} catch {}
		} else if (cha.HandAbility.GetType ().IsSubclassOf (typeof(OnGoing))) {
			speed = 10f;
			max = 180 / (int)speed;
		} else {
			max /= (int)speed;
		}
		rot = new Vector3(0, speed, 0);
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		//Trap activate
		if (trapflipping) {
			if (!zetamaxed) {
				zeta++;
				transform.Rotate(zetarot);
				if (zeta == 4) {
					transform.GetComponent<Animator> ().Play ("LightUpTrap");
					try {
						AudioClip vv = Resources.Load ("Sound/ACTIVATE") as AudioClip;
						AudioSource.PlayClipAtPoint (vv, new Vector3 ());
					} catch {
					}
				}

				if (zeta == zetamax) {
					zetamaxed = true;
				}
			} else {
				zeta++;
				if (zeta == zetamax + 30) {
					Destroy (this.gameObject);
					ActiveTriggerMonitor.TRIGGERED = true;
					ActiveTriggerMonitor.C = cha;
					try {
						AudioClip vv = Resources.Load ("Sound/" + cha.NameCode + "_3") as AudioClip;
						AudioSource.PlayClipAtPoint (vv, new Vector3 ());
					} catch {
					}
				}
					
			}
			return;
		}

		if (!tripped) {
			t++;
			if (t == tmax) {
				tripped = true;
				//GetComponent<Animator> ().Stop ();
				ls = transform.localScale;
				ls.x = ls.x + 0.02f * p;
				ls.y = ls.y + 0.02f * p;
				transform.Find ("Shadow").gameObject.SetActive (true);

			}
			return;
		}
			
		transform.localScale = Vector3.Lerp (transform.localScale, ls, 12.0f * Time.deltaTime);

		if (counter >= max) {
			if (!maxed) {
				if (cha.HandAbility.GetType ().IsSubclassOf (typeof(Active))
				    || cha.HandAbility.GetType ().IsSubclassOf (typeof(OnGoing))) {
					c++;

					if (c == 4) {
						transform.GetComponent<Animator> ().Play ("LightUpTrap");
						try {
							AudioClip vv = Resources.Load ("Sound/ACTIVATE") as AudioClip;
							AudioSource.PlayClipAtPoint (vv, new Vector3 ());
						} catch {
						}
					}

					if (c == cmax) {
						maxed = true;

						ActiveTriggerMonitor.TRIGGERED = true;
						ActiveTriggerMonitor.C = cha;
						try {
							AudioClip vv = Resources.Load ("Sound/" + cha.NameCode + "_3") as AudioClip;
							AudioSource.PlayClipAtPoint (vv, new Vector3 ());
						} catch {}
					}
				} else { //trap?
					
				}
			}

			if (cha.HandAbility.GetType ().IsSubclassOf (typeof(Active))) {
				if(c == cmax)
					Destroy (this.gameObject);
			}
			return;
		}
		counter++;
		transform.Rotate(rot);

	}

	public void TrapActivate(){
		//Debug.Log ("ACTIVATE!");
		trapflipping = true;
		zeta = 0;
		zetaspeed = 20f;
		zetamax = 180 / (int)zetaspeed;
		zetarot = new Vector3(0, zetaspeed, 0);
	}
}
