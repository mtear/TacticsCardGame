using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Tactics_CoreGameEngine;
using UnityEngine.UI;

public class UICharAnimator : MonoBehaviour {

	List<Sprite> spritelist = new List<Sprite>();

	int currentframetime = 0, maxframetime = 10;
	int currentframe = 0;

	Image sr;

	public string namecode = "UN000001";
	string ncbackup;

	bool init = false;

	// Use this for initialization
	void Start () {
		sr = transform.GetComponent<Image> ();
		Init ();
	}


	void Init(){
		ncbackup = namecode;

		int i = 1;
		while (true) {
			Sprite sprite = Resources.Load <Sprite> ("battle/" + namecode + "_"+i++);
			if (sprite != null)
				spritelist.Add (sprite);
			else
				break;
		}

		currentframe = 0;
		init = true;
	}

	public void SetBlank(){
		sr.sprite = Resources.Load <Sprite> ("blank");
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (ncbackup != namecode) {
			init = false;
			spritelist.Clear ();
		}

		if (!init) {
			Init ();
			//return;
		}
			
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
