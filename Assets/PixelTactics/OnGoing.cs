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
* OnGoing.cs
* Author: Nic Wilson
* Last updated: 3/28/2016
*/

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tactics_CoreGameEngine
{
	/// <summary>
	/// Represents and OnGoing Ability that lasts for several turns
	/// </summary>
	public abstract class OnGoing : Ability
	{

		//----------------------------------------------------------------

		/// <summary>
		/// Gets a value indicating whether this
		///  <see cref="Tactics_CoreGameEngine.OnGoing"/> is expired.
		/// </summary>
		/// <value><c>true</c> if expired; otherwise, <c>false</c>.</value>
		public bool Expired {
			get {
				return Timer <= 0;
			}
		}

		//----------------------------------------------------------------

		/// <summary>
		/// The timer for how many lasting turns are remaining
		/// </summary>
		public int Timer = 0;

		/// <summary>
		/// A list of the triggers this card adds
		/// </summary>
		public List<Trigger> Triggers;

		/// <summary>
		/// A list of the passives this card adds
		/// </summary>
		public List<Passive> Passives;

		//----------------------------------------------------------------

		/// <summary>
		/// Initializes a new instance of the 
		/// <see cref="Tactics_CoreGameEngine.OnGoing"/> class.
		/// </summary>
		/// <param name="name">Ability name</param>
		/// <param name="description">Description.</param>
		/// <param name="Timer">How many turns this lasts for</param>
		public OnGoing (string name, string description, int Timer) : base (name, description)
		{
			this.Timer = Timer;
			this.Triggers = new List<Trigger> ();
			this.Passives = new List<Passive> ();
		}

		//----------------------------------------------------------------

		/// <summary>
		/// Decrement this instance's turn timer
		/// </summary>
		public void Decrement(){
			Timer--;
		}

		/// <summary>
		/// Effects that occur when the OnGoing card is discarded
		/// </summary>
		/// <param name="P">A reference Player</param>
		public abstract void Discard (Player P);

	} // End OnGoing class

} // End namespace

