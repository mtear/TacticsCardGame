using UnityEngine;
using System.Collections;
using Tactics_CoreGameEngine;
using UnityEngine.UI;

public class HandMonitorUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	int HANDSIZE = 0;
	public Transform prefab;
	//float SPACING = 1.3f;

	public static int INDEX = -1;

	int spacing = 3;

	// Update is called once per frame
	void FixedUpdate () {

		int hs = MainDriver.MAINTABLE.P1.HAND.Count;
		if (HANDSIZE < hs || transform.childCount < hs) { //draw cards
			HANDSIZE++;
			Transform t = Instantiate (prefab);
			t.SetParent (this.transform);
			t.position = this.transform.position;
			t.localScale = new Vector3 (1, 1, 1);

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
		//float STARTSPACE = -1 * (HANDSIZE * SPACING) / 2 - 1;

		float startpos = 0;
		float step = 0;

		foreach (Transform child in transform) {
			if (i >= hs) {
				Destroy (child.gameObject);
				continue;
			}

			if (i >= MainDriver.MAINTABLE.P1.HAND.Count)
				return;

			Character character = MainDriver.MAINTABLE.P1.HAND.Get (i);

			child.GetComponent<HandUIScript> ().index = i;

			RectTransform rt = child.GetComponent<RectTransform> ();

			Vector2 vt = new Vector2 (0, 0);
			vt.x += startpos;
			//rt.anchoredPosition = vt;
			rt.anchoredPosition = Vector2.Lerp(rt.anchoredPosition, vt, 10.0f * Time.deltaTime);

			step = rt.rect.width;
			if (i != INDEX) {
				startpos += step / 2;
			} else {
				startpos += step;
				startpos += spacing;
			}



			//Set sprite
			Sprite sprite = Resources.Load <Sprite> (character.NameCode+"_C");
			child.Find("Image").GetComponent<Image> ().sprite = sprite;

			//Set text
			child.Find("Life").GetComponent<Text>().text = character.RemainingLife.ToString();
			child.Find("Attack").GetComponent<Text>().text = character.Attack.ToString();

			i++;
		}
			
	}
}
