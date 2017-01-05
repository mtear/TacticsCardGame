using UnityEngine;
using System.Collections;

public class PipelinePopulator : MonoBehaviour {

	public Transform segmentprefab;

	bool switchlock = false;


	RectTransform rt;
	Vector2 newpos;

	public int pulls = 0;
	public Vector2 startpos;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < 6; i++) {
			Transform go = GameObject.Instantiate (segmentprefab);
			go.SetParent (transform);
			//go.parent = transform;
			go.transform.localPosition = new Vector3 (0,0,0);
			go.localScale = new Vector3 (1, 1, 1);
			int n = i;
			if (i == 3) n = 1;
			if (i == 4) n = 0;
			if (i == 5) n = 2;
			go.GetComponent<PipelineSegmentMonitor> ().playerid = n;
		}

		rt = GetComponent<RectTransform> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
//		Debug.Log (rt.rect.width);
		if (MasterMonitor.STATUS == MasterMonitor.SWITCHTURN || UnityAnimationInterface.ATTACKSTARTING) {
			if (!switchlock) {
				if (pulls < 12) {
					try{
						AudioClip vv = Resources.Load("Sound/COCKSOUND") as AudioClip;
						AudioSource.PlayClipAtPoint (vv, new Vector3 ());
					}catch{
					}
				}

				switchlock = true;
				newpos = rt.anchoredPosition;
				newpos -= new Vector2 (0, rt.rect.width / 6f);
				pulls++;
			}
		} else {
			switchlock = false;
		}

		if (switchlock) {
			Vector2 v = Vector2.Lerp (rt.anchoredPosition, newpos, 10f * Time.deltaTime);
			rt.anchoredPosition = v;
		}

		if (pulls >= 12) {
			rt.anchoredPosition = new Vector2(startpos.x, startpos.y);
			newpos = new Vector2 (startpos.x, startpos.y);
			pulls = 0;
		}
	}
}
