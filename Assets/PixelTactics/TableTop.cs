/*************************************************************************
 * 
 * FELICITY CONFIDENTIAL
 * __________________
 * 
 *  [2016] - [2016] Felicity Entertainment
 *  All Rights Reserved.
 * 
 * NOTICE:  All information contained herein is, and remains
 * the property of Felicity Entertainment and its suppliers,
 * if any.  The intellectual and technical concepts contained
 * herein are proprietary to Felicity Entertainment and its 
 * suppliers and may be covered by U.S. and Foreign Patents,
 * patents in process, and are protected by trade secret or 
 * copyright law. Dissemination of this information or reproduction
 * of this material is strictly forbidden unless prior written
 * permission is obtained from Felicity Entertainment.
 */

/*
* TableTop.cs
* Author: Nic Wilson
* Last updated: 3/27/2016
*/

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tactics_CoreGameEngine
{
	public class TableTop
	{
		public static readonly int ROWS = 2, COLUMNS = 3, MAXLIFE = 15;

		private bool firstturn = true;

		public bool VALID = true;
		public bool STILLATTACKING = false;
		public bool CHANGINGTURNS = false;

		public List<TriggerPacket> PIPELINE = new List<TriggerPacket>();
		public List<TriggerPair> EFFECTPIPELINE = new List<TriggerPair>();

		public Player WINNER = null;
		public Player CURRENTTURN = null;
		public Player P1;
		public Player P2;

		public StringTable STRINGTABLE;

		public AnimationInterface ANIMATIONINTERFACE;

		private int currentplayercount = 1;

		private int STATE = 0;

		public TableTop (String LCODE, Player P1, Player P2,
			AnimationInterface ANIMATIONINTERFACE)
		{
			ANIMATIONINTERFACE.SetTable (this);
			this.ANIMATIONINTERFACE = ANIMATIONINTERFACE;

			P1.TABLE = this;
			P2.TABLE = this;
			this.P1 = P1;
			this.P2 = P2;

			//Setup string table
			STRINGTABLE = new StringTable(LCODE, P1);

			P1.DrawHand ();
			P2.DrawHand ();

			Play (P1);
		}

		public void Play(Player PLAYER){
			Turn1 (PLAYER); //first turn
		}

		public bool Flush(Player P, Command c){
			//Ignore if it's not this player's turn
			if (P != CURRENTTURN || CHANGINGTURNS)
				return false;


			bool validturn = true;
			bool successful = true;
			//If the command is an active, break out and handle
			if (c != null && c.Type == Command.TYPE.ACTIVE) {
				//Go to the start active
				//Continue later
				validturn = false;
				successful = P.Active (int.Parse (c.Parameters [0]),
					int.Parse(c.Parameters[1]));

				if (!successful) {
					STATE = 1;
					SettleState (P);
				}
				//return false;
			}else{
				//Find if this is a valid move or not
				//Execute if it is
				try{
					validturn = Command.Execute (c);
					successful = validturn;
				}catch
				{
					validturn = false;
					successful = false;
				}

				if (c == null)
					validturn = false;
				else
					validturn = c.Type == Command.TYPE.PASS;

				if (c != null && c.Type == Command.TYPE.PASS) {
					successful = true;
				}

				//If it wasn't a valid turn, repeat grab
				if (!validturn) {
					STATE = 1;
					SettleState (P);
					//Will resume in Turn3
				} else {
					//Successful turn,
					//Switch player's and move on
					CHANGINGTURNS = true;
					//				Debug.Log ("CHANGING TURNS TRUE");
					STATE = 2; //TESTING
					SwitchTurn ();
				}
			}



			return successful;
		}

		public void Turn1(Player P){
			//Debug.Log ("Turn1 " + P.Name);
			CURRENTTURN = P;
			STATE = 0;

			//Add Start turn trigger event
			PIPELINE.Add (new TriggerPacket (Trigger.TYPE.STARTTURN,
				P, null, P));
			SettleState (P);
			//Will resume in Turn2
		}

		public void Turn2(){
			//Debug.Log ("Turn2 " + CURRENTTURN.Name);
			CURRENTTURN.StartTurn ();
			if (!firstturn) CURRENTTURN.DrawCard (); //Draw if not the first turn
			else firstturn = false;
			SettleState(CURRENTTURN);
			//Will resume in Turn3
		}

		public void Turn3(){
			//Debug.Log ("Turn3 " + CURRENTTURN.Name);
			Print (CURRENTTURN);
			TakeTurn (CURRENTTURN);
		}

		public void TakeTurn(Player PLAYER){
			//Kick off Command grabbing
			PLAYER.GetTurnCommand ();
			//Code will resume in Flush()
		}

		private void SwitchTurn(){
			//End the current turn
			CURRENTTURN.EndTurn ();
			SettleState (CURRENTTURN);
			//Will resume in Turn4
		}

		private void Turn4(){
			//Debug.Log ("Turn4 " + CURRENTTURN.Name);
			CURRENTTURN.PostEndTurn ();

			currentplayercount++;
			if (currentplayercount == 1) {
				FullAttack (CURRENTTURN);
			} else
				SwitchTurn2 ();
		}

		public void AttackContinue(List<Character> Units){
			if (Units.Count > 0) {
				FullAttackLoop (Units);
			} else {
				SwitchTurn2 ();
			}
		}

		private void SwitchTurn2(){
			CheckDeadPlayers ();
			if (!VALID)
				return;

			STILLATTACKING = false;
//			Debug.Log ("SWITCHING TURN!");
			if (currentplayercount == 2) {
				currentplayercount = 0;
				CURRENTTURN = CURRENTTURN.ENEMY;
			}

			//Next turn, continue in switchturn3
			ANIMATIONINTERFACE.PrepareNextTurn();
		}

		public void SwitchTurn3(){
			//Next turn
			ANIMATIONINTERFACE.SwitchTurnBreak();
		}

		public void SwitchTurnContinue(){
//			Debug.Log ("CHANGING TURNS FALSE");
			CHANGINGTURNS = false;
			Turn1 (CURRENTTURN);
		}

		public void EndGame(){
			Print (CURRENTTURN);
			if (WINNER == null)
				Console.WriteLine ("\n\n\nTHE GAME IS A DRAW");
			else
				Console.WriteLine ("\n\n\n" + WINNER.Name + " WINS!");
			//Environment.Exit (0);
		}

		public void Print(Player P){
			//Console.Clear ();
			Console.WriteLine("\n\n\n\n\n\n");
			P.ENEMY.PrintTraps ();
			P.ENEMY.GAMEBOARD.PrintBoard (true);
			P.ENEMY.GAMEBOARD.PrintUnits ();
			Console.WriteLine ("---------------");
			P.GAMEBOARD.PrintBoard (false);
			P.PrintTraps ();
			P.GAMEBOARD.PrintUnits ();
			Console.WriteLine ();

			Console.WriteLine (P.Name + "(" + P.Life + ") HAND:");
			P.PrintHand (false);
			Console.WriteLine ();
			Console.WriteLine (P.ENEMY.Name + "(" + P.ENEMY.Life + ") HAND:");
			P.ENEMY.PrintHand (true);
			Console.WriteLine ();
		}

		public void CheckDeadPlayers(){
			Player P = CURRENTTURN;
			if (P.Life <= 0 || P.ENEMY.Life <= 0) {
				VALID = false;
				if (P.Life <= 0 && P.ENEMY.Life <= 0)
					WINNER = null;
				else if (P.Life <= 0)
					WINNER = P.ENEMY;
				else
					WINNER = P;
				ANIMATIONINTERFACE.EndGameBreak ();

			}
		}

		public void SettleState(Player P){
			//if (!VALID)
			//	return;

			ANIMATIONINTERFACE.AnimationLock ();

			P.GAMEBOARD.CheckForDeaths ();
			P.ENEMY.GAMEBOARD.CheckForDeaths ();
			CalculateUnitStats (P);
			CalculateUnitStats (P.ENEMY);
			PopTrigger (P);
			//break and resume in 1p5
		}

		public void SettleState1p5(){
			bool haseffects = EFFECTPIPELINE.Count > 0;
			if (haseffects) { //Go through the effect pipeline if it has stuff
				while (EFFECTPIPELINE.Count > 0) {
					PopEffect ();
				}
			}else SettleState2 (haseffects);
		}

		public void SettleState2(bool haseffects){
			if (haseffects || PIPELINE.Count > 0) {
				ANIMATIONINTERFACE.SettleBreak (); //Recursive call
				//Resume back here in SettleState
			} else { //End recursion
				ANIMATIONINTERFACE.AnimationUnlock();

				//Check dead players
				CheckDeadPlayers();
				//Where to go from here
				if (STATE == 0) {
					STATE++;
					Turn2 ();
				} else if (STATE == 1) {
					STATE++;
					Turn3 ();
				} else if (STATE == 2) { //ending turn
					STATE++;
					Turn4 ();
				}

			}
		}

		void PopEffect(){ //Pop EFFECTS!!
			if (EFFECTPIPELINE.Count == 0) return;
			TriggerPair T = EFFECTPIPELINE [0];
			EFFECTPIPELINE.RemoveAt (0);
			//Animate. Return in PopEffect2
			ANIMATIONINTERFACE.TriggerEffectAnimation(T);
		}

		public void PopEffectContinue(TriggerPair T){ //Pop EFFECTS of triggers
			if (T.TP.TYPE == Trigger.TYPE.PLAYORDER) {
				if (T.TP.ACTIVETARGET.VALID) {
					T.TP.ACTIVETARGET.Execute (T.TP.TARGET, T.TP.PLAYER);
					//ANIMATIONINTERFACE.TemporaryPlayActiveSound (T.TP.TARGET.NameCode);
				}
			} else {
				T.TRIGGER.Execute (T.TP);
			}

			//Testing
			if (EFFECTPIPELINE.Count == 0) {
				SettleState2 (true);
			}
		}

		void PopTrigger(Player P){
			//No need to animate if no triggers
			if (PIPELINE.Count == 0) {
				SettleState1p5 ();
				return;
			}
			TriggerPacket TP = PIPELINE [0];
			PIPELINE.RemoveAt (0);
			ProcessTriggerPacket (TP, P);
			BroadcastEvent (TP, EFFECTPIPELINE);
			//return TP;
			//BREAK AND RESUME IN 1p5!!!
			ANIMATIONINTERFACE.TriggerAnimation(TP);
		}

		private void ProcessTriggerPacket(TriggerPacket TP, Player P){
			if (TP.TYPE == Trigger.TYPE.UNITDIED
				|| TP.TYPE == Trigger.TYPE.MOVE
				|| TP.TYPE == Trigger.TYPE.STARTTURN) {
				TP.PLAYER = (P.DoesControlUnit (TP.USER)) ? P : P.ENEMY;
				TP.INITIATOR = (P.DoesControlUnit (TP.USER)) ? P : P.ENEMY;
			}
		}

		public void FullAttack(Player P){
			Debug.Log ("ATTACKING!");
			STILLATTACKING = true;
			ANIMATIONINTERFACE.AttackStartBreak ();
		}

		public void FullAttackStart(Player P){
			Debug.Log ("START ATTACKING!");
			List<Character> Units = P.GAMEBOARD.Units;
			Units.AddRange(P.ENEMY.GAMEBOARD.Units);
			Units.Sort((x,y) => x.CONTROLLER.GAMEBOARD.LocateInBoard(x).x.CompareTo(
				y.CONTROLLER.GAMEBOARD.LocateInBoard(y).x));
			FullAttackLoop (Units);
		}

		public void FullAttackLoop(List<Character> Units){
			if (Units.Count == 0) {
				//return;
			}
			Character attacker = null;
			while (attacker == null && Units.Count > 0) {
				attacker = Units [0];
				Units.RemoveAt (0);
				Point p = attacker.CONTROLLER.GAMEBOARD.LocateInBoard (attacker);
				if (!attacker.CONTROLLER.GAMEBOARD.ValidAttack(p.x, p.y)) {
					attacker = null;
				}
			}
			if (attacker == null) {
				SwitchTurn2 ();
			} else {

				Point p = attacker.CONTROLLER.GAMEBOARD.LocateInBoard (attacker);
				Character target = attacker.CONTROLLER.ENEMY.GAMEBOARD.FindCharacterInMelee (p.x);
				attacker.CONTROLLER.GAMEBOARD.Melee (p.x, p.y);
				attacker.CONTROLLER.TABLE.ANIMATIONINTERFACE.AttackBreak (attacker, target, Units);

			}
		}

		public void CalculateUnitStats(Player P){
			P.GAMEBOARD.CalculateUnitStats ();
		}

		public void BroadcastEvent(TriggerPacket TP, List<TriggerPair> EFFECTPIPELINE){
			bool countered = CheckTraps (TP, EFFECTPIPELINE);
			if (countered) {
				PIPELINE.Add (TP);
				return;
			}

			//Check unit triggers
			for (int i = 0; i < ROWS; i++) {
				for (int a = 0; a < COLUMNS; a++) {
					Player A = TP.PLAYER;
					for(int j = 0; j < 2; j++){ //Go twice for both players
						Character c = A.GAMEBOARD.Board [a, i];
						if (c == null || c.Dead) {
							A = TP.PLAYER.ENEMY;
							continue;
						}

						foreach (Trigger trig in c.Triggers[i]) {
							TP.USER = c;
							trig.CheckAndExecute (TP, EFFECTPIPELINE);
						}

						//Setup for next loop
						A = TP.PLAYER.ENEMY;
					}
				}
			}

			//Checked and processed
			//If a trapactivate, put on effect stack (or order activate)
			if (TP.TYPE == Trigger.TYPE.TRAPACTIVATE) {
				TP.TRIGGERTARGET.ExportEffectToPipeline (TP, EFFECTPIPELINE);
			}
			if (TP.TYPE == Trigger.TYPE.PLAYORDER) {
				TP.ACTIVETARGET.ExportEffectToPipeline (TP, EFFECTPIPELINE);
			}

		}

		public bool CheckTraps(TriggerPacket TP, List<TriggerPair> EFFECTPIPELINE){
			bool countered = false;
			//Check traps
			Player P = TP.PLAYER;
			Character[] traps = TP.PLAYER.TRAPS;
			for (int a = 0; a < 2; a++) {
				for (int i = 0; i < traps.Length; i++) {
					if (traps [i] == null)
						continue;
					if (!traps [i].TrapArmed)
						continue;
					if (traps [i].HandAbility.GetType ().IsSubclassOf (typeof(Trigger))) {
						//Get the trap
						Trigger t = (Trigger)traps [i].HandAbility;
						TP.USER = traps [i];
						bool activated = t.Check (TP);
						//Activate trap
						if (activated) {
							TriggerPacket TRAPPACKET = new TriggerPacket (Trigger.TYPE.TRAPACTIVATE,
								traps[i].CONTROLLER, traps[i], traps[i].CONTROLLER, TP.TARGET);
							TRAPPACKET.TRIGGERTARGET = (Trigger)traps[i].HandAbility;
							TRAPPACKET.TPTARGET = TP;
							if (TRAPPACKET.TRIGGERTARGET.COUNTER) {
								PIPELINE.Insert (0, TRAPPACKET);
								countered = true;
							}
							else PIPELINE.Add(TRAPPACKET);

							//discard
							P.GRAVEYARD.AddCard (traps [i], 999, null);
							traps [i] = null;
						}
					} else if (traps [i].HandAbility.GetType ().IsSubclassOf (typeof(OnGoing))) {
						//Get the ongoing
						OnGoing og = (OnGoing)traps [i].HandAbility;
						foreach (Trigger t in og.Triggers) {
							TP.USER = traps [i];
							t.CheckAndExecute (TP, EFFECTPIPELINE);
						}
					}
				}
				traps = TP.PLAYER.ENEMY.TRAPS;
				P = TP.PLAYER.ENEMY;
			}
			return countered;
		}

	} // End TableTop

} // End namespace

