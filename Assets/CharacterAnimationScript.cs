using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Tactics_CoreGameEngine;

public class CharacterAnimationScript : MonoBehaviour {

	List<Sprite> spritelist = new List<Sprite>();

	int currentframetime = 0, maxframetime = 10;
	int currentframe = 0;

	SpriteRenderer sr, shadow;

	public Character character;

	bool init = false;

	bool droppedin = false, droppedin2 = false;
	int d = 0, dmax = 15;
	Vector3 v = new Vector3();
	float a = 0, dist = 0;

	// Use this for initialization
	void Start () {
		Init ();
		transform.Find ("Image").transform.position += new Vector3 (0, 6.0f, 0);
		a = (2.0f * 6.0f) / (15.0f * 15.0f);
	}


	void Init(){
		try{
			character = GetComponent<CharacterMonitor> ().character;
		}catch{
		}

		if (character == null) {
			return;
		}

		sr = transform.Find("Image").GetComponent<SpriteRenderer> ();
		shadow = transform.Find ("Shadow").GetComponent<SpriteRenderer> ();

		int i = 1;
		while (true) {
			Sprite sprite = Resources.Load <Sprite> ("battle/" + character.NameCode + "_"+i++);
			if (sprite != null)
				spritelist.Add (sprite);
			else
				break;
		}

		Sprite sprite2 = Resources.Load <Sprite> ("battle/" + character.NameCode + "_S");
		shadow.sprite = sprite2;

		init = true;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (!init) {
			Init ();
			return;
		}

		if (!droppedin) {
			if (d++ == dmax) {
				droppedin = true;
				d = 0;
				//dmax /= 4;
			} else {
				v.y = v.y + a;
				dist += v.y;
				if (dist > 6)
					v.y = dist - 6;
				transform.Find ("Image").transform.position -= v;
			}
			return;
		} else {
			if (!droppedin2) {
				if (d++ == dmax) {
					droppedin2 = true;
				} else {
					Vector3 v = new Vector3(0,0,0);
					if (d < dmax / 2)
						v = new Vector3 (0, 0.045f, 0);
					else if (d > dmax / 2 && d < dmax)
						v = new Vector3 (0, -0.045f, 0);
					transform.Find ("Image").transform.position += v;
				}
			}
		}

		if (character.Dead)
			return;
		
		currentframetime++;
		if (currentframetime == maxframetime) {
			currentframetime = 0;
			currentframe++;
			if (currentframe == spritelist.Count) {
				currentframe = 0;
			}
		}

		sr.sprite = spritelist [currentframe];
	}
}
