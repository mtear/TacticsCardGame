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
 * OnGoingAbilities.cs
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
* 		THIS FILE IS A COLLECTION OF ONGOING EFFECTS FOR CHARACTER HAND ABILITIES
* 
**********************************************************************************
**********************************************************************************/

	/// <summary>
	/// Template class.
	/// </summary>
	public class TOT : OnGoing{
		public TOT() : base("TOT", "", 0){

		}

		public override void Discard (Player P){

		}
	}

	public class TO1 : OnGoing{ //Friendlies get Armor 1
		public TO1() : base("TOT", "TO000001", 3){
			this.Passives.Add (new TP8 ());
		}

		public override void Discard (Player P){

		}
	}

	public class TO2 : OnGoing{
		public TO2() : base("TOT", "TO000002", 3){
			this.Passives.Add (new TP17 ());
		}

		public override void Discard (Player P){

		}
	}

} //End namespace

