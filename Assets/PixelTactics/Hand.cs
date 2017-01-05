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
* Hand.cs
* Author: Nic Wilson
* Last updated: 3/28/2016
*/

using System;
using System.Collections.Generic;

namespace Tactics_CoreGameEngine
{
	/// <summary>
	/// A class representing a Player's Hand
	/// </summary>
	public class Hand
	{

		//----------------------------------------------------------------

		/// <summary>
		/// Gets the cards in the Hand
		/// </summary>
		/// <value>The cards in this Hand</value>
		public List<Character> Cards{
			get{
				return hand;
			}
		}

		/// <summary>
		/// Gets the number of cards in this Hand
		/// </summary>
		/// <value>The number of cards in this Hand</value>
		public int Count{
			get{
				return hand.Count;
			}
		}

		//----------------------------------------------------------------

		/// <summary>
		/// The list of cards in this Hand
		/// </summary>
		private List<Character> hand;

		//----------------------------------------------------------------

		/// <summary>
		/// Initializes a new instance of the 
		/// <see cref="Tactics_CoreGameEngine.Hand"/> class.
		/// </summary>
		public Hand ()
		{
			hand = new List<Character> ();
		}

		//----------------------------------------------------------------

		/// <summary>
		/// Adds a card to this Hand
		/// </summary>
		/// <param name="c">Card to add</param>
		/// <param name="MaxHandSize">The maximum hand size for this Hand</param> 
		public void AddCard(Character c, int MaxHandSize, Hand DiscardPile){
			//Don't allow null cards
			if (c == null)
				return;
			
			//Discard the card if there are too many cards in this hand
			if (hand.Count >= MaxHandSize) {
				DiscardPile.AddCard (c, 999, null);
				c.CONTROLLER.TABLE.PIPELINE.Add (
					new TriggerPacket (
						Trigger.TYPE.HANDTOOFULL,
						c.CONTROLLER,
						c,
						c.CONTROLLER));
			} else
				hand.Add (c);
		}

		/// <summary>
		/// Discard the specified card into the specified Hand
		/// Used for discarding into a discard pile
		/// </summary>
		/// <param name="c">The card to discard</param>
		/// <param name="d">The Hand to place the card into</param>
		public void Discard(Character c, Hand d){
			//Don't allow null entries
			if (c == null)
				return;
			
			//Reset any stat increases on the card
			c.ResetBaseStats ();
			//Remove the card from the Hand
			bool removed = hand.Remove (c);
			//Add the card into the other Hand (discard pile)
			d.AddCard (c, 999, null);

			//Raise a trigger event if a card was removed
			if (removed) {
				c.CONTROLLER.TABLE.PIPELINE.Add (
					new TriggerPacket (
						Trigger.TYPE.DISCARDCARD,
						c.CONTROLLER,
						c,
						c.CONTROLLER));
			}
		}

		/// <summary>
		/// Discard the specified card
		/// </summary>
		/// <param name="c">Card to discard</param>
		public void Discard(Character c){
			//Reset any stat increases on the card
			c.ResetBaseStats ();
			//Remove the card
			bool removed = hand.Remove (c);

			//Raise a trigger event if a card was removed
			if (removed) {
				c.CONTROLLER.TABLE.PIPELINE.Add (
					new TriggerPacket (
						Trigger.TYPE.DISCARDCARD,
						c.CONTROLLER,
						c,
						c.CONTROLLER));
			}
		}

		/// <summary>
		/// Discard a Random card into the specified discard pile
		/// </summary>
		/// <param name="graveyard">The Graveyard to discard to</param>
		public void DiscardRandomCard(Hand graveyard){
			Character c = GetRandomCard ();
			if (c != null)
				Discard (c, graveyard);
		}

		/// <summary>
		/// Draw a card from the specified Player's deck
		/// </summary>
		/// <param name="PLAYER">The Player to draw from</param>
		public bool Draw(Player PLAYER){
			//Get a card from the deck
			Character c = PLAYER.DECK.Draw ();
			//The Player loses if they couldn't draw a card
			if (c == null) {
				PLAYER.LoseFromDrawOut ();
			}

			//Discard the card if there are too many cards in this hand
			if (PLAYER.HAND.Cards.Count >= PLAYER.MaxHandSize) {
				PLAYER.GRAVEYARD.AddCard (c, 999, null);
				c.CONTROLLER.TABLE.PIPELINE.Add (
					new TriggerPacket (
						Trigger.TYPE.HANDTOOFULL,
						c.CONTROLLER,
						c,
						c.CONTROLLER));
			}else hand.Add(c);
			return true;
		}

		/// <summary>
		/// Get the Card at the specified index
		/// </summary>
		/// <param name="index">Index</param>
		public Character Get(int index){
			return hand [index];
		}

		/// <summary>
		/// Gets a random card from this Hand
		/// </summary>
		/// <returns>A random card</returns>
		public Character GetRandomCard(){
			if (hand.Count == 0)
				return null;
			return hand [Utility.R.Next (hand.Count)];
		}

		/// <summary>
		/// Remove the specified Character from this Hand
		/// </summary>
		/// <param name="c">The Character to remove</param>
		public void Remove(Character c){
			hand.Remove (c);
		}

	} // End Hand class

} // End namespace

