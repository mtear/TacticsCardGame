using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerDeckMonitor : MonoBehaviour {

	public Text text;

	public bool p1 = true;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void FixedUpdate () {
		int l1 = MainDriver.MAINTABLE.P1.DECK.Count;
		int l2 = MainDriver.MAINTABLE.P2.DECK.Count;

		int u = (p1) ? l1 : l2;

		text.text = u.ToString ();
	}
}
