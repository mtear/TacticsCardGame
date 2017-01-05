using UnityEngine;
using System.Collections;

public class ReticleLineScript : MonoBehaviour {

	float scalex, w = 989f/100f;
	//Transform reticleline;
	public Transform P1BOARD, P2BOARD;

	// Use this for initialization
	void Start () {
		scalex = transform.localScale.x;
		//reticleline = transform.Find ("reticleline");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (UnityCommandInterface.TARGETING) {
			Vector3 pos = P1BOARD.Find(UnityCommandInterface.ACTIVEINDEX + "2").position;
			pos.z = -4.9999f;
			transform.position = pos;

			Vector3 mousePosition = Input.mousePosition;
			mousePosition = Camera.main.ScreenToWorldPoint (mousePosition);

			Vector2 v1 = new Vector2 (transform.position.x, transform.position.y);
			Vector2 v2 = new Vector2 (mousePosition.x, mousePosition.y);
			float angle = Mathf.Atan2 (v2.y - v1.y, v2.x - v1.x) * 180 / Mathf.PI;
			transform.rotation = Quaternion.AngleAxis (angle, Vector3.forward);

			float dist = Vector2.Distance (v1, v2);
			float ratio = (dist * scalex) / w;
			ratio *= .8f;
			Vector3 ls = transform.localScale;
			ls.x = ratio;
			transform.localScale = ls;
		} else {
			transform.position = new Vector3 (100, 100, 100);
			Vector3 ls = transform.localScale;
			ls.x = 0;
			transform.localScale = ls;
		}
	}

}
