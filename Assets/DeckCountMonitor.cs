using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DeckCountMonitor : MonoBehaviour {

	public Text text;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		int l1 = MainDriver.MAINTABLE.P1.DECK.Count;
		int l2 = MainDriver.MAINTABLE.P2.DECK.Count;

		text.text = "Deck: " + l1 + " " + l2;
	}
}
