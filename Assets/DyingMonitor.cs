using UnityEngine;
using System.Collections;

public class DyingMonitor : MonoBehaviour {

	CharacterMonitor CM;

	int t = 0;
	int frame = 0;
	int MAX = 28;

	int c = 0;
	int cframe = 0;
	int CMAX = 30;

	bool playedsound1 = false, playedsound2 = false;

	// Use this for initialization
	void Start () {
		CM = this.GetComponent<CharacterMonitor> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (CM.DYING && MasterMonitor.STATUS == MasterMonitor.DYING) {
			c++;
			if (c % 3 == 0)
				cframe++;
			if (cframe == 2) {
				try{
					if(!playedsound1){
						playedsound1 = true;
						AudioClip vv = Resources.Load("Sound/DEATH1") as AudioClip;
						AudioSource.PlayClipAtPoint (vv, new Vector3 ());
					}
				}catch{
				}
			}
			if (c < CMAX+10) {
				Sprite sprite = Resources.Load <Sprite> ("battle/CLOUDS" + cframe);
				transform.Find("Clouds").GetComponent<SpriteRenderer> ().sprite = sprite;
			}

			if (cframe == 6) {
				Sprite sprite = Resources.Load <Sprite> ("battle/CORPSE1");
				transform.Find("Image").GetComponent<SpriteRenderer> ().sprite = sprite;
				sprite = Resources.Load <Sprite> ("battle/CORPSE_S");
				transform.Find("Shadow").GetComponent<SpriteRenderer> ().sprite = sprite;

			}

			if (c >= (CMAX/10)*6) {
				t++;
				if (t == MAX) {
					CM.DYING = false;
					CharacterMonitor.DYINGCOUNT--;
					t = 0;
					//Set sprite
					Sprite sprite = Resources.Load <Sprite> ("battle/CORPSE7");
					transform.Find("Image").GetComponent<SpriteRenderer> ().sprite = sprite;
				} else {
					if (t % 4 == 0) {
						frame++;
					}
					if (frame == 4) {
						try{
							if(!playedsound2){
								playedsound2 = true;
								AudioClip vv = Resources.Load("Sound/DEATH2") as AudioClip;
								AudioSource.PlayClipAtPoint (vv, new Vector3 ());
							}
						}catch{
						}
					}
					Sprite sprite = Resources.Load <Sprite> ("battle/CORPSE" + frame);
					transform.Find("Image").GetComponent<SpriteRenderer> ().sprite = sprite;
				}
			}
		} else {
			t = 0;
			c = 0;
			frame = 0;
			cframe = 0;
		}
	}
}
