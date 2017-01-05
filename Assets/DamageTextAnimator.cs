using UnityEngine;
using System.Collections;

public class DamageTextAnimator : MonoBehaviour {

	// Use this for initialization
	void Start () {
		pos = transform.position;
	}

	Vector3 pos;
	
	// Update is called once per frame
	void FixedUpdate () {
		pos -= new Vector3 (0, 0.2f * Time.deltaTime, 0);
		transform.position = pos;
	}
}
