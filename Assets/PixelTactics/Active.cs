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
 * Active.cs
 * Author: Nic Wilson
 * Last updated: 3/26/2016
 */

using System;
using System.Collections.Generic;

namespace Tactics_CoreGameEngine
{
	/// <summary>
	/// An Active ability that can be a Hand ability
	/// </summary>
	public abstract class Active : Ability
	{

		//----------------------------------------------------------------

		/// <summary>
		/// Indicates if this Active ability requests a target
		/// </summary>
		public bool RequestsTarget = false;

		/// <summary>
		/// The Target for this ability if it requests one
		/// </summary>
		public Target TARGET;

		//----------------------------------------------------------------

		/// <summary>
		/// Initializes a new instance of the <see cref="PixelTactics.Active"/> class.
		/// The base constructor doesn't need to do any action as the base class 
		/// handles the heavy lifting.
		/// </summary>
		/// <param name="name">Name.</param>
		/// <param name="description">Description.</param>
		public Active (string name, string description) : base(name, description)
		{
		}

		//----------------------------------------------------------------

		/// <summary>
		/// Execute the Active from the target Character and PLAYER.
		/// Intended to be implemented by subclasses
		/// </summary>
		/// <param name="target">The Character card the Active is coming from.</param>
		/// <param name="PLAYER">The reference Player</param>
		public abstract void Execute(Character target, Player PLAYER);

		/// <summary>
		/// Exports the effect to the game Effect Pipeline.
		/// </summary>
		/// <param name="TP">A TriggerPacket to export this with</param>
		/// <param name="PIPELINE">The Effect Pipeline.</param>
		public void ExportEffectToPipeline(TriggerPacket TP, List<TriggerPair> PIPELINE){
			PIPELINE.Add (new TriggerPair (TP, null));
		}
			
		public void GetTarget(Player P, Character c){
			//If this ability doesn't need a target,
			//	no need to get one
			if (!RequestsTarget) {
				P.ActiveContinue (true, c, null);
				return;
			}
			
			//Get the target from the Command Interface
			P.COMMANDINTERFACE.GetTarget (c);
		}

		public bool TargetContinue(){
			//If the target isn't good return no Target acquired
			if (TARGET == null || !ValidTarget(TARGET))
				return false;
			//If we're targeting a creature and it's not targetable return false
			if (!TARGET.TARGETPLAYER && (TARGET.CTARGET == null || !TARGET.CTARGET.Targetable))
				return false;
			//If we're targeting a player and it's a bad target
			if (TARGET.TARGETPLAYER && TARGET.PTARGET == null)
				return false;
			//If we're targeting a player and he's not targetable
			if (TARGET.TARGETPLAYER && !TARGET.PTARGET.Targetable)
				return false;

			//Otherwise a valid target was acquired
			return true;
		}

		//----------------------------------------------------------------

		/// <summary>
		/// Determines if a Target is a valid Target for this Active
		/// Intended to be overridden by subclasses
		/// </summary>
		/// <returns><c>true</c>, if target was valid, <c>false</c> otherwise.</returns>
		/// <param name="TARGET">The Target</param>
		protected virtual bool ValidTarget(Target TARGET){
			return true;
		}

	} // End Active class

} // End namespace

