using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Tactics_CoreGameEngine;

public class ActivatePanelMonitor : MonoBehaviour {

	public Character character = new UC10(null);

	Text charname, chardesc;

	UICharAnimator uica;

	//public GameObject bgblack;
	//Animator animator;

	int t = 0;
	int tmax = 100;

	bool trig = true;

	// Use this for initialization
	void Start () {
		charname = transform.Find ("nametext").GetComponent<Text>();
		chardesc = transform.Find ("desctext").GetComponent<Text>();
		uica = transform.Find ("Image").GetComponent<UICharAnimator> ();

		//animator = bgblack.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (trig) {
			//bgblack.SetActive (true);
			//animator.Play ("fadeoutanim4");
			BlackTriggerMonitor.TRIGGER = true;

			trig = false;
		}
			
		if (t++ == tmax) {
			//bgblack.SetActive (false);
			BlackTriggerMonitor.TRIGGER = true;
			t = 0;
			transform.gameObject.SetActive (false);
			chardesc.text = " ";
			charname.text = " ";
			uica.SetBlank ();
			trig = true;
			return;
		}

		uica.namecode = character.NameCode;
		charname.text = MainDriver.MAINTABLE.STRINGTABLE.Get (character.NameCode);
		Ability a = character.HandAbility;
		if (a == null) {
			chardesc.text = "";
			return;
		}
		if (a.GetType ().IsSubclassOf (typeof(Active))) {
			chardesc.text = MainDriver.MAINTABLE.STRINGTABLE.Get (a.DescriptionCode);
		} else if (a.GetType ().IsSubclassOf (typeof(Trigger))) {
			chardesc.text = MainDriver.MAINTABLE.STRINGTABLE.Get (a.DescriptionCode);
		} else {
			chardesc.text = MainDriver.MAINTABLE.STRINGTABLE.Get ("ONGOING EFFECT");
		}
	}
}
