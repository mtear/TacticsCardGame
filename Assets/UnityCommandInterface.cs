using UnityEngine;
using System.Collections;
using Tactics_CoreGameEngine;
using System.Threading;
using System.Collections.Generic;
using System;

/// <summary>
/// An EnemyAI interface that only passes turn
/// </summary>
public class UnityCommandInterface : CommandInterface{
	public UnityCommandInterface(){}

	public static bool WAITING = false;
	public static bool TARGETING = false;
	public static Command COMMAND;
	public static Active ACTIVE;
	public static Character ACTIVETARGETINGCARD;

	public static bool TRIGGER = false;

	public static int ACTIVEINDEX = 0;

	public override void GetTurnCommand(){
		WAITING = true;
	}

	public override void GetTarget (Character c)
	{
		//Debug.Log ("Hi");
		ACTIVETARGETINGCARD = c;
		ACTIVE = (Active)c.HandAbility;
		TARGETING = true;
	}

	public bool Flush(Player P, Command c){
		//TRIGGER = true;
		//Debug.Log ("COMMAND!");
		return MainDriver.MAINTABLE.Flush (P, c);
	}
		

} // End EnemyAI_Pass class


/// <summary>
/// An EnemyAI interface that only passes turn
/// </summary>
public class EnemyAI : CommandInterface{
	public EnemyAI(){}

	public override void GetTurnCommand(){

		//Wait for no animations
		if (UnityAnimationInterface.ANIMATING) {
			AnimationPipeline.ENEMYCOMMAND = true;
			return;
		}

//		Debug.Log ("Getting Enemy turn!");
		if(P.HAND.Count == 0){
			Command c = new Command (Command.TYPE.PASS, null, P);
			P.TABLE.Flush (P, c);
		}
		else {
			Debug.Log ("Creating Enemy Command!");
			Command c = Command.Parse ("pass", P);
			int rval = 10;
			if (P.GAMEBOARD.Units.Count == 0 || ValidHandActions().Count == 0)
				rval = 1;
			if (Utility.R.Next (rval) < 1) {
				List<Command> actions = ValidRecruits ();
				if (actions.Count > 0) {
					int i = Utility.R.Next (actions.Count);
					c = actions [i];
					UnityCommandInterface.ACTIVEINDEX = int.Parse (actions [i].Parameters [1]);
				} //pass
			} else {
				List<Command> actions = ValidHandActions ();
				if (actions.Count > 0) {
					int i = Utility.R.Next (actions.Count);
					c = actions [i];
					UnityCommandInterface.ACTIVEINDEX = int.Parse (actions [i].Parameters [1]);
				} else {
					c = Command.Parse ("null", P);
				}
			}

			P.TABLE.Flush (P, c);
		}

	}

	List<Command> ValidHandActions(){
		List<Command> ret = new List<Command> ();
		for (int i = 0; i < P.HAND.Count; i++) {
			for (int x = 0; x < 3; x++) {
				if (P.GAMEBOARD.ValidHandAbility (P.HAND.Get (i), x))
					ret.Add (Command.Parse ("hand " + i + " " + x, P));
			}
		}
		return ret;
	}

	List<Command> ValidRecruits(){
		List<Command> ret = new List<Command> ();
		for (int j = 0; j < P.HAND.Count; j++) {
			for (int i = 0; i < 2; i++) {
				for (int a = 0; a < 3; a++) {
					if (P.GAMEBOARD.ValidRecruit (P.HAND.Get (j), a, i)) {
						ret.Add (Command.Parse ("recruit " + a + " " + i + " " + j, P));
					}
				}
			}
		}
		return ret;
	}

	public override void GetTarget (Character c)
	{
		Target T = new Target (c.CONTROLLER, null);
		try{
			Active a = (Active)c.HandAbility;
			a.TARGET = T;
		}catch{
		}
		c.CONTROLLER.ActiveContinue (false, c, T);
	}

} // End EnemyAI_Pass class


public class UnityAnimationInterface : AnimationInterface{

	public static bool ATTACKING = false;
	public static Character attacker;
	public static Character target;
	public static List<Character> Units;
	public static bool TURNSWITCHING = false;
	public static bool ATTACKSTARTING = false;
	public static bool TURNENDTRIGGER = false;
	public static bool GAMEENDED = false;
	public static bool TRIGGERANIMATING = false, TRIGGEREFFECTANIMATING = false;

	public static bool ANIMATING = false;

	public static TriggerPair T;
	public static TriggerPacket TP;

	public override void AttackBreak(Character a, Character t, List<Character> Units){
		attacker = a;
		target = t;
		ATTACKING = true;
		Debug.Log ("Attack breaking...");
		UnityAnimationInterface.Units = Units;

		AnimationLock ();
	}

	public override void SwitchTurnBreak(){
//		Debug.Log ("Switch turn breaking!");
		TURNSWITCHING = true;
		UnityCommandInterface.TRIGGER = true;

		AnimationLock ();
	}

	public void AttackContinue(){
		Debug.Log ("Attack continue!");
		TABLE.AttackContinue (UnityAnimationInterface.Units);

		AnimationUnlock ();
	}

	public override void SettleBreak(){
		//Debug.Log ("Settling...");
		UnityCommandInterface.TRIGGER = true;

		AnimationLock ();
	}

	public void SettleContinue(){
		TABLE.SettleState (TABLE.CURRENTTURN);

		AnimationUnlock ();
	}

	public override void TemporaryPlayActiveSound(String namecode){
		try {
			//Debug.Log(namecode);
			//AudioClip vv = Resources.Load ("Sound/" + namecode + "_3") as AudioClip;
			//AudioSource.PlayClipAtPoint (vv, new Vector3 ());
		} catch {
		}
	}

	public override void AttackStartBreak(){
		ATTACKSTARTING = true;

		AnimationLock ();
	}

	public override void PrepareNextTurn(){
		TURNENDTRIGGER = true;

		AnimationLock ();
	}

	public override void EndGameBreak(){
		GAMEENDED = true;

		AnimationLock ();
	}

	public override void ActiveAnimation(int x, Character character){
//		Debug.Log ("ANIMATING ACTIVE");
		AnimationLock ();
		int p = x;
		//Debug.Log (p);
		GameObject t = GameObject.Instantiate (Resources.Load ("CardPrefabObj")) as GameObject;
		string fullpath = (character.CONTROLLER == MainDriver.MAINTABLE.P1) ? "/P1" : "/P2";
		Transform root = GameObject.Find ("Traps" + fullpath).transform;
		t.transform.SetParent (root);
		Vector3 pos = TrapBoardMonitor.positions [p];
		if (fullpath == "/P2")
			pos.x *= -1;
		t.transform.position = pos;
		Vector3 ls = t.transform.localScale;
		ls.x = ls.x + 0.02f * p;
		ls.y = ls.y + 0.02f * p;
		t.transform.localScale = ls;
		String spritename = character.NameCode + "_C";
		Sprite sprite = Resources.Load <Sprite> (spritename);
		t.transform.Find ("Front").Find ("Image").transform.GetComponent<SpriteRenderer> ().sprite = sprite;
		t.GetComponent<TrapAnimationScript> ().SetCha(character);
		t.GetComponent<Animator> ().Play ("TrapOngoingAnimation");
		t.GetComponent<TrapAnimationScript> ().p = p;
	}

	public override void TriggerEffectAnimation(TriggerPair T){
		UnityAnimationInterface.T = T;
		TRIGGEREFFECTANIMATING = true;
		AnimationLock ();
//		Debug.Log ("STARTING TRIGGER EFFECT ANIMATION");
	}

	public void TriggerEffectAnimationContinue(){
//		Debug.Log ("ENDING TRIGGER ANIMATION");
		TABLE.PopEffectContinue (T);
		AnimationUnlock ();
	}

	public void TriggerAnimationContinue(){
		TABLE.SettleState1p5 ();

		AnimationUnlock ();
	}

	public override void TriggerAnimation(TriggerPacket TP){
		//Save the trigger packet
		//Kick off the trigger animating
		UnityAnimationInterface.TP = TP;
		TRIGGERANIMATING = true;
		AnimationLock ();
//		Debug.Log ("STARTING TRIGGER ANIMATION");
	}

	public override void AnimationLock(){
		//Debug.Log ("LOCK");
		ANIMATING = true;
	}

	public override void AnimationUnlock(){
//		Debug.Log ("UNLOCK");
		ANIMATING = false;
	}

}
