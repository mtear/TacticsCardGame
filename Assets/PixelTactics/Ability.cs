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
 * Ability.cs
 * Author: Nic Wilson
 * Last updated: 3/28/2016
 */

using System;
using System.Collections.Generic;

namespace Tactics_CoreGameEngine
{
	/// <summary>
	/// A class representing a character Ability
	/// </summary>
	public class Ability
	{

		//----------------------------------------------------------------

		/// <summary>
		/// A flag for if this ability is still valid once it executes.
		/// Mostly used for trap and order countering.
		/// </summary>
		public bool VALID = true;
	
		private string name;
		/// <summary>
		/// The name code of the Ability.
		/// </summary>
		/// <value>The name.</value>
		public String NameCode{
			get{
				return name;
			}
		}
		private string description;
		/// <summary>
		/// Gets the description code.
		/// </summary>
		/// <value>The description code.</value>
		public String DescriptionCode {
			get {
				return description;
			}
		}

		//----------------------------------------------------------------

		/// <summary>
		/// Initializes a new instance of the 
		/// <see cref="PixelTactics.Ability"/> class.
		/// </summary>
		/// <param name="name">The name of the ability</param>
		/// <param name="description">A description code for the 
		/// ability</param>
		public Ability (string name, string description)
		{
			this.name = name;
			this.description = description;
		}

		//----------------------------------------------------------------

		/// <summary>
		/// Returns the possible Ability objects this ability can create.
		/// </summary>
		/// <returns>The possible creatable Abilities</returns>
		public virtual List<Ability> CreatableAbilities(){
			return new List<Ability> ();
		}

		/// <summary>
		/// Returns the possible Character objects this ability can create.
		/// </summary>
		/// <returns>The possible creatable Characters</returns>
		public virtual List<Character> CreatableCharacters(){
			return new List<Character> ();
		}

		/// <summary>
		/// Gets the description for the currenet language StringTable.
		/// </summary>
		/// <returns>The description.</returns>
		/// <param name="P">A Player to reference for the TableTop</param>
		public String GetDescription(Player P){
			return P.TABLE.STRINGTABLE.Get (description);
		}

		/// <summary>
		/// Gets the name of this Ability
		/// </summary>
		/// <returns>The name.</returns>
		/// <param name="P">A Player to reference</param>
		public String GetName(Player P){
			return P.TABLE.STRINGTABLE.Get (name);
		}

	} // End Ability class

} // End namespace

