using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Tactics_CoreGameEngine;

public class TrapBoardMonitor : MonoBehaviour {

	public static bool SUMMONING = false;

	// Use this for initialization
	void Start () {
		if (positions.Count > 0)
			return;
		positions.Add (new Vector3 (-4.932f, 1.78f, -2.94f));
		positions.Add (new Vector3 (-5.343f, 0.92f, -2.94f));
		positions.Add (new Vector3 (-5.8f, -0.07f, -2.94f));
	}

	public static List<GameObject> prefabs = new List<GameObject> ();

	List<Character> p1chars = new List<Character>();
	List<Character> p2chars = new List<Character>();

	public GameObject p1board;
	public GameObject p2board;

	public static List<Vector3> positions = new List<Vector3>();

	// Update is called once per frame
	void FixedUpdate () {
		if (MasterMonitor.STATUS != MasterMonitor.SUMMONSTART) {
			return;
		}

		List<Character> chars1 = new List<Character>(MainDriver.MAINTABLE.P1.TRAPS);
		List<Character> chars2 = new List<Character>(MainDriver.MAINTABLE.P2.TRAPS);

		List<Character> list = chars1;
		List<Character> estlist = p1chars;
		GameObject board = p1board;
		Player P = MainDriver.MAINTABLE.P1;
		bool secondtime = false;
		for (int a = 0; a < 2; a++) {
			for (int i = 0; i < list.Count; i++) {
				if (list [i] == null)
					continue;
				if (!estlist.Contains (list [i])) {
					//Recruit because we don't have this
					estlist.Add (list [i]);

					int p = P.GAMEBOARD.LocateTrap (list [i]);

					//recruit
					GameObject t = Instantiate (Resources.Load ("CardPrefabObj")) as GameObject;
					string fullpath = (secondtime) ? "/P2" : "/P1";
					t.transform.SetParent (GameObject.Find ("Traps" + fullpath).transform);
					Vector3 pos = positions [p];
					if (secondtime)
						pos.x *= -1;
					t.transform.position = pos;
					Vector3 ls = t.transform.localScale;
					ls.x = ls.x + 0.02f * p;
					ls.y = ls.y + 0.02f * p;
					if (secondtime)
						ls.x *= -1;
					t.transform.localScale = ls;
					prefabs.Add (t);

					t.GetComponent<TrapAnimationScript> ().SetCha(list [i]);

					bool trap = true;

					//Set sprite
					string spritename = "TRAP";
					if (list [i].HandAbility.GetType ().IsSubclassOf (typeof(OnGoing))) {
						spritename = list [i].NameCode + "_O";
						trap = false;
					}


					spritename = list[i].NameCode + "_C";

					Sprite sprite = Resources.Load <Sprite> (spritename);
					t.transform.Find("Front").Find("Image").transform.GetComponent<SpriteRenderer> ().sprite = sprite;

					t.GetComponent<TrapMonitor> ().character = list [i];
					t.GetComponent<TrapAnimationScript> ().p = p;

					SUMMONING = true;

					//t.GetComponent<Animator>()["TrapOngoingAnimation"].wrapMode = WrapMode.Once;
					t.GetComponent<Animator> ().Play ("TrapOngoingAnimation");

					if (!trap) {
						try {
							//AudioClip vv = Resources.Load ("Sound/" + list [i].NameCode + "_3") as AudioClip;
							//AudioSource.PlayClipAtPoint (vv, new Vector3 ());
						} catch {
						}
					}

					//MainDriver.STALLED = true;

					//Remove and decrement
					//list.RemoveAt (i--);
				}
			} // end for
			//Here if there are leftovers they are junk and need to be removed
			for (int i = 0; i < estlist.Count; i++) {
				if (estlist[i] != null && !list.Contains (estlist [i]))
					RemovePrefab (estlist [i]);
			}
			list = chars2;
			estlist = p2chars;
			board = p2board;
			P = MainDriver.MAINTABLE.P2;
			secondtime = true;
		}
	}

	private void RemovePrefab(Character character){
		//Destroy it and remove from the list if the char matches
		for (int i = 0; i < prefabs.Count; i++) {
			try{
				if (prefabs [i].GetComponent<TrapMonitor> ().character == character
					&& !character.HandAbility.GetType().IsSubclassOf(typeof(Trigger))) {
					Destroy (prefabs [i]);
					prefabs.RemoveAt (i--);
				}
			}catch{
			}
		}
	}
}
