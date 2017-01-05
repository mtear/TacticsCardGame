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
* Deck.cs
* Author: Nic Wilson
* Last updated: 3/27/2016
*/

using System;
using System.Collections.Generic;

namespace Tactics_CoreGameEngine
{
	/// <summary>
	/// A class representing a Player's deck of cards
	/// </summary>
	public class Deck
	{

		//----------------------------------------------------------------

		/// <summary>
		/// Get the contents of the Deck as a list of cards
		/// </summary>
		/// <value>The deck contents</value>
		public List<Character> Contents{
			get{
				return deck;
			}
		}

		/// <summary>
		/// Returns how many cards are in the deck
		/// </summary>
		/// <value>How many cards are in the deck</value>
		public int Count {
			get {
				return deck.Count;
			}
		}

		//----------------------------------------------------------------

		/// <summary>
		/// The List of cards, the deck
		/// </summary>
		private List<Character> deck;

		//----------------------------------------------------------------

		/// <summary>
		/// Initializes a new instance of the 
		/// <see cref="Tactics_CoreGameEngine.Deck"/> class.
		/// </summary>
		/// <param name="deck">The list of cards to populate
		/// the deck with</param>
		public Deck (List<Character> deck)
		{
			this.deck = deck;
			//Shuffle after setting up
			Shuffle ();
		}

		//----------------------------------------------------------------

		/// <summary>
		/// Draw a card from this deck
		/// </summary>
		/// <value>A card from this deck
		/// null if there are no cards left</value>
		public Character Draw(){
			//Return null if no cards are in the deck
			if (deck.Count == 0)
				return null;
			else { //Otherwise return the top card
				Character c = deck [0];
				deck.RemoveAt (0);
				return c;
			}
		}

		/// <summary>
		/// Shuffle the Deck
		/// </summary>
		public void Shuffle(){
			List<Character> temp = new List<Character> ();
			while (deck.Count > 0) {
				int i = Utility.R.Next (deck.Count);
				temp.Add (deck [i]);
				deck.RemoveAt (i);
			}
			deck = temp;
		}
			
	} // End Deck class

} // End namespace

