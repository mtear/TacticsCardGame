using UnityEngine;
using System.Collections;
using Tactics_CoreGameEngine;

public class MasterMonitor : MonoBehaviour {

	void Start () {
	
	}

	public static int STATUS = 0;

	public static int IDLE = 0;
	public static int SETTLING = 1;
	public static int DYINGSTART = 10;
	public static int DYING = 11;
	public static int SUMMONSTART = 20;
	public static int SUMMONING = 21;
	public static int TRAPSUMMONSTART = 22;
	public static int TRAPSUMMONING = 23;
	public static int ATTACKSTART = 30;
	public static int ATTACKING = 31;
	public static int SWITCHTURN = 40;

	public static int SETTLED = 100;

	//State machine
	void FixedUpdate () {
		if (MainDriver.MAINTABLE.STILLATTACKING)
			return;
		
		if (UnityCommandInterface.TRIGGER) {
			if (STATUS == IDLE) {
				//Debug.Log ("Starting master loop!");
				STATUS = SETTLING;
			} else if (STATUS == SETTLING) {
				//branch
				//if (UnityAnimationInterface.TURNSWITCHING) {
				//	STATUS = SWITCHTURN;
				//} else {
					STATUS = SUMMONSTART;
				//}
			} else if (STATUS == SUMMONSTART) {
				if (GameBoardMonitor.SUMMONING) {
					STATUS = SUMMONING;
				} else {
					STATUS = TRAPSUMMONSTART;
				}
			} else if (STATUS == SUMMONING) {
				if (!SummoningMonitor.SUMMONING) {
					STATUS = SUMMONSTART;
				}
			} else if (STATUS == TRAPSUMMONSTART) {
				if (TrapBoardMonitor.SUMMONING) {
					STATUS = TRAPSUMMONING;
				} else {
//					Debug.Log ("Dying start");
					STATUS = DYINGSTART;
				}
			} else if (STATUS == TRAPSUMMONING){
				if (!TrapSummoningMonitor.SUMMONING) {
					STATUS = TRAPSUMMONSTART;
				}
			} else if (STATUS == DYINGSTART) {
				STATUS = DYING;
			} else if (STATUS == DYING) {
				if (CharacterMonitor.DYINGCOUNT == 0) {
					STATUS = SETTLED;
				}
			} else if (STATUS == SWITCHTURN) {
				if (!TurnSwitchMonitor.SWITCHING) {
					//test
					STATUS = IDLE;
					UnityCommandInterface.TRIGGER = false;
					Debug.Log ("Turn switch continuing!");
					MainDriver.MAINTABLE.SwitchTurnContinue ();
				}
			} else if (STATUS == SETTLED) {
				if (UnityAnimationInterface.TURNSWITCHING) {
					STATUS = SWITCHTURN;
				} else {
					STATUS = IDLE;
					UnityCommandInterface.TRIGGER = false;
					MainDriver.UAI.SettleContinue ();
				}
			}
		}

		if (STATUS == IDLE && !UnityAnimationInterface.ANIMATING) {
			if (MainDriver.playercommands.Count > 0) {
				Command command = MainDriver.playercommands [0];
				MainDriver.playercommands.RemoveAt (0);
				bool b = MainDriver.UCI.Flush (MainDriver.MAINTABLE.P1, command);
				if (!b)
					MainDriver.playercommands.Insert (0, command);
			}
		}

	} // End update
}
