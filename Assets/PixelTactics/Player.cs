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
* Player.cs
* Author: Nic Wilson
* Last updated: 3/28/2016
*/

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tactics_CoreGameEngine
{
	public class Player
	{
		public String Name;
		public GameBoard GAMEBOARD;
		public Character[] TRAPS;
		public Hand HAND;
		public Hand GRAVEYARD;
		public Deck DECK;
		public Player ENEMY;
		public int Life;
		private int MAXLIFE;
		public bool Targetable = true;
		public CommandInterface COMMANDINTERFACE;
		public bool RevealHandEffect = false;
		public int RecruitsRemaining = 1;

		private int ROWS, COLUMNS;

		public TableTop TABLE;

		public int BaseMaxHandSize = 5;
		private int m_MaxHandSize = 5;
		public int MaxHandSize {
			get {
				if (m_MaxHandSize < 0)
					return 0;
				return m_MaxHandSize;
			}
			set{
				m_MaxHandSize = value;
			}
		}

		public List<Character> Traps{
			get{
				List<Character> ret = new List<Character> ();
				foreach (Character c in TRAPS) {
					if (c != null
						&& c.HandAbility.GetType().IsSubclassOf(typeof(Trigger)))
						ret.Add (c);
				}
				return ret;
			}
		}

		public Player (string Name, int ROWS, int COLUMNS, int MAXLIFE,
			Player ENEMY, CommandInterface COMMANDINTERFACE)
		{
			this.Name = Name;
			this.ROWS = ROWS; this.COLUMNS = COLUMNS;
			this.GAMEBOARD = new GameBoard (COLUMNS, ROWS, this);
			this.TRAPS = new Character[3];
			this.Life = MAXLIFE;
			this.MAXLIFE = MAXLIFE;
			this.HAND = new Hand ();
			this.GRAVEYARD = new Hand ();
			this.ENEMY = ENEMY;
			COMMANDINTERFACE.P = this;
			this.COMMANDINTERFACE = COMMANDINTERFACE;
		}

		public void LoadDeck(Deck DECK){
			this.DECK = DECK;
		}

		public void DrawHand(){
			for (int i = 0; i < 4; i++) {
				DrawCard ();
			}
		}

		public void SetEnemy(Player ENEMY){
			this.ENEMY = ENEMY;
			for (int i = 0; i < 4; i++) {
				DrawCard ();
			}
		}

		public void GetTurnCommand(){
			COMMANDINTERFACE.GetTurnCommand ();
		}

		public void DrawCard(){
			Command c = new Command (Command.TYPE.DRAW,
				            null, this);
			Command.Execute (c);
		}

		public void ClearBoard(){
			for (int i = 0; i < ROWS; i++) {
				for (int a = 0; a < COLUMNS; a++) {
					GAMEBOARD.Board [a, i] = null;
				}
			}
		}

		public bool AddTrap(Character c){
			bool g = false;
			for (int i = 0; i < 3; i++) {
				if (TRAPS [i] == null) {
					TRAPS [i] = c;
					g = true;
					break;
				}
			}
			return g;
		}

		public bool AddTrap(Character c, int index){
			if (TRAPS [index] != null)
				return false;
			TRAPS [index] = c;
			return true;
		}

		public void LoseFromDrawOut(){
			if (DECK.Count == 0) {
				Life = 0;
				TABLE.VALID = false;
			}
		}

		public bool Active(int handindex, int x){
			if (TRAPS [x] != null)
				return false;
			Character c = HAND.Get(handindex);
			if (c == null)
				return false;
			Active a = (Active)c.HandAbility;
			if (a == null || a.GetType().IsSubclassOf(typeof(Trigger)))
				return false;
			//Get the target if there is any and return false if it fails
			//Request the target from the interface, continue in ActiveContinue
			a.GetTarget(this, c);
			return true;
		}

		public void ActiveContinue(bool t, Character c, Target T){
			if (!t) { //Change
				TABLE.Flush (this, new Command (Command.TYPE.NULL, null, this));
				return;
			}

			Active a = (Active)c.HandAbility;
			a.TARGET = T;

			//Add pipeline event for the active
			TriggerPacket TP = new TriggerPacket (Trigger.TYPE.PLAYORDER,
				this, c, this);
			TP.ACTIVETARGET = a;
			TABLE.PIPELINE.Add (TP);

			HAND.Discard (c, GRAVEYARD);
			TABLE.Flush (this, new Command (Command.TYPE.NULL, null, this));
		}

		public bool Trap(int handindex, int index){
			Character c = HAND.Get (handindex);
			if (c == null)
				return false;
			Ability a = (Ability)c.HandAbility;
			if (a == null || !a.GetType().IsSubclassOf(typeof(Trigger)))
				return false;
			bool g;
			if (index == -1)
				g = AddTrap (c);
			else
			    g = AddTrap (c, index);
			if (g) {
				HAND.Discard (c);
				TABLE.PIPELINE.Add (new TriggerPacket (Trigger.TYPE.PLAYTRAP, this, c, this));
			}

			return g;
		}

		public void AddDamage(Damage D){
			this.Life -= D.Value;
			TABLE.PIPELINE.Add (new TriggerPacket (Trigger.TYPE.PLAYERDAMAGE, this, D.Source, D.Source.CONTROLLER));
		}

		public void HealDamage(Damage D){
			this.Life += D.Value;
			if (this.Life > MAXLIFE)
				this.Life = MAXLIFE;
			TABLE.PIPELINE.Add (new TriggerPacket (Trigger.TYPE.PLAYERHEAL, this, D.Source, D.Source.CONTROLLER));
		}

		public bool OnGoing(int handindex, int index){
			Character c = HAND.Get (handindex);
			if (c == null)
				return false;
			Ability a = (Ability)c.HandAbility;
			if (a == null || !a.GetType().IsSubclassOf(typeof(OnGoing)))
				return false;
			bool g;
			if (index == -1)
				g = AddTrap (c);
			else
				g = AddTrap (c, index);
			if (g) {
				HAND.Discard (c);
				TABLE.PIPELINE.Add (new TriggerPacket (Trigger.TYPE.PLAYONGOING, this, c, this));
			}
			return g;
		}

		public void PrintHand(bool hide){
			foreach (Character c in HAND.Cards) {
				if (!hide || RevealHandEffect)
					Console.WriteLine (c);
				else
					Console.WriteLine ("****** HIDDEN CARD ******");
			}
		}

		public void PrintTraps(){
			foreach (Character c in TRAPS) {
				Console.Write ("< ");
				if (c == null)
					Console.Write (" ");
				else if (c.HandAbility.GetType ().IsSubclassOf (typeof(Trigger)))
					Console.Write ("T");
				else
					Console.Write (((OnGoing)(c.HandAbility)).Timer);
				Console.Write (" >");
			}
			Console.WriteLine ();
		}

		public bool DoesControlUnit(Character c){
			for (int i = 0; i < ROWS; i++) {
				for (int a = 0; a < COLUMNS; a++) {
					if (GAMEBOARD.Board [a, i] == c)
						return true;
				}
			}
			return false;
		}

		public List<PassivePair> GatherPassives(){
			List<PassivePair> ret = new List<PassivePair> ();
			//Grab ongoing passives
			for (int i = 0; i < TRAPS.Length; i++) {
				if (TRAPS [i] == null)
					continue;
				if ((TRAPS [i]).HandAbility.GetType ().IsSubclassOf (typeof(OnGoing))) {
					//If this trap slot is an ongoing
					OnGoing og = (OnGoing)TRAPS [i].HandAbility;
					foreach (Passive p in og.Passives) {
						ret.Add (new PassivePair(p, TRAPS[i]));
					}
				}
			}
			//Board passives
			ret.AddRange(GAMEBOARD.GatherPassives());
			ret.Sort((x,y) => x.PASSIVE.Priority.CompareTo(y.PASSIVE.Priority));
			return ret;
		}

		public void EndTurn(){
			//Debug.Log ("OMG " + Name);

			//Check expired ongoings
			for (int i = 0; i < TRAPS.Length; i++) {
				if (TRAPS [i] == null)
					continue;
				TRAPS [i].TrapArmed = true;
				if (TRAPS [i].HandAbility.GetType ().IsSubclassOf (typeof(OnGoing))) {
					//If this trap slot is an ongoing
					OnGoing og = (OnGoing)TRAPS [i].HandAbility;
					og.Decrement ();
					if (og.Expired) {
						og.Discard (this);
						GRAVEYARD.AddCard (TRAPS [i], 999, null);
						TRAPS [i] = null;
					}
				}
			}
		}

		public void PostEndTurn(){
			GAMEBOARD.ResetFlags ();
			GAMEBOARD.ResetStunned ();

			this.RecruitsRemaining = 1;
		}

		public void StartTurn(){
			GAMEBOARD.ResetFlags ();
		}

	} // End Player class

} // End namespace

