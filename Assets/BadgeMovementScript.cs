using UnityEngine;
using System.Collections;

public class BadgeMovementScript : MonoBehaviour {

	float speed = 0.3f;
	float rotspeed = 60.0f;

	float xpos;

	Vector3 move = new Vector3(-1f, -1f, 0);

	// Use this for initialization
	void Start () {
		xpos = transform.position.x;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (transform.position.y < -6) {
			transform.position = new Vector3 (xpos, 6, transform.position.z);
		}

		transform.RotateAround(transform.position, transform.forward, rotspeed * Time.deltaTime);
		transform.position += move * speed * Time.deltaTime;
	}
}
