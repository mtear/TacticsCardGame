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
* Passive.cs
* Author: Nic Wilson
* Last updated: 3/28/2016
*/

using System;

namespace Tactics_CoreGameEngine
{

	/// <summary>
	/// A class representing a Passive ability that occurs staticly
	/// </summary>
	public abstract class Passive : Ability
	{

		//----------------------------------------------------------------

		/// <summary>
		/// The application priority of this Passive
		/// </summary>
		public int Priority = 0;

		//----------------------------------------------------------------

		/// <summary>
		/// Initializes a new instance of the 
		/// <see cref="Tactics_CoreGameEngine.Passive"/> class.
		/// </summary>
		/// <param name="name">The name</param>
		/// <param name="description">The Description.</param>
		/// <param name="priority">The Application Priority.</param>
		public Passive (string description, int priority)
			: base("", description)
		{
			this.Priority = priority;
		}

		//----------------------------------------------------------------

		/// <summary>
		/// Modifies the damage.
		/// </summary>
		/// <returns>The damage.</returns>
		/// <param name="damage">Damage.</param>
		/// <param name="target">Target.</param>
		/// <param name="user">User.</param>
		/// <param name="PLAYER">The reference Player.</param>
		public abstract int ModifyDamage (int damage, Character target,
			Character user, Player PLAYER);

		/// <summary>
		/// Modifies the life.
		/// </summary>
		/// <returns>The life.</returns>
		/// <param name="life">Life.</param>
		/// <param name="target">Target.</param>
		/// <param name="user">User.</param>
		/// <param name="PLAYER">The reference Player.</param>
		public abstract int ModifyLife (int life, Character target,
			Character user, Player PLAYER);

		/// <summary>
		/// Modifies the type of the attack.
		/// </summary>
		/// <returns>Your modified attack type</returns>
		/// <param name="melee">If set to <c>true</c> melee.</param>
		/// <param name="target">Target.</param>
		/// <param name="user">User.</param>
		/// <param name="PLAYER">The reference Player.</param>
		public abstract bool ModifyAttackType (bool melee, Character target,
			Character user, Player PLAYER);

		/// <summary>
		/// Calculates if you should reveal your Hand
		/// </summary>
		/// <returns><c>true</c>, if your hand was revealed,
		///  <c>false</c> otherwise.</returns>
		/// <param name="reveal">If set to <c>true</c> reveal.</param>
		/// <param name="user">User.</param>
		/// <param name="PLAYER">The reference Player.</param>
		public abstract bool RevealYourHand (bool reveal, Character user,
			Player PLAYER);

		/// <summary>
		/// Reveals the enemy hand.
		/// </summary>
		/// <returns><c>true</c>, if enemy hand was revealed,
		///  <c>false</c> otherwise.</returns>
		/// <param name="reveal">If set to <c>true</c> reveal.</param>
		/// <param name="user">User.</param>
		/// <param name="PLAYER">The reference Player.</param>
		public abstract bool RevealEnemyHand (bool reveal, Character user,
			Player PLAYER);

		/// <summary>
		/// Modifies the intercept value
		/// </summary>
		/// <returns><c>true</c>, if intercept was modifyed,
		///  <c>false</c> otherwise.</returns>
		/// <param name="intercept">Base intercept value</param>
		/// <param name="target">Target.</param>
		/// <param name="user">User.</param>
		/// <param name="PLAYER">The reference Player.</param>
		public abstract bool ModifyIntercept (bool intercept, Character target,
			Character user, Player PLAYER);

		/// <summary>
		/// Modifies the rooted value
		/// </summary>
		/// <returns><c>true</c>, if rooted was modifyed,
		///  <c>false</c> otherwise.</returns>
		/// <param name="rooted">Base rooted value</param>
		/// <param name="target">Target.</param>
		/// <param name="user">User.</param>
		/// <param name="PLAYER">The reference Player.</param>
		public abstract bool ModifyRooted (bool rooted, Character target,
			Character user, Player PLAYER);

		/// <summary>
		/// Modifies the overkill value
		/// </summary>
		/// <returns><c>true</c>, if overkill was applied,
		///  <c>false</c> otherwise.</returns>
		/// <param name="overkill">Base overkill value</param>
		/// <param name="target">Target.</param>
		/// <param name="user">User.</param>
		/// <param name="PLAYER">The reference Player.</param>
		public abstract bool ModifyOverkill (bool overkill, Character target,
			Character user, Player PLAYER);

		/// <summary>
		/// Modifies the armor value
		/// </summary>
		/// <returns>The armor.</returns>
		/// <param name="armor">Armor.</param>
		/// <param name="target">Target.</param>
		/// <param name="user">User.</param>
		/// <param name="PLAYER">The reference Player.</param>
		public abstract int ModifyArmor (int armor, Character target,
			Character user, Player PLAYER);

		/// <summary>
		/// Modifies the zombie value
		/// </summary>
		/// <returns><c>true</c>, if zombie was applied,
		///  <c>false</c> otherwise.</returns>
		/// <param name="zombie">Base zombie value</param>
		/// <param name="target">Target.</param>
		/// <param name="user">User.</param>
		/// <param name="PLAYER">The reference Player.</param>
		public abstract bool ModifyZombie (bool zombie, Character target,
			Character user, Player PLAYER);

		/// <summary>
		/// Modifies the damage to certain types
		/// </summary>
		/// <returns>The damage to x type</returns>
		/// <param name="damage">Damage.</param>
		/// <param name="user">User.</param>
		/// <param name="attacker">Attacker.</param>
		/// <param name="defender">Defender.</param>
		public abstract int ModifyDamageToX (int damage, Character user,
			Character attacker, Character defender);

		/// <summary>
		/// Modifies the max hand size
		/// </summary>
		/// <returns>The max hand size.</returns>
		/// <param name="handsize">Handsize.</param>
		/// <param name="user">User.</param>
		/// <param name="PLAYER">The reference Player.</param>
		public abstract int ModifyMaxHandSize (int handsize, Character user,
			Player PLAYER);

		/// <summary>
		/// Modifies if the Character is targetable or not
		/// </summary>
		/// <returns><c>true</c>, if targetable was modifyed,
		///  <c>false</c> otherwise.</returns>
		/// <param name="targetable">Base targetable value</param>
		/// <param name="target">Target.</param>
		/// <param name="user">User.</param>
		/// <param name="PLAYER">The reference Player.</param>
		public abstract bool ModifyTargetable (bool targetable, Character target,
			Character user, Player PLAYER);

		/// <summary>
		/// Modifies the player's targetable value
		/// </summary>
		/// <returns><c>true</c>, if player targetable was modifyed,
		///  <c>false</c> otherwise.</returns>
		/// <param name="targetable">Base targetable value</param>
		/// <param name="user">User.</param>
		/// <param name="PLAYER">The reference Player.</param>
		public abstract bool ModifyPlayerTargetable (bool targetable,
			Character user, Player PLAYER);

	} // End Passive class

	//-----------------------------------------------------------------------------


	/// <summary>
	/// A pair of a Passive and the character that carries it
	/// </summary>
	public class PassivePair{

		//----------------------------------------------------------------

		/// <summary>
		/// The Passive
		/// </summary>
		public Passive PASSIVE;

		/// <summary>
		/// The Character
		/// </summary>
		public Character CHARACTER;

		//----------------------------------------------------------------

		/// <summary>
		/// Initializes a new instance of the 
		/// <see cref="Tactics_CoreGameEngine.PassivePair"/> class.
		/// </summary>
		/// <param name="p">A Passive</param>
		/// <param name="c">C.</param>
		public PassivePair(Passive p, Character c){
			this.PASSIVE = p;
			this.CHARACTER = c;
		}

	} // End PassivePair class

} // End namespace

