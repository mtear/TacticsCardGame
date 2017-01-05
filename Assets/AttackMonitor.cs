using UnityEngine;
using System.Collections;
using Tactics_CoreGameEngine;

public class AttackMonitor : MonoBehaviour {

	bool attackanimating = false;
	Vector3 startpos, endpos;
	float t = 0;
	float timeToReachTarget = 0.2f;
	int phase = 0;
	public static bool HALFFLAG = false;
	bool TRIGGEREDHALFFLAG = false;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//if (MasterMonitor.STATUS != MasterMonitor.ATTACKING)
		//	return;

		//Check for it this unit is attacking
		if (UnityAnimationInterface.ATTACKING && !attackanimating) {

			Character thischar = GetComponent<CharacterMonitor> ().character;
			Character target = UnityAnimationInterface.target;

			//If this is the attacker
			if (UnityAnimationInterface.attacker == thischar) {
				attackanimating = true;
				t = 0;
				startpos = transform.position;
				foreach (GameObject go in GameBoardMonitor.prefabs) {
					if (go.GetComponent<CharacterMonitor> ().character == target) {
						endpos = go.transform.position;
					}
				}
				if (target == null) {
					if (thischar.CONTROLLER.ENEMY == MainDriver.MAINTABLE.P2)
						endpos = new Vector3 (3, startpos.y, startpos.z);
					else
						endpos = new Vector3 (-3, startpos.y, startpos.z);
				}
				
			}
			//if this is the target
			if (target == thischar) {
				//Animate if the target for this would be the attacker
				Point p = thischar.CONTROLLER.GAMEBOARD.LocateInBoard(thischar);
				if (thischar.CONTROLLER.GAMEBOARD.ValidAttack (p.x, p.y)) {
					//Here, thischar is valid to attack
					Character targ = thischar.CONTROLLER.ENEMY.GAMEBOARD.FindCharacterInMelee(p.x);
					if (targ == UnityAnimationInterface.attacker) {
						//the character and target are attacking one another
						attackanimating = true;
						t = 0;
						startpos = transform.position;
						//Find the end position
						foreach (GameObject go in GameBoardMonitor.prefabs) {
							if (go.GetComponent<CharacterMonitor> ().character == targ) {
								endpos = go.transform.position;
							}
						}
						//Remove self from units list?
						//Check about damage
						UnityAnimationInterface.Units.Remove(thischar);
						thischar.CONTROLLER.GAMEBOARD.Melee (p.x, p.y);
						AttackMonitor.HALFFLAG = true;
					}
				}
			}
		}

		//Half way flag
		if (HALFFLAG && !TRIGGEREDHALFFLAG) {
			TRIGGEREDHALFFLAG = true;
			if(endpos.x != startpos.x)
				endpos.x = startpos.x - (startpos.x - endpos.x) / 2.5f;
			endpos.y = startpos.y - (startpos.y - endpos.y) / 2.5f;
			//endpos.z = endpos.z / 2;

		}

		//If this character is doing an attack animation
		if (attackanimating) {
			t += Time.deltaTime / timeToReachTarget;
			transform.position = Vector3.Lerp (startpos, endpos, t);
			if (phase == 0) {
				if (transform.position == endpos) {
					phase = 1;
					Vector3 sp = startpos;
					startpos = endpos;
					endpos = sp;
					endpos.x -= 0.01f;
					t = 0;

					try{
						int smacksound = Utility.R.Next(2) + 1;
						AudioClip vv = Resources.Load("Sound/SMACK" + smacksound) as AudioClip;
						AudioSource.PlayClipAtPoint (vv, new Vector3 ());
					}catch{}
				}
			} else {
				
				if (transform.position == endpos) {
					attackanimating = false;
					UnityAnimationInterface.ATTACKING = false;
					phase = 0;
					HALFFLAG = false; TRIGGEREDHALFFLAG = false;
				}
			}
		}
	}
}
