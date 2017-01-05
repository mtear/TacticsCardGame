using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Tactics_CoreGameEngine;

public class InfoPanelMonitor : MonoBehaviour {

	Transform conditiontext, frpanel, rrpanel, hapanel;

	Text ctext, frtext, rrtext, hatext, atk, life, charname, typestext;

	public static Character character = new UC11(null);
	Character backupchar;

	Sprite img;
	string satk, slife, scondition, sfr, srr, sha, scharname, stypes;

	// Use this for initialization
	void Start () {
		Transform content = transform.Find("Scroll View").Find ("Viewport").Find ("Content");
		conditiontext = content.Find ("ConditionText");
		frpanel = content.Find ("FRPanel");
		rrpanel = content.Find ("RRPanel");
		hapanel = content.Find ("HAPanel");
		ctext = conditiontext.GetComponent<Text> ();
		typestext = content.Find ("TypesText").GetComponent<Text> ();
		frtext = frpanel.Find ("innerpanel").Find ("Text").GetComponent<Text> ();
		rrtext = rrpanel.Find ("innerpanel").Find ("Text").GetComponent<Text> ();
		hatext = hapanel.Find ("innerpanel").Find ("Text").GetComponent<Text> ();

		atk = transform.Find ("atk").GetComponent<Text> ();
		life = transform.Find ("life").GetComponent<Text> ();
		charname = transform.Find ("charname").GetComponent<Text> ();

		ctext.text = "test";
		frtext.text = "test2";
		rrtext.text = "test3";
		hatext.text = "test4";

		frpanel.Find ("rowlabel").GetComponent<Text> ().text =
			MainDriver.MAINTABLE.STRINGTABLE.Get ("FR");
		rrpanel.Find ("rowlabel").GetComponent<Text> ().text =
			MainDriver.MAINTABLE.STRINGTABLE.Get ("RR");
		hapanel.Find ("rowlabel").GetComponent<Text> ().text =
			MainDriver.MAINTABLE.STRINGTABLE.Get ("HA");
	}

	bool Init(){
		if (character != backupchar) {
			backupchar = character;
			satk = character.CardAttack.ToString();
			slife = character.CardLife.ToString();
			img = Resources.Load<Sprite> (character.NameCode + "_C");
			scondition = ConditionText (character);
			sfr = RowText (character, 0);
			srr = RowText (character, 1);
			sha = RowText (character, 2);
			stypes = TypesText (character);
			scharname = MainDriver.MAINTABLE.STRINGTABLE.Get (character.NameCode);
			return true;
		}
		return false;
	}

	public static string ConditionText(Character character){
		string ret = "";
		if (character.Upgrade) {
			ret = MainDriver.MAINTABLE.STRINGTABLE.Get ("Upgrade");
			if (character.UpgradePlus > 0) {
				ret += "+" + character.UpgradePlus;
			}
		} else if (character.Gravedigger) {
			ret = MainDriver.MAINTABLE.STRINGTABLE.Get ("Gravedigger");
			if (character.GravediggerPlus > 0) {
				ret += "+" + character.GravediggerPlus;
			}
		}
		return ret;
	}

	public static string TypesText(Character character){
		int len = character.BaseTypes.Count;
		string ret = "";
		while (len > 0) {
			ret += MainDriver.MAINTABLE.STRINGTABLE.Get(character.BaseTypes [len-1]);
			if (len > 1)
				ret += " • ";
			len--;
		}
		return ret;
	}

	public static string RowText(Character character, int row){
		string ret = "";

		if (row < 2) {
			int i = 0;
			foreach (Passive a in character.CardPassives[row]) {
				ret += MainDriver.MAINTABLE.STRINGTABLE.Get (a.DescriptionCode);
				if (i++ < character.CardPassives [row].Count - 1) ret += "\n";
			}
			if (ret != "" && character.CardTriggers[row].Count > 0)
				ret += "\n";
			i = 0;
			foreach (Trigger a in character.CardTriggers[row]) {
				ret += MainDriver.MAINTABLE.STRINGTABLE.Get (a.DescriptionCode);
				if (i++ < character.CardTriggers [row].Count - 1) ret += "\n";
			}
		} else {
			if (character.HandAbility != null) {
				ret = MainDriver.MAINTABLE.STRINGTABLE
					.Get (character.CardHandAbility.DescriptionCode);
			}
		}

		if (ret == "") {
			ret = MainDriver.MAINTABLE.STRINGTABLE.Get ("FOOBAR");
		}
		return ret;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Init ()) {
			transform.Find ("cardimg").GetComponent<Image> ().sprite = img;
			atk.text = satk;
			life.text = slife;
			ctext.text = scondition;
			ctext.gameObject.SetActive (scondition != "");
			frtext.text = sfr;
			rrtext.text = srr;
			hatext.text = sha;
			charname.text = scharname;
			typestext.text = stypes;
		}
	}
}
