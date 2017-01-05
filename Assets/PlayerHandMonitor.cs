using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHandMonitor : MonoBehaviour {

	public Text text;

	public bool p1 = true;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void FixedUpdate () {
		int l1 = MainDriver.MAINTABLE.P1.HAND.Count;
		int l2 = MainDriver.MAINTABLE.P2.HAND.Count;

		int m1 = MainDriver.MAINTABLE.P1.MaxHandSize;
		int m2 = MainDriver.MAINTABLE.P2.MaxHandSize;

		int u = (p1) ? l1 : l2;
		int m = (p1) ? m1 : m2;

		string t = u.ToString();
		if (m != 5)
			t += "/<color=yellow>" + m + "</color>";

		text.text = t;
	}
}
