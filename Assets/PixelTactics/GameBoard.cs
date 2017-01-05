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
* GameBoard.cs
* Author: Nic Wilson
* Last updated: 3/28/2016
*/

using System;
using System.Collections.Generic;

namespace Tactics_CoreGameEngine
{
	/// <summary>
	/// A class representing a Player's Game Board
	/// </summary>
	public class GameBoard
	{

		//----------------------------------------------------------------

		/// <summary>
		/// Gets a value indicating whether this
		///  <see cref="Tactics_CoreGameEngine.GameBoard"/> is full.
		/// </summary>
		/// <value><c>true</c> if full; otherwise, <c>false</c>.</value>
		public bool Full{
			get{
				for (int i = 0; i < ROWS; i++) {
					for (int a = 0; a < COLUMNS; a++) {
						if (Board [a, i] == null)
							return false;
					}
				}
				return true;
			}
		}

		/// <summary>
		/// Gets the number of corpses in this GameBoard
		/// </summary>
		/// <value>The number of corpses.</value>
		public int NumberOfCorpses{
			get{
				int corpses = 0;
				for (int i = 0; i < ROWS; i++) {
					for (int a = 0; a < COLUMNS; a++) {
						if (Board [a, i] != null
							&& Board [a, i].Dead)
							corpses++;
					}
				}
				return corpses;
			}
		}

		/// <summary>
		/// Gets the number of units in this GameBoard
		/// </summary>
		/// <value>The number of units.</value>
		public int NumberOfUnits{
			get{
				int units = 0;
				for (int i = 0; i < ROWS; i++) {
					for (int a = 0; a < COLUMNS; a++) {
						if (Board [a, i] != null
							&& !Board [a, i].Dead)
							units++;
					}
				}
				return units;
			}
		}

		public List<Character> Units{
			get{
				List<Character> ret = new List<Character> ();
				for (int i = 0; i < ROWS; i++) {
					for (int a = 0; a < COLUMNS; a++) {
						if (Board [a, i] != null && !Board[a,i].Dead)
							ret.Add (Board [a, i]);
					}
				}
				return ret;
			}
		}

		public List<Character> AllUnits{
			get{
				List<Character> ret = new List<Character> ();
				for (int i = 0; i < ROWS; i++) {
					for (int a = 0; a < COLUMNS; a++) {
						if (Board [a, i] != null)
							ret.Add (Board [a, i]);
					}
				}
				return ret;
			}
		}

		//----------------------------------------------------------------

		/// <summary>
		/// The matrix of Characters in this GameBoard
		/// </summary>
		public Character[,] Board;

		/// <summary>
		/// The Player that owns this GameBoard
		/// </summary>
		public Player Owner;

		//----------------------------------------------------------------

		/// <summary>
		/// The dimensions of this GameBoard
		/// </summary>
		private int COLUMNS, ROWS;

		//----------------------------------------------------------------

		/// <summary>
		/// Initializes a new instance of the 
		/// <see cref="Tactics_CoreGameEngine.GameBoard"/> class.
		/// </summary>
		/// <param name="COLUMNS">Board width</param>
		/// <param name="ROWS">Board height</param>
		/// <param name="OWNER">The owner Player</param>
		public GameBoard (int COLUMNS, int ROWS, Player OWNER)
		{
			this.Owner = OWNER;
			this.COLUMNS = COLUMNS; this.ROWS = ROWS;
			Board = new Character[COLUMNS, ROWS];
		}

		//----------------------------------------------------------------

		/// <summary>
		/// Calculates the modified damage a unit will do to another unit
		/// </summary>
		/// <returns>The modified attack damage</returns>
		/// <param name="damage">A Damage object</param>
		/// <param name="attacker">The Attacker.</param>
		/// <param name="target">The Target.</param>
		public Damage CalculateModifyDamageToX(Damage damage, Character attacker,
			Character target){

			//Get the base value
			int value = damage.Value;
			//Gather all the passives up
			List<PassivePair> AP = attacker.CONTROLLER.GAMEBOARD.GatherPassives();
			List<PassivePair> EP = attacker.CONTROLLER.ENEMY.GAMEBOARD.GatherPassives ();
			//Modify the damage value using the passives
			foreach (PassivePair P in AP) {
				value = P.PASSIVE.ModifyDamageToX (value, P.CHARACTER, attacker, target);
			}
			foreach (PassivePair P in EP) {
				value = P.PASSIVE.ModifyDamageToX (value, P.CHARACTER, attacker, target);
			}

			//Modify the damage object
			damage.Value = value;
			//return
			return damage;
		}

		/// <summary>
		/// Triggers the GameBoard to calculate modified unit stats from passives
		/// </summary>
		public void CalculateUnitStats(){
			Owner.RevealHandEffect = CalculateRevealHand ();
			Owner.MaxHandSize = CalculateMaxHandSize ();
			Owner.Targetable = CalculatePlayerTargetable ();
			for (int i = 0; i < ROWS; i++) {
				for (int a = 0; a < COLUMNS; a++) {
					if (Board [a, i] != null) {
						Board [a, i].ClearEnhancements ();
						Board [a, i].Attack = CalculateAttack (a, i);
						Board [a, i].Life = CalculateLife (a, i);
						Board [a, i].IsMelee = CalculateAttackType(a,i);
						Board [a, i].Intercept = CalculateIntercept(a,i);
						Board [a, i].Rooted = CalculateRooted(a,i);
						Board [a, i].Overkill = CalculateOverkill(a,i);
						Board [a, i].Armor = CalculateArmor(a,i);
						Board [a, i].Zombie = CalculateZombie(a,i);
						Board [a, i].Targetable = CalculateTargetable (a, i);
					}
				}
			}
		}

		/// <summary>
		/// Checks for the deaths of units in this GameBoard
		/// </summary>
		public void CheckForDeaths(){
			for (int i = 0; i < ROWS; i++) {
				for (int a = 0; a < COLUMNS; a++) {
					if(Board[a,i] != null) Board [a, i].CheckDead ();
				}
			}
		}

		/// <summary>
		/// Clears a corpse in this GameBoard
		/// </summary>
		/// <returns><c>true</c>, if corpse was cleared,
		///  <c>false</c> otherwise.</returns>
		/// <param name="GRAVEYARD">The discard pile</param>
		/// <param name="x1">The column to remove from</param>
		/// <param name="y1">The row to remove from</param>
		public bool ClearCorpse(Hand GRAVEYARD, int x, int y){
			if (Board [x, y].Dead) {
				GRAVEYARD.AddCard (Board [x, y], 999, null);

				//Add to Pipeline
				Owner.TABLE.PIPELINE.Add (new TriggerPacket (Trigger.TYPE.CLEARCORPSE,
					Owner, Board[x,y], Owner));

				Board [x, y] = null;

				return true;
			} else
				return false;
		}

		/// <summary>
		/// Whether or not this GameBoard contains the specified Character
		/// </summary>
		/// <param name="c">A Character</param>
		public bool Contains(Character c){
			for (int i = 0; i < ROWS; i++) {
				for (int a = 0; a < COLUMNS; a++) {
					if (Board [a, i] == c)
						return true;
				}
			}
			return false;
		}

		/// <summary>
		/// Finds a character in melee in the specified column
		/// </summary>
		/// <returns>The character in melee.</returns>
		/// <param name="col">The column</param>
		public Character FindCharacterInMelee(int col){
			Character ret = null;
			for (int i = 0; i < ROWS; i++) {
				//Look for intercept target in the row
				Character InterceptTarget = FindInterceptTarget (i);
				if (InterceptTarget != null) {
					return InterceptTarget;
				}
				//Otherwise look through the column and return the first
				//	alive Character
				if (Board [col, i] != null && !Board[col,i].Dead) {
					return Board [col, i];
				}
			}
			return ret;
		}

		/// <summary>
		/// Find any intercept Character in a row if there are any
		/// </summary>
		/// <returns>The intercept target, or null if none exist</returns>
		/// <param name="row">The row to search in</param>
		public Character FindInterceptTarget(int row){
			List<Character> interceptors = new List<Character> ();
			//Gather alive characters with intercept in the row
			for (int i = 0; i < COLUMNS; i++) {
				if (Board [i, row] == null || Board[i,row].Dead)
					continue;
				if (Board [i, row].Intercept)
					interceptors.Add (Board [i, row]);
			}
			//Pick one at random or return null if none
			if (interceptors.Count == 0)
				return null;
			else
				return interceptors [Utility.R.Next (interceptors.Count)];
		}

		/// <summary>
		/// Gathers the passives from this GameBoard
		/// </summary>
		/// <returns>The passives.</returns>
		public List<PassivePair> GatherPassives(){
			//Make a returnable list
			List<PassivePair> ret = new List<PassivePair> ();
			//Search the board for passives
			for (int i = 0; i < ROWS; i++) {
				for (int a = 0; a < COLUMNS; a++) {
					Character uc1 = Board[a,i];
					if (uc1 == null || uc1.Dead)
						continue;
					foreach(Passive p in uc1.Passives[i]){
						ret.Add (new PassivePair (p, uc1));
					}

				}
			}
			//Sort the passives by priority
			ret.Sort((x,y) => x.PASSIVE.Priority.CompareTo(y.PASSIVE.Priority));
			return ret;
		}

		/// <summary>
		/// Determines whether this lane is open the specified column.
		/// </summary>
		/// <returns><c>true</c> if this instance is lane open;
		///  otherwise, <c>false</c>.</returns>
		/// <param name="attackcolumn">The column to look in</param>
		public bool IsLaneOpen(int attackcolumn){
			bool open = true;
			for(int i = 0; i < ROWS; i++){
				open = (Board[attackcolumn, i] == null) && open;
			}
			return open;
		}

		/// <summary>
		/// Locates a Character in the GameBoard
		/// </summary>
		/// <returns>The location of the Character in the board,
		/// or null</returns>
		/// <param name="c">The Character to look for</param>
		public Point LocateInBoard(Character c){
			for (int i = 0; i < ROWS; i++) {
				for (int a = 0; a < COLUMNS; a++) {
					if (Board [a, i] == c)
						return new Point (a, i);
				}
			}
			return null;
		}

		public int LocateTrap(Character c){
			for (int i = 0; i < Owner.TRAPS.Length; i++) {
				if (Owner.TRAPS [i] == c) {
					return i;
				}
			}
			return -1;
		}

		/// <summary>
		/// Perform a melee attack with a character
		/// </summary>
		/// <param name="x">The first x value.</param>
		/// <param name="y">The first y value.</param>
		/// <param name="col">The target column</param>
		public bool Melee(int x, int y){
			//Get the attacker
			Character attacker = Board [x, y];
			//Return false if the character can't make an attack
			if (attacker == null || attacker.Dead || attacker.Stunned)
				return false;

			Player ENEMY = Owner.ENEMY;
			//Get the target for this character to attack
			Character target = ENEMY.GAMEBOARD.FindCharacterInMelee (x);

			//Create a Damage type
			Damage.TYPE DT = Damage.TYPE.MELEE;
			if (!attacker.IsMelee)
				DT = Damage.TYPE.RANGE;

			//Perform the attack
			if (target != null) {
				target.AddDamage (GetDamage(DT, attacker.Attack, attacker, target));
				//Calculate the Overkill keyword
				for (int r = 0; r < 2; r++) {
					if (target == null)
						continue;
					if (attacker.Overkill && target.Damage >= target.Life) {
						int overkilldamage = target.Damage - target.Life;
						target = ENEMY.GAMEBOARD.FindOverkillTarget (target);
						if (target != null) {
							target.AddDamage (GetDamage (
								DT, overkilldamage, attacker, target));
						} else {
							ENEMY.AddDamage (GetDamage(
								DT, overkilldamage, attacker, null));
						}
					}
				}
			} else {
				//If there isn't a Character target, hit the enemy Player
				ENEMY.AddDamage (GetDamage(DT, attacker.Attack, attacker, null));
			}

			//Attack successful
			return true;
		}

		/// <summary>
		/// Move the specified Character from x1, y1 to
		///  x2, y2.
		/// </summary>
		/// <param name="x1">The first x value.</param>
		/// <param name="y1">The first y value.</param>
		/// <param name="x2">The second x value.</param>
		/// <param name="y2">The second y value.</param>
		public bool Move(int x1, int y1, int x2, int y2){
			//Check valid openings
			if (Board [x1, y1] == null ||
				(Board [x2, y2] != null && !Board[x2,y2].Dead))
				return false;
			
			//Check valid action
			Character c1 = Board [x1, y1];
			if (c1.Moved || c1.Dead)
				return false;

			//Clear corpse if needed
			if(Board[x2,y2] != null && Board[x2,y2].Dead){
				Command.Execute (new Command (
					Command.TYPE.CLEARCORPSE,
					new string[] { x1.ToString (), y1.ToString () },
					Owner));
			}
			
			//Move
			Board[x2,y2] = Board[x1,y1];
			Board [x1, y1] = null;

			c1.Moved = true;
			//Successful
			return true;
		}

		/// <summary>
		/// Recruit the a Character from a Player's hand to x,y
		/// </summary>
		/// <param name="x">The x value</param>
		/// <param name="y">The y value.</param>
		/// <param name="handslot">The card index in the Player's hand</param>
		/// <param name="PLAYER">The Player</param>
		public bool Recruit(int x, int y, int handslot, Player PLAYER){
			//Get the Character from the Hand
			Character c = PLAYER.HAND.Get (handslot);
			if (c == null)
				return false;

			//Check for legal recruitment
			if (!ValidRecruit (c, x, y)) 
				return false;

			Character c2 = Board [x, y];
			//Clear a corpse if there is one there
			if (c2 != null) {
				if (c2.Dead) {
					Command.Execute (new Command (
						Command.TYPE.CLEARCORPSE,
						new string[] { x.ToString (),
							y.ToString () },
						PLAYER));
				}
			}
			//Place the Character in the board
			Board [x, y] = c;

			//Perform Post Recruitment effects
			PostRecruitEffects (c, x, y);

			//Remove the Character from the Player's hand
			PLAYER.HAND.Discard (c, PLAYER.GRAVEYARD);
			PLAYER.RecruitsRemaining--;
			return true;
		}

		/// <summary>
		/// Reset flags from Characters in this GameBoard
		/// </summary>
		public void ResetFlags(){
			for (int i = 0; i < ROWS; i++) {
				for (int a = 0; a < COLUMNS; a++) {
					if (Board [a, i] != null) {
						Board [a, i].Moved = false;
					}
				}
			}
		}

		/// <summary>
		/// Reset the stunned status for Characters in this GameBoard
		/// </summary>
		public void ResetStunned(){
			for (int i = 0; i < ROWS; i++) {
				for (int a = 0; a < COLUMNS; a++) {
					if (Board [a, i] != null) {
						Board [a, i].Stunned = false;
					}
				}
			}
		}

		/// <summary>
		/// Determine whether a row contains a Character
		/// </summary>
		/// <returns><c>true</c>, if the row contains the Character,
		/// <c>false</c> otherwise.</returns>
		/// <param name="c">A Character to look for</param>
		/// <param name="row">The row to search in</param>
		public bool RowContains(Character c, int row){
			for (int i = 0; i < ROWS; i++) {
				if (Board [i, row] == c)
					return true;
			}
			return false;
		}

		public bool ValidAttack(int x, int y){
			//If no one is in this space, return false
			if (Board [x, y] == null)
				return false;
			//Get the attacker
			Character attacker = Board [x, y];
			//Check status effects
			if (attacker == null || attacker.Dead || attacker.Stunned)
				return false;

			//Look for blocking characters if no ranged attack
			if (attacker.IsMelee && y == 1) {
				//Check if the spot isn't empty
				if (Board [x, 0] != null) {
					//If an alive unit is there melee is blocked
					if (!Board [x, 0].Dead)
						return false;
				}
			}

			//Check for 0 attack
			if (attacker.Attack == 0 && !attacker.ModifiedDamage)
				return false;

			//Valid attack
			return true;
		}

		/// <summary>
		/// Determine if a Move Command is valid or not
		/// Add a Trigger to the Pipeline if it is
		/// </summary>
		/// <returns><c>true</c>, if move was valid,
		///  <c>false</c> otherwise.</returns>
		/// <param name="x1">The first x value.</param>
		/// <param name="y1">The first y value.</param>
		/// <param name="x2">The second x value.</param>
		/// <param name="y2">The second y value.</param>
		/// <param name="forced">Whether the move is a forced move</param>
		public bool ValidMove(int x1, int y1, int x2, int y2, bool forced, bool addpipeline){
			//Check valid openings
			if (Board [x1, y1] == null ||
				(Board [x2, y2] != null && !Board[x2,y2].Dead))
				return false;

			if (Board [x1, y1].Rooted && !forced
				|| Board[x1, y1].Stunned && !forced)
				return false;

			//Check valid action
			Character c1 = Board [x1, y1];
			if (c1.Moved || c1.Dead)
				return false;

			//Add to Pipeline
			if(addpipeline)
				c1.CONTROLLER.TABLE.PIPELINE.Add (new TriggerPacket (Trigger.TYPE.MOVE,
					null, c1, null));
			return true;
		}

		public bool ValidMove(Character c, int x2, int y2, bool forced, bool addpipeline){
			Point p = LocateInBoard (c);
			if (p == null)
				return false;

			return ValidMove (p.x, p.y, x2, y2, forced, addpipeline);
		}

		//----------------------------------------------------------------

		/// <summary>
		/// Print the GameBoard
		/// </summary>
		/// <param name="flip">Whether or not to invert the board printing</param>
		public void Print(bool flip){
			PrintBoard (flip);
			PrintUnits ();
		}

		/// <summary>
		/// Prints the board.
		/// </summary>
		/// <param name="flip">Whether or not to invert the board</param>
		public void PrintBoard(bool flip){
			for (int i = 0; i < ROWS; i++) {
				for (int a = 0; a < COLUMNS; a++) {
					Console.Write ("[ ");
					int row = (flip) ? ROWS - i - 1 : i;
					if (Board [a, row] == null) {
						Console.Write (" ");
					} else {
						if (Board [a, row].Dead)
							Console.Write ("C");
						else
							Console.Write ("X");
					}
					Console.Write (" ]");
				}
				Console.WriteLine ();
			}
		}

		/// <summary>
		/// Prints the units in this GameBoard
		/// </summary>
		public void PrintUnits(){
			for (int i = 0; i < ROWS; i++) {
				for (int a = 0; a < COLUMNS; a++) {
					if (Board [a, i] != null) {
						Console.Write (a + ", " + i + ": ");
						Console.WriteLine (Board [a, i]);
					}
				}
			}
			Console.WriteLine ();
		}

		//----------------------------------------------------------------

		/// <summary>
		/// Calculates the armor of a Character
		/// </summary>
		/// <returns>The modified armor value</returns>
		/// <param name="x1">A column</param>
		/// <param name="y1">A row</param>
		private int CalculateArmor(int x1, int y1){
			Character c = Owner.GAMEBOARD.Board [x1, y1];
			int armor = c.BaseArmor;
			if (c == null)
				return armor;

			List<PassivePair> AP = Owner.GatherPassives ();
			List<PassivePair> EP = Owner.ENEMY.GatherPassives ();

			foreach (PassivePair pp in AP) {
				armor = pp.PASSIVE.ModifyArmor (armor, c, pp.CHARACTER, Owner);
			}
			foreach (PassivePair pp in EP) {
				armor = pp.PASSIVE.ModifyArmor (armor, c, pp.CHARACTER, Owner.ENEMY);
			}

			//clamp and return
			if (armor < 0)
				armor = 0;

			return armor;
		}

		/// <summary>
		/// Calculates the attack of a Character
		/// </summary>
		/// <returns>The modified attack power</returns>
		/// <param name="x1">A column</param>
		/// <param name="y1">A row</param>
		private int CalculateAttack(int x1, int y1){
			Character c = Owner.GAMEBOARD.Board [x1, y1];
			int damage = c.BaseAttack;
			if (c == null)
				return damage;

			List<PassivePair> AP = Owner.GatherPassives ();
			List<PassivePair> EP = Owner.ENEMY.GatherPassives ();

			foreach (PassivePair pp in AP) {
				damage = pp.PASSIVE.ModifyDamage (damage, c, pp.CHARACTER, Owner);
			}
			foreach (PassivePair pp in EP) {
				damage = pp.PASSIVE.ModifyDamage (damage, c, pp.CHARACTER, Owner.ENEMY);
			}

			//clamp and return
			if (damage < 0)
				damage = 0;

			return damage;
		}

		/// <summary>
		/// Calculates the attack type of a Character
		/// </summary>
		/// <returns>The modified attack type</returns>
		/// <param name="x1">A column</param>
		/// <param name="y1">A row</param>
		private bool CalculateAttackType(int x1, int y1){
			Character c = Owner.GAMEBOARD.Board[x1, y1];
			bool melee = c.BaseIsMelee;
			if (c == null)
				return melee;

			List<PassivePair> AP = Owner.GatherPassives ();
			List<PassivePair> EP = Owner.ENEMY.GatherPassives ();

			foreach (PassivePair pp in AP) {
				melee = pp.PASSIVE.ModifyAttackType (melee, c, pp.CHARACTER, Owner);
			}
			foreach (PassivePair pp in EP) {
				melee = pp.PASSIVE.ModifyAttackType (melee, c, pp.CHARACTER, Owner.ENEMY);
			}

			return melee;
		}

		/// <summary>
		/// Calculates the Intercept value of a Character
		/// </summary>
		/// <returns>The modified Intercept value</returns>
		/// <param name="x1">A column</param>
		/// <param name="y1">A row</param>
		private bool CalculateIntercept(int x1, int y1){
			Character c = Owner.GAMEBOARD.Board[x1, y1];
			bool intercept = c.BaseIntercept;
			if (c == null)
				return intercept;

			List<PassivePair> AP = Owner.GatherPassives ();
			List<PassivePair> EP = Owner.ENEMY.GatherPassives ();

			foreach (PassivePair pp in AP) {
				intercept = pp.PASSIVE.ModifyIntercept (intercept, c, pp.CHARACTER, Owner);
			}
			foreach (PassivePair pp in EP) {
				intercept = pp.PASSIVE.ModifyIntercept (intercept, c, pp.CHARACTER, Owner.ENEMY);
			}

			return intercept;
		}

		/// <summary>
		/// Calculates the life of a Character
		/// </summary>
		/// <returns>The modified life</returns>
		/// <param name="x1">A column</param>
		/// <param name="y1">A row</param>
		private int CalculateLife(int x1, int y1){
			Character c = Owner.GAMEBOARD.Board[x1, y1];
			int life = c.BaseLife;
			if (c == null)
				return life;

			List<PassivePair> AP = Owner.GatherPassives ();
			List<PassivePair> EP = Owner.ENEMY.GatherPassives ();

			foreach (PassivePair pp in AP) {
				life = pp.PASSIVE.ModifyLife (life, c, pp.CHARACTER, Owner);
			}
			foreach (PassivePair pp in EP) {
				life = pp.PASSIVE.ModifyLife (life, c, pp.CHARACTER, Owner.ENEMY);
			}

			//clamp and return
			if (life < 0)
				life = 0;

			return life;
		}

		/// <summary>
		/// Calculates the max size of the owner's hand
		/// </summary>
		/// <returns>The max hand size.</returns>
		private int CalculateMaxHandSize(){
			int handsize = Owner.BaseMaxHandSize;

			List<PassivePair> AP = Owner.GatherPassives ();
			List<PassivePair> EP = Owner.ENEMY.GatherPassives ();

			foreach (PassivePair pp in AP) {
				handsize = pp.PASSIVE.ModifyMaxHandSize (handsize, pp.CHARACTER, Owner);
			}
			foreach (PassivePair pp in EP) {
				handsize = pp.PASSIVE.ModifyMaxHandSize (handsize, pp.CHARACTER, Owner);
			}

			return handsize;
		}

		/// <summary>
		/// Calculates the Overkill value of a Character
		/// </summary>
		/// <returns>The modified Overkill value</returns>
		/// <param name="x1">A column</param>
		/// <param name="y1">A row</param>
		private bool CalculateOverkill(int x1, int y1){
			Character c = Owner.GAMEBOARD.Board[x1, y1];
			bool overkill = false;
			if (c == null)
				return overkill;

			List<PassivePair> AP = Owner.GatherPassives ();
			List<PassivePair> EP = Owner.ENEMY.GatherPassives ();

			foreach (PassivePair pp in AP) {
				overkill = pp.PASSIVE.ModifyOverkill (overkill, c, pp.CHARACTER, Owner);
			}
			foreach (PassivePair pp in EP) {
				overkill = pp.PASSIVE.ModifyOverkill (overkill, c, pp.CHARACTER, Owner.ENEMY);
			}

			return overkill;
		}

		/// <summary>
		/// Calculates if the Owner should be targetable
		/// </summary>
		/// <returns>Whether or not the owner is targetable</returns>
		private bool CalculatePlayerTargetable(){
			bool reveal = true;

			List<PassivePair> AP = Owner.GatherPassives ();
			List<PassivePair> EP = Owner.ENEMY.GatherPassives ();

			foreach (PassivePair pp in AP) {
				reveal = pp.PASSIVE.ModifyPlayerTargetable (reveal, pp.CHARACTER, Owner);
			}
			foreach (PassivePair pp in EP) {
				reveal = pp.PASSIVE.ModifyPlayerTargetable (reveal, pp.CHARACTER, Owner.ENEMY);
			}

			return reveal;
		}

		/// <summary>
		/// Calculates if the Owner should reveal his Hand
		/// </summary>
		/// <returns>Whether or not the owner must reveal his Hand</returns>
		private bool CalculateRevealHand(){
			bool reveal = false;

			List<PassivePair> AP = Owner.GatherPassives ();
			List<PassivePair> EP = Owner.ENEMY.GatherPassives ();

			foreach (PassivePair pp in AP) {
				reveal = pp.PASSIVE.RevealYourHand (reveal, pp.CHARACTER, Owner);
			}
			foreach (PassivePair pp in EP) {
				reveal = pp.PASSIVE.RevealEnemyHand (reveal, pp.CHARACTER, Owner.ENEMY);
			}

			return reveal;
		}

		/// <summary>
		/// Calculates the Rooted value of a Character
		/// </summary>
		/// <returns>The modified Rooted value</returns>
		/// <param name="x1">A column</param>
		/// <param name="y1">A row</param>
		private bool CalculateRooted(int x1, int y1){
			Character c = Owner.GAMEBOARD.Board[x1, y1];
			bool intercept = false;
			if (c == null)
				return intercept;

			List<PassivePair> AP = Owner.GatherPassives ();
			List<PassivePair> EP = Owner.ENEMY.GatherPassives ();

			foreach (PassivePair pp in AP) {
				intercept = pp.PASSIVE.ModifyRooted (intercept, c, pp.CHARACTER, Owner);
			}
			foreach (PassivePair pp in EP) {
				intercept = pp.PASSIVE.ModifyRooted (intercept, c, pp.CHARACTER, Owner.ENEMY);
			}

			return intercept;
		}

		/// <summary>
		/// Calculates the Targetable value of a Character
		/// </summary>
		/// <returns>The modified Targetable value</returns>
		/// <param name="x1">A column</param>
		/// <param name="y1">A row</param>
		private bool CalculateTargetable(int x1, int y1){
			Character c = Owner.GAMEBOARD.Board[x1, y1];
			bool intercept = true;
			if (c == null)
				return intercept;

			List<PassivePair> AP = Owner.GatherPassives ();
			List<PassivePair> EP = Owner.ENEMY.GatherPassives ();

			foreach (PassivePair pp in AP) {
				intercept = pp.PASSIVE.ModifyTargetable (intercept, c, pp.CHARACTER, Owner);
			}
			foreach (PassivePair pp in EP) {
				intercept = pp.PASSIVE.ModifyTargetable (intercept, c, pp.CHARACTER, Owner.ENEMY);
			}

			return intercept;
		}

		/// <summary>
		/// Calculates the Zombie value of a Character
		/// </summary>
		/// <returns>The modified zmobie value</returns>
		/// <param name="x1">A column</param>
		/// <param name="y1">A row</param>
		private bool CalculateZombie(int x1, int y1){
			Character c = Owner.GAMEBOARD.Board[x1, y1];
			bool zombie = c.BaseZombie;
			if (c == null)
				return zombie;

			List<PassivePair> AP = Owner.GatherPassives ();
			List<PassivePair> EP = Owner.ENEMY.GatherPassives ();

			foreach (PassivePair pp in AP) {
				zombie = pp.PASSIVE.ModifyZombie (zombie, c, pp.CHARACTER, Owner);
			}
			foreach (PassivePair pp in EP) {
				zombie = pp.PASSIVE.ModifyZombie (zombie, c, pp.CHARACTER, Owner.ENEMY);
			}

			return zombie;
		}

		/// <summary>
		/// Finds an overkill target.
		/// </summary>
		/// <returns>The overkill target.</returns>
		/// <param name="target">The target that just died</param>
		private Character FindOverkillTarget(Character target){
			Point P = LocateInBoard (target);
			int x = P.x; int y = P.y + 1;
			if (y == ROWS)
				return null;
			return Board [x, y];
		}

		/// <summary>
		/// Creates a Damage object with the specified Parameters
		/// </summary>
		/// <returns>The damage attacker gives to the target</returns>
		/// <param name="type">A damage type</param>
		/// <param name="value">A damage value</param>
		/// <param name="attacker">The attacker</param>
		/// <param name="target">The target</param>
		private Damage GetDamage(Damage.TYPE type, int value, Character attacker,
			Character target){

			//Create a base damage object
			Damage damage = new Damage (type, value, attacker);
			//Modify the damage via passives
			damage = CalculateModifyDamageToX (damage, attacker, target);
			//Return
			return damage;

		}

		/// <summary>
		/// Performs Post Recruitment effects
		/// Handles Upgrade and Gravedigger
		/// </summary>
		/// <param name="c">A Character</param>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		private void PostRecruitEffects(Character c, int x, int y){
			//If the Character is a Gravedigger
			if (c.Gravedigger) {
				int p = c.GravediggerPlus;
				while (p > 0) { //Siphon Corposes
					Point P = RandomCorpse();
					Command.Execute (new Command (
						Command.TYPE.CLEARCORPSE,
						new string[] { P.x.ToString (), P.y.ToString () },
						Owner));
					p--;
				}
			}
			//If the Character is an Upgrade
			if (c.Upgrade) {
				int p = c.UpgradePlus;
				while (p > 0) { //Siphon Units
					Point P = RandomUnit();
					if (P.x == x && P.y == y)
						continue;
					//remove unit
					Owner.GRAVEYARD.AddCard(Board[P.x,P.y], 999, null);
					Board [P.x, P.y] = null;
					p--;
				}
			}
		}

		/// <summary>
		/// Finds a Random Corpse
		/// </summary>
		/// <returns>A Random Corpse</returns>
		private Point RandomCorpse(){
			List<Point> corpses = new List<Point> ();
			for (int i = 0; i < ROWS; i++) {
				for (int a = 0; a < COLUMNS; a++) {
					if (Board [a, i] != null && Board [a, i].Dead)
						corpses.Add (new Point(a, i));
				}
			}
			if (corpses.Count == 0)
				return null;
			return corpses [Utility.R.Next (corpses.Count)];
		}

		/// <summary>
		/// Finds a Random Unit
		/// </summary>
		/// <returns>A Random Unit</returns>
		private Point RandomUnit(){
			List<Point> units = new List<Point> ();
			for (int i = 0; i < ROWS; i++) {
				for (int a = 0; a < COLUMNS; a++) {
					if (Board [a, i] != null && !Board [a, i].Dead)
						units.Add (new Point(a, i));
				}
			}
			if (units.Count == 0)
				return null;
			return units [Utility.R.Next (units.Count)];
		}

		/// <summary>
		/// Determines whether a recruitment is valid
		/// Bases this on whether or not the Character is an Upgrade
		/// or a Gravedigger, and what is on the GameBoard current
		/// </summary>
		/// <returns><c>true</c>, if recruit was valid, 
		/// <c>false</c> otherwise.</returns>
		/// <param name="c">A character</param>
		/// <param name="x">The column to recruit to</param>
		/// <param name="y">The row to recruit to</param>
		public bool ValidRecruit(Character c, int x, int y){
			if (Owner.RecruitsRemaining == 0)
				return false;
			if (x >= TableTop.COLUMNS || x < 0)
				return false;
			if (y >= TableTop.ROWS || y < 0)
				return false;

			Character c2 = Board [x, y];
			if (c2 == null) { //Nothing in the slot
				if (c.Upgrade || c.Gravedigger) //upgrade or graved
					return false;
				return true; //regular unit
			} else { //Something there
				if (c2.Dead) { //Corpse there
					if (c.Upgrade) //upgrade
						return false; //regular unit
					if (c.Gravedigger) { //Gravedigger
						return NumberOfCorpses >= c.GravediggerPlus+1;
					}
					return true; //regular unit
				} else { //Alive unit there
					if (c.Upgrade) //upgrade
						return NumberOfUnits >= c.UpgradePlus+1;
					return false; //regular unit / gravedigger
				}
			}
		}

		public bool ValidHandAbility(Character c, int x){
			if (Owner.TRAPS [x] != null)
				return false;
			if (c.HandAbility == null)
				return false;
			if (c.HandAbility.GetType ().IsSubclassOf (typeof(Active)) &&
			   ((Active)c.HandAbility).RequestsTarget)
				return false;
			return true;
		}

	} // End GameBoard class

} // End namespace

