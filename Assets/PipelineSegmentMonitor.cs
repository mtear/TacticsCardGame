using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PipelineSegmentMonitor : MonoBehaviour {

	public int playerid = 0;
	Image img;

	// Use this for initialization
	void Start () {
		img = transform.Find ("portrait").GetComponent<Image>();
		if (playerid == 0) {
			Sprite sprite = Resources.Load <Sprite> ("portrait1_s");
			img.sprite = sprite;
			img.color = new Color (1, 0.89f, 0.89f);
		} else if (playerid == 1) {
			Sprite sprite = Resources.Load <Sprite> ("portrait2_s");
			img.sprite = sprite;
			img.color = new Color (0.89f, 0.89f, 1);
			Vector3 ls = img.transform.localScale;
			ls.x *= -1;
			img.transform.localScale = ls;
		} else {
			Sprite sprite = Resources.Load <Sprite> ("attacksegment");
			img.sprite = sprite;
			img.color = new Color (1, 1, 1);
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
	}
}
