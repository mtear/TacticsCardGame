using UnityEngine;
using System.Collections;

public class StarScript : MonoBehaviour {

	public float speed = 1;

	RectTransform rt;

	Vector2 target;

	// Use this for initialization
	void Start () {
		rt = GetComponent<RectTransform> ();
		target = rt.anchoredPosition;
		target.x = -402;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//Vector2 v = Vector2.Lerp (rt.anchoredPosition, target, speed * Time.deltaTime);
		Vector2 v = new Vector2(rt.anchoredPosition.x - speed * Time.deltaTime, rt.anchoredPosition.y);

		if (v.x <= -401) {
			v.x = 401;
		}

		rt.anchoredPosition = v;
	}
}
