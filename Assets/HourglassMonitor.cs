using UnityEngine;
using System.Collections;
using Tactics_CoreGameEngine;

public class HourglassMonitor : MonoBehaviour {

	public int index;
	public bool p1;

	public bool ready = false;

	Player P;
	TextMesh text;

	Color red, white;

	Transform hourglass, shadow, textgo;

	Animator animator;

	int counter = 0;
	int cmax = 20;

	// Use this for initialization
	void Start () {
		P = MainDriver.MAINTABLE.P1;
		if (!p1) P = MainDriver.MAINTABLE.P2;
		text = transform.Find ("Text").GetComponent<TextMesh> ();

		red = new Color (1, 0, 0);
		white = new Color (1, 1, 1);

		hourglass = transform.Find ("hourglass");
		textgo = transform.Find ("Text");
		shadow = transform.Find ("Shadow");

		animator = textgo.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (P.TRAPS [index] == null) {
			ready = false;
			this.gameObject.SetActive (false);
			hourglass.gameObject.SetActive (false);
			textgo.gameObject.SetActive (false);
			shadow.gameObject.SetActive (false);
			counter = 0;
		}

		if (!ready) {
			counter = 0;
			return;
		}

		if (counter < cmax) {
			counter++;
			return;
		}

		hourglass.gameObject.SetActive (true);
		shadow.gameObject.SetActive (true);
		textgo.gameObject.SetActive (true);

		int t = ((OnGoing)(P.TRAPS [index].HandAbility)).Timer;
		text.text = t.ToString ();
		if (t == 1) {
			text.color = red;
			animator.Play ("FlashHourglassText");
		} else {
			text.color = white;
			animator.Play ("FlashHourglassIdle");
		}

	}
}
