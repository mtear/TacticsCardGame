using UnityEngine;
using System.Collections;
using Tactics_CoreGameEngine;

public class HealthBarMonitor : MonoBehaviour {

	public bool P1 = true;

	Player P;

	float sls, odx, ody;

	RectTransform rt;

	// Use this for initialization
	void Start () {
		rt = GetComponent<RectTransform>();
		sls = rt.sizeDelta.x;
		odx = sls;
		ody = rt.sizeDelta.y;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (P == null) {
			P = MainDriver.MAINTABLE.P1;
			if(!P1) P = MainDriver.MAINTABLE.P2;
			return;
		}

		if (P.Life >= TableTop.MAXLIFE) {
			//return;
		}

		float d = TableTop.MAXLIFE;
		d = ((float)P.Life) / d;

		rt.sizeDelta = new Vector2(d * odx, ody);

	}
}
