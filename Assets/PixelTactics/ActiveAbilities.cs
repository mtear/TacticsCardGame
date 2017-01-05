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
 * ActiveAbilities.cs
 * Author: Nic Wilson
 * Last updated: 3/26/2016
 */

using System;
using UnityEngine;

namespace Tactics_CoreGameEngine
{

/**********************************************************************************
**********************************************************************************
* 
* 					NOTE
* 
* 		THIS FILE IS A COLLECTION OF ACTIVE ABILITIES FOR CHARACTER HAND EFFECTS
* 
**********************************************************************************
**********************************************************************************/

	/// <summary>
	/// Template class.
	/// </summary>
	public class TPA : Active{
		public TPA () : base ("TA1", ""){}

		public override void Execute(Character target, Player PLAYER){

		}
	}

	public class AA1 : Active{ //Draw a card
		public AA1 () : base ("TA1", "AA000001"){}

		public override void Execute(Character target, Player PLAYER){
			PLAYER.DrawCard ();
		}
	}

	public class AA2 : Active{ //Enemy discards a card. Draw a card
		public AA2 () : base ("TA1", "AA000002"){}

		public override void Execute(Character target, Player PLAYER){
			PLAYER.ENEMY.HAND.DiscardRandomCard (PLAYER.ENEMY.GRAVEYARD);
			PLAYER.DrawCard ();
		}
	}

	public class AA3 : Active{ //Do 2 damage to unit
		public AA3 () : base ("TA1", "AA000003"){
			this.RequestsTarget = true;
		}

		public override void Execute(Character target, Player PLAYER){
			TARGET.CTARGET.AddDamage (new Damage (Damage.TYPE.SPELL, 2, null));
		}

		protected override bool ValidTarget(Target TARGET){
			if (TARGET.TARGETPLAYER)
				return false;
			return true;
		}
	}

	public class AA4 : Active{
		public AA4 () : base ("TA1", "AA000004"){}

		public override void Execute(Character target, Player PLAYER){
			foreach (Character c in PLAYER.GAMEBOARD.Units) {
				if (!c.Dead) {
					c.AddDamage (new Damage (Damage.TYPE.SPELL, 2, null));
				}
			}
			foreach (Character c in PLAYER.ENEMY.GAMEBOARD.Units) {
				if (!c.Dead) {
					c.AddDamage (new Damage (Damage.TYPE.SPELL, 2, null));
				}
			}
		}
	}

	public class AA5 : Active{ //Do 2 damage to Player
		public AA5 () : base ("TA1", "AA000005"){
			this.RequestsTarget = true;
		}

		public override void Execute(Character target, Player PLAYER){
			TARGET.PTARGET.HealDamage (new Damage (Damage.TYPE.HEAL, 3, target));
			TARGET.PTARGET.HAND.DiscardRandomCard (TARGET.PTARGET.GRAVEYARD);
		}

		protected override bool ValidTarget(Target TARGET){
			if (TARGET.TARGETPLAYER)
				return true;
			return false;
		}
	}

	public class AA6 : Active{ //Destroy a random trap from each player
		public AA6 () : base ("TA1", "AA000006"){}

		public override void Execute(Character target, Player PLAYER){
			if (PLAYER.Traps.Count != 0) {
				Character trap = PLAYER.Traps [Utility.R.Next (PLAYER.Traps.Count)];
				for (int i = 0; i < PLAYER.TRAPS.Length; i++) {
					if (PLAYER.TRAPS [i] == trap)
						PLAYER.TRAPS [i] = null;
				}
			}
			PLAYER = PLAYER.ENEMY;
			if (PLAYER.Traps.Count != 0) {
				Character trap = PLAYER.Traps [Utility.R.Next (PLAYER.Traps.Count)];
				for (int i = 0; i < PLAYER.TRAPS.Length; i++) {
					if (PLAYER.TRAPS [i] == trap)
						PLAYER.TRAPS [i] = null;
				}
			}
		}
	}

} //End namespace

