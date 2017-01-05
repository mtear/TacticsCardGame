using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Tactics_CoreGameEngine;

public class GameBoardMonitor : MonoBehaviour {

	public static bool SUMMONING = false;

	public static List<GameObject> prefabs = new List<GameObject> ();

	List<Character> p1chars = new List<Character>();
	List<Character> p2chars = new List<Character>();

	public GameObject p1board;
	public GameObject p2board;

	public Transform UnitsObj;
	
	// Update is called once per frame
	void FixedUpdate () {
		if (MasterMonitor.STATUS != MasterMonitor.SUMMONSTART) {
			return;
		}
		List<Character> chars1 = MainDriver.MAINTABLE.P1.GAMEBOARD.AllUnits;
		List<Character> chars2 = MainDriver.MAINTABLE.P2.GAMEBOARD.AllUnits;

		List<Character> list = chars1;
		List<Character> estlist = p1chars;
		GameObject board = p1board;
		Player P = MainDriver.MAINTABLE.P1;
		for (int a = 0; a < 2; a++) {
			for (int i = 0; i < list.Count; i++) {
				if (!estlist.Contains (list [i])) {
					//Recruit because we don't have this
					estlist.Add (list [i]);

					Point p = P.GAMEBOARD.LocateInBoard (list [i]);

					//recruit
					GameObject t = Instantiate (Resources.Load ("Character")) as GameObject;
					t.transform.SetParent (UnitsObj);
					t.transform.position = board.transform.Find ("" + p.x + p.y).position
						+ new Vector3(0, 0.8f) + new Vector3(0, 0, -1*p.x);
					Vector3 tls = t.transform.localScale;
					tls.x = tls.x + ((float)p.x * 0.025f);
					tls.y = tls.y + ((float)p.x * 0.025f);
					t.transform.localScale = tls;

					prefabs.Add (t);

					//Set sprite
					Sprite sprite = Resources.Load <Sprite> ("battle/" + list [i].NameCode + "_1");
					t.transform.Find("Image").GetComponent<SpriteRenderer> ().sprite = sprite;
					sprite = Resources.Load <Sprite> ("battle/" + list [i].NameCode + "_S");
					t.transform.Find("Shadow").GetComponent<SpriteRenderer> ().sprite = sprite;

					t.GetComponent<CharacterMonitor> ().character = list [i];

					GameBoardMonitor.SUMMONING = true;

					SummoningMonitor.summonchar = list [i];
					try{
						AudioClip vv = Resources.Load("Sound/"+list[i].NameCode+"_0") as AudioClip;
						AudioSource.PlayClipAtPoint (vv, new Vector3 ());
					}catch{
					}

					//Remove and decrement
					//list.RemoveAt (i--);
				}
			} // end for
			//Here if there are leftovers they are junk and need to be removed
			for (int i = 0; i < estlist.Count; i++) {
				if (!list.Contains (estlist [i]))
					RemovePrefab (estlist [i]);
			}
			list = chars2;
			estlist = p2chars;
			board = p2board;
			P = MainDriver.MAINTABLE.P2;
		}
	}

	private void RemovePrefab(Character character){
		//Destroy it and remove from the list if the char matches
		for (int i = 0; i < prefabs.Count; i++) {
			if (prefabs [i].GetComponent<CharacterMonitor> ().character == character) {
				Destroy (prefabs [i]);
				prefabs.RemoveAt (i--);
			}
		}
	}
}
