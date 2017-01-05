using UnityEngine;
using System.Collections;
using Tactics_CoreGameEngine;

public class DamageMonitor : MonoBehaviour {

	public Character character;

	Animator animator;

	// Use this for initialization
	void Start () {
		animator = transform.Find ("Image").GetComponent<Animator> ();
		Init ();
	}

	bool Init(){
		if (character == null) {
			try{
				character = transform.GetComponent<CharacterMonitor>().character;
				return true;
			}catch{
				return false;
			}
		}
		return true;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (!Init ())
			return;

		if (character.TAKENDAMAGE) {
			animator.Play ("TakeDamageFlash");

			GameObject t = Instantiate (Resources.Load ("DamageText")) as GameObject;
			t.GetComponent<TextMesh> ().text = "-" + character.TAKENDAMAGEVALUE.ToString ();
			t.transform.position = transform.position + new Vector3 (.55f, .05f, -3);

			character.TAKENDAMAGE = false;
			character.TAKENDAMAGEVALUE = 0;
		}

		if (character.TAKENHEALS) {
			//animator.Play ("TakeDamageFlash");

			GameObject t = Instantiate (Resources.Load ("HealText")) as GameObject;
			t.GetComponent<TextMesh> ().text = "+" + character.TAKENHEALSVALUE.ToString ();
			t.transform.position = transform.position + new Vector3 (.55f, .05f, -3);

			character.TAKENHEALS = false;
			character.TAKENHEALSVALUE = 0;
		}
	}
}
