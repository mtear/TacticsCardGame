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
 * TriggerAbilities.cs
 * Author: Nic Wilson
 * Last updated: 3/26/2016
 */

using System;
using System.Collections.Generic;

namespace Tactics_CoreGameEngine
{

/**********************************************************************************
**********************************************************************************
* 
* 					NOTE
* 
* 		THIS FILE IS A COLLECTION OF TRIGGER ABILITIES FOR CHARACTERS AND TRAPS
* 
**********************************************************************************
**********************************************************************************/


	/// <summary>
	/// Template class.
	/// </summary>
	class TTA : Trigger{
		public TTA() : base("TTA1", ""){}

		public override bool Triggered(TriggerPacket TP){
			return false;
		}

		protected override void mExecute(TriggerPacket TP){

		}
	}

	class TA1 : Trigger{ //Recruit: Draw a card
		public TA1() : base("TTA1", "TA000001"){}

		public override bool Triggered(TriggerPacket TP){
			if (TP.TYPE == TYPE.RECRUIT) {
				if (TP.USER == TP.TARGET) {
					return true;
				}
			}
			return false;
		}

		protected override void mExecute(TriggerPacket TP){
			TP.TARGET.CONTROLLER.DrawCard ();
			TP.TARGET.CONTROLLER.AddDamage (new Damage (Damage.TYPE.SPELL, 1, TP.USER));
		}
	}

	class TA2 : Trigger{ //Trap: Do 3 Damage to a Recruited Front Enemy Unit
		public TA2() : base("TTA1", "TA000002"){}

		public override bool Triggered(TriggerPacket TP){
			if (TP.TYPE == TYPE.RECRUIT) {
				if (TP.USER.CONTROLLER != TP.TARGET.CONTROLLER) {
					if(TP.TARGET.CONTROLLER.GAMEBOARD.LocateInBoard(TP.TARGET).y == 0)
						return true;
				}
			}
			return false;
		}

		protected override void mExecute(TriggerPacket TP){
			TP.TARGET.AddDamage (new Damage (Damage.TYPE.SPELL, 3, TP.USER));
		}
	}

	class TA3 : Trigger{ //Attack+1 when an Enemy is Recruited
		public TA3() : base("TTA1", "TA000003"){}

		public override bool Triggered(TriggerPacket TP){
			if (TP.TYPE == TYPE.RECRUIT) {
				if (TP.INITIATOR != TP.USER.CONTROLLER) {
					return true;
				}
			}
			return false;
		}

		protected override void mExecute(TriggerPacket TP){
			TP.USER.BaseAttack++;
		}
	}

	class TA4 : Trigger{ //Draw a card at start of turn
		public TA4() : base("TTA1", "TA000004"){}

		public override bool Triggered(TriggerPacket TP){
			if (TP.TYPE == TYPE.STARTTURN) {
				if (TP.INITIATOR == TP.USER.CONTROLLER) {
					return true;
				}
			}
			return false;
		}

		protected override void mExecute(TriggerPacket TP){
			TP.USER.CONTROLLER.DrawCard ();
		}
	}

	class TA5 : Trigger{ //Heal 2 at start of turn
		public TA5() : base("TTA1", "TA000005"){}

		public override bool Triggered(TriggerPacket TP){
			if (TP.TYPE == TYPE.STARTTURN) {
				if (TP.INITIATOR == TP.USER.CONTROLLER) {
					return true;
				}
			}
			return false;
		}

		protected override void mExecute(TriggerPacket TP){
			TP.USER.HealDamage (
				new Damage (Damage.TYPE.HEAL, 2, TP.USER));
		}
	}

	class TA6 : Trigger{ // Random Enemy takes 2 damage when they Clear Corpse
		public TA6() : base("TTA1", "TA000006"){}

		public override bool Triggered(TriggerPacket TP){
			if (TP.TYPE == TYPE.CLEARCORPSE) {
				if (TP.USER.CONTROLLER != TP.TARGET.CONTROLLER) {
					return true;
				}
			}
			return false;
		}

		protected override void mExecute(TriggerPacket TP){
			List<Character> units = TP.TRUEUSER.CONTROLLER.ENEMY.GAMEBOARD.Units;
			if (units.Count == 0)
				return;
			int targetindex = 0;
			if (units.Count > 1)
				targetindex = Utility.R.Next (units.Count);
			Character target = units [targetindex];
			target.AddDamage (
				new Damage (Damage.TYPE.SPELL, 2, TP.TRUEUSER));
		}
	}

	class TA7 : Trigger{ //Trap: Discard a card, counter a trap
		public TA7() : base("TTA1", "TA000007"){this.COUNTER = true;}

		public override bool Triggered(TriggerPacket TP){
			if (TP.TYPE == TYPE.TRAPACTIVATE) {
				if (TP.TRUEUSER.CONTROLLER != TP.USER.CONTROLLER) {
					return true;
				}
			}
			return false;
		}

		protected override void mExecute(TriggerPacket TP){
			TP.TRUEUSER.CONTROLLER.HAND.DiscardRandomCard (TP.TRUEUSER.CONTROLLER.GRAVEYARD);
			TP.TPTARGET.TRIGGERTARGET.VALID = false;
		}
	}

	class TA8 : Trigger{ //Heal friendlies for 1 when an enemy trap is played
		public TA8() : base("TTA1", "TA000008"){}

		public override bool Triggered(TriggerPacket TP){
			if (TP.TYPE == TYPE.PLAYTRAP) {
				if(TP.USER.CONTROLLER != TP.PLAYER)
					return true;
			}
			return false;
		}

		protected override void mExecute(TriggerPacket TP){
			for (int i = 0; i < TableTop.ROWS; i++) {
				for (int a = 0; a < TableTop.COLUMNS; a++) {
					Character c = TP.USER.CONTROLLER.GAMEBOARD.Board [a, i];
					if (c == null)
						continue;
					c.HealDamage (new Damage (Damage.TYPE.HEAL, 1, TP.USER));
				}
			}
		}
	}

	class TA9 : Trigger{
		public TA9() : base("TTA1", "TA000009"){}

		public override bool Triggered(TriggerPacket TP){
			if (TP.TYPE == TYPE.DRAW) {
				if (TP.USER.CONTROLLER == TP.PLAYER) {
					return true;
				}
			}
			return false;
		}

		protected override void mExecute(TriggerPacket TP){
			TP.USER.CONTROLLER.HealDamage (new Damage (Damage.TYPE.HEAL, 1, TP.USER));
		}
	}

} //End namespace

