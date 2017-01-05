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
* Damage.cs
* Author: Nic Wilson
* Last updated: 3/27/2016
*/

using System;

namespace Tactics_CoreGameEngine
{
	/// <summary>
	/// A class representing a Damage packet
	/// </summary>
	public class Damage
	{

		//----------------------------------------------------------------

		/// <summary>
		/// An enum representing the types of Damage that can be applied
		/// </summary>
		public enum TYPE{
			MELEE, RANGE, SPELL, HEAL, STUN
		}

		//----------------------------------------------------------------

		/// <summary>
		/// The Character that is the source of the Damage
		/// </summary>
		public Character Source;

		/// <summary>
		/// The Type of Damage this packet is delivering
		/// </summary>
		public TYPE Type;

		/// <summary>
		/// The Value of the Damage
		/// </summary>
		public int Value;

		//----------------------------------------------------------------

		/// <summary>
		/// Initializes a new instance of the 
		/// <see cref="Tactics_CoreGameEngine.Damage"/> class.
		/// </summary>
		/// <param name="Type">Type.</param>
		/// <param name="Value">Value.</param>
		/// <param name="Source">Source.</param>
		public Damage (Damage.TYPE Type, int Value, Character Source)
		{
			this.Type = Type;
			this.Value = Value;
			this.Source = Source;
		}

	} // End Damage class

} // End namespace

