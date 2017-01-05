using UnityEngine;
using System.Collections;
using Tactics_CoreGameEngine;

public class CharacterMonitor : MonoBehaviour {

	public Character character;
	TextMesh atk, life, armor;

	public int x, y;

	public bool DYING = false;
	public static int DYINGCOUNT = 0;
	bool tripped = false;

	Transform sword, heart, shield, lightning, shieldb;

	Animator lightninglightupanim;

	Color r, g, w, yel;

	// Use this for initialization
	void Start () {
		r = new Color (1, 0.345f, 0.451f);
		g = new Color (0, 1, 0);
		w = new Color (1, 1, 1);
		yel = new Color (1, 1, 0);

		atk = transform.Find ("Attack").GetComponent<TextMesh>();
		life = transform.Find ("Life").GetComponent<TextMesh>();
		armor = transform.Find ("Armor").GetComponent<TextMesh>();
		lightning = transform.Find ("Lightning");
		shieldb = transform.Find ("InterceptShield");

		sword = transform.Find("icons").Find ("swordicon");
		heart = transform.Find("icons").Find ("hearticon");
		shield = transform.Find("icons").Find ("shieldicon");

		lightninglightupanim = transform.Find ("LightningLightup").GetComponent<Animator> ();

		x = character.CONTROLLER.GAMEBOARD.LocateInBoard (character).x;
		y = character.CONTROLLER.GAMEBOARD.LocateInBoard (character).y;
		if (character.CONTROLLER == character.CONTROLLER.TABLE.P2) {
			Vector3 ls = transform.Find ("Image").localScale;
			ls.x = ls.x * -1;
			transform.Find ("Image").localScale = ls;
			ls = transform.Find ("Shadow").localScale;
			ls.x = ls.x * -1;
			transform.Find ("Shadow").localScale = ls;
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		atk.text = character.Attack.ToString();
		life.text = character.RemainingLife.ToString();
		armor.text = character.Armor.ToString ();

		char atk_c = character.AttackColor;
		if (atk_c == 'g')
			atk.color = g;
		else
			atk.color = w;

		char life_c = character.LifeColor;
		if (life_c == 'g')
			life.color = g;
		else if (life_c == 'r')
			life.color = r;
		else
			life.color = w;

		if (character.Armor == 0 || character.Dead) {
			armor.gameObject.SetActive (false);
			shield.gameObject.SetActive (false);
		} else {
			armor.gameObject.SetActive (true);
			shield.gameObject.SetActive (true);
		}

		if(!character.Dead)
			shieldb.gameObject.SetActive (character.Intercept);

		if (character.Overkill) {
			sword.GetComponent<SpriteRenderer> ().color = yel;
		} else {
			sword.GetComponent<SpriteRenderer> ().color = w;
		}

		if (character.Dead && !tripped && MasterMonitor.STATUS == MasterMonitor.DYINGSTART) {
			DYINGCOUNT++;
			DYING = true;
//			Debug.Log("Start dying");
			tripped = true;

			atk.gameObject.SetActive (false);
			sword.gameObject.SetActive (false);
			life.gameObject.SetActive (false);
			heart.gameObject.SetActive (false);
			armor.gameObject.SetActive (false);
			shield.gameObject.SetActive (false);
			shieldb.gameObject.SetActive (false);
		}
			
		lightning.gameObject.SetActive (character.Triggers [y].Count > 0);
		if (character.Dead)
			lightning.gameObject.SetActive (false);
			
	}

	public void LightUpTrigger(){
		lightninglightupanim.Play ("TriggerLightupAnimation");
	}
}
