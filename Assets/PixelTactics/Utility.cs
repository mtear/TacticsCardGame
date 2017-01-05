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
* Command.cs
* Author: Nic Wilson
* Last updated: 3/28/2016
*/

using System;
using System.Collections.Generic;

namespace Tactics_CoreGameEngine
{
	/// <summary>
	/// A Utility class of static functions and variables
	/// </summary>
	public class Utility
	{

		//----------------------------------------------------------------

		/// <summary>
		/// A common variable for generating Random numbers
		/// </summary>
		public static Random R = new Random();

		//----------------------------------------------------------------

		/// <summary>
		/// Gets a debug starter deck.
		/// </summary>
		/// <returns>The debug starter deck.</returns>
		/// <param name="P">A Player to mark as the cards' Controller</param>
		public static List<Character> GetDebugStarterDeck(Player P){
			List<Character> deck = new List<Character> ();
			deck.Add (new UC20 (P));
			deck.Add (new UC24 (P));
			deck.Add (new UC12 (P));
			deck.Add (new UC12 (P));
			deck.Add (new UC3 (P));
			deck.Add (new UC3 (P));
			deck.Add (new UC4 (P));
			deck.Add (new UC28 (P));
			deck.Add (new UC9 (P));
			deck.Add (new UC29 (P));
			deck.Add (new UC10 (P));
			deck.Add (new UC25 (P));
			deck.Add (new UC11 (P));
			deck.Add (new UC14 (P));
			deck.Add (new UC26 (P));
			deck.Add (new UC19 (P));
			deck.Add (new UC27 (P));
			deck.Add (new UC31 (P));
			deck.Add (new UC1 (P));
			return deck;
		}


	} // End Utility class

	//-----------------------------------------------------------------------------

	/// <summary>
	/// A class representing an x,y Point
	/// </summary>
	public class Point{

		//----------------------------------------------------------------

		/// <summary>
		/// The x and y parameters
		/// </summary>
		public int x,y;

		//----------------------------------------------------------------

		/// <summary>
		/// Initializes a new instance of the 
		/// <see cref="Tactics_CoreGameEngine.Point"/> class.
		/// </summary>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		public Point(int x, int y){
			this.x = x;
			this.y = y;
		}

	} // End Point class

} // End namespace

