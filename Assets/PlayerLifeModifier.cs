using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerLifeModifier : MonoBehaviour {

	public Text text;

	public bool p1 = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		int l1 = MainDriver.MAINTABLE.P1.Life;
		int l2 = MainDriver.MAINTABLE.P2.Life;

		int u = (p1) ? l1 : l2;

		text.text = u.ToString ();
	}
}
