using UnityEngine;
using System.Collections;
using Tactics_CoreGameEngine;

public class HandMonitor : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	int HANDSIZE = 0;
	public Transform prefab;
	float SPACING = 1.3f;
	
	// Update is called once per frame
	void FixedUpdate () {

		int hs = MainDriver.MAINTABLE.P1.HAND.Count;
		if (HANDSIZE < hs || transform.childCount < hs) { //draw cards
			HANDSIZE++;
			Transform t = Instantiate (prefab);
			t.SetParent (this.transform);
			t.position = this.transform.position;

			AdjustCards ();
		} else if(HANDSIZE > hs){
			HANDSIZE = hs;
			//AdjustCards ();
		}

		if(!MainDriver.DRAGGING)
			AdjustCards ();
	}

	void AdjustCards(){
		if (UnityCommandInterface.TARGETING)
			return;
		int i = 0;
		int hs = MainDriver.MAINTABLE.P1.HAND.Count;
		float STARTSPACE = -1 * (HANDSIZE * SPACING) / 2 - 1;
		foreach (Transform child in transform) {
			if (i >= hs) {
				Destroy (child.gameObject);
				continue;
			}

			if (i >= MainDriver.MAINTABLE.P1.HAND.Count)
				return;
			
			Character character = MainDriver.MAINTABLE.P1.HAND.Get (i);

			child.GetComponent<TestMouseFunctions> ().index = i;
			child.transform.position = new Vector3 (
				STARTSPACE + i*SPACING,
				this.transform.position.y,
				child.transform.position.z);

			//Set sprite
			Sprite sprite = Resources.Load <Sprite> (character.NameCode+"_C");
			child.Find("Image").GetComponent<SpriteRenderer> ().sprite = sprite;

			//Set text
			child.Find("Life").GetComponent<TextMesh>().text = character.RemainingLife.ToString();
			child.Find("Attack").GetComponent<TextMesh>().text = character.Attack.ToString();

			i++;
		}
	}
}
