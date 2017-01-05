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
* Program.cs
* Author: Nic Wilson
* Last updated: 3/27/2016
*/

using System;

namespace Tactics_CoreGameEngine
{
	class MainClass
	{
		public static void Main (string[] args)
		{

			Player P1 = new Player ("P1", TableTop.ROWS, TableTop.COLUMNS, TableTop.MAXLIFE, null, new CommandInterface());
			Player P2 = new Player ("P2", TableTop.ROWS, TableTop.COLUMNS, TableTop.MAXLIFE, P1, new CommandInterface());
			P1.SetEnemy (P2);

			Deck D1 = new Deck (Utility.GetDebugStarterDeck(P1));
			Deck D2 = new Deck (Utility.GetDebugStarterDeck(P2));
			P1.LoadDeck (D1);
			P2.LoadDeck (D2);

			new TableTop ("EN", P1, P2, new AnimationInterface());
		}

	} // End Program class

} // End namespace
