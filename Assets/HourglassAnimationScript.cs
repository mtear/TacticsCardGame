using UnityEngine;
using System.Collections;

public class HourglassAnimationScript : MonoBehaviour {

	Vector3 startpos, pos1, pos2, nexttarget;

	int t = 0;
	int tmax = 20;

	int pull = 0;

	bool spinning = false;

	Vector3 rot = new Vector3(0,0,18);

	// Use this for initialization
	void Start () {
		startpos = transform.position;
		pos1 = startpos + new Vector3 (0, -0.02f, 0);
		pos2 = startpos - new Vector3 (0, -0.02f, 0);
		nexttarget = pos1;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (spinning) {
			t++;
			transform.Rotate (rot);
			if (t == tmax) {
				spinning = false;
				t = 0;
			}
			return;
		}


		transform.position = Vector3.Lerp (transform.position, nexttarget, 5 * Time.deltaTime);
		t++;
		if (t == tmax) {
			t = 0;
			if (nexttarget == pos1)
				nexttarget = pos2;
			else
				nexttarget = pos1;
			pull++;

			if (pull == 8) {
				spinning = true;
				pull = 0;
				t = 0;
			}
		}
	}
}
