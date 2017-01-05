using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WhiteInScript : MonoBehaviour {

	public GameObject turnswitch;
	Animator animator;
	public Text gotext;

	void Start(){
		animator = turnswitch.GetComponent<Animator> ();
	}

	public void Toggle(){
		gameObject.SetActive (false);


		animator.Play ("turnswitchanimation");
		BlackTriggerMonitor.TRIGGER = true;
		//animator2.Play ("fadeoutanim1");
		string t = "<color=red>P1</color> TURN";
		if (MainDriver.MAINTABLE.CURRENTTURN == MainDriver.MAINTABLE.P2)
			t = "<color=blue>P2</color> TURN";
		gotext.text = t;

		try{
			AudioClip vv = Resources.Load("Sound/COCKSOUND") as AudioClip;
			AudioSource.PlayClipAtPoint (vv, new Vector3 ());
		}catch{}
	}
}
