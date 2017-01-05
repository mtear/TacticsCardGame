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
* Trigger.cs
* Author: Nic Wilson
* Last updated: 3/27/2016
*/

using System;
using System.Collections.Generic;

namespace Tactics_CoreGameEngine
{
	public abstract class Trigger : Ability
	{
		public enum TYPE
		{
			DRAW, RECRUIT, NULL, ENDTURN, UNITDIED, MOVE, STARTTURN, UNITDAMAGE, UNITHEAL, PLAYERDAMAGE, PLAYERHEAL,
			TRAPACTIVATE, PLAYTRAP, PLAYONGOING, PLAYORDER, ACTIVE, CREATECORPSE, DISCARDCARD, HANDTOOFULL, CLEARCORPSE
		}

		public bool COUNTER = false;

		public Trigger (string name, string description) : base(name, description)
		{
		}

		public bool Check(TriggerPacket TP){
			bool b = Triggered (TP);
			return b;
		}

		public bool CheckAndExecute(TriggerPacket TP, List<TriggerPair> PIPELINE){
			bool b = Triggered (TP);
			if (b)
				PIPELINE.Add (new TriggerPair (
					new TriggerPacket(TP), this));
			return b;
		}

		public void ExportEffectToPipeline(TriggerPacket TP, List<TriggerPair> PIPELINE){
			PIPELINE.Add (new TriggerPair (TP, this));
		}

		public abstract bool Triggered(TriggerPacket TP);
		public void Execute(TriggerPacket TP){
			if (TP.TYPE == Trigger.TYPE.TRAPACTIVATE) {
				if (TP.TRIGGERTARGET.VALID) {
					mExecute (TP);
					return;
				} else
					return;
			}
			mExecute (TP);
		}
		protected abstract void mExecute(TriggerPacket TP);

	} // End Trigger class

	public class TriggerPacket{
		public Trigger.TYPE TYPE;
		public Player PLAYER, INITIATOR;
		public Character USER, TARGET;
		public Trigger TRIGGERTARGET = null;
		public Active ACTIVETARGET = null;
		public TriggerPacket TPTARGET = null;
		public Character TRUEUSER;
		public TriggerPacket(Trigger.TYPE TYPE, Player PLAYER,
			Character USER, Player INITIATOR){
			this.TYPE = TYPE;
			this.PLAYER = PLAYER;
			this.USER = USER;
			this.TRUEUSER = USER;
			this.TARGET = USER;
			this.INITIATOR = INITIATOR;
		}
		public TriggerPacket(Trigger.TYPE TYPE, Player PLAYER,
			Character USER, Player INITIATOR, Character TARGET){
			this.TYPE = TYPE;
			this.PLAYER = PLAYER;
			this.USER = USER;
			this.TRUEUSER = USER;
			this.TARGET = TARGET;
			this.INITIATOR = INITIATOR;
		}
		public TriggerPacket(TriggerPacket TP){
			this.TYPE = TP.TYPE;
			this.PLAYER = TP.PLAYER;
			this.INITIATOR = TP.INITIATOR;
			this.USER = TP.USER;
			this.TRUEUSER = USER;
			this.TARGET = TP.TARGET;
			this.TRIGGERTARGET = TP.TRIGGERTARGET;
			this.ACTIVETARGET = TP.ACTIVETARGET;
			this.TPTARGET = TP.TPTARGET;
		}
	} // End TriggerPacket class

	public class TriggerPair{
		public TriggerPacket TP;
		public Trigger TRIGGER;
		public TriggerPair (TriggerPacket TP, Trigger TRIGGER)
		{
			this.TP = TP;
			this.TRIGGER = TRIGGER;
		}
	} // End TriggerPair
		
} // End namespace

