using UnityEngine;
using System.Collections;

public class TriggerDarkenerScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Toggle(){
		transform.gameObject.SetActive (false);
	}
}
