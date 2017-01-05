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
* CommandInterface.cs
* Author: Nic Wilson
* Last updated: 3/27/2016
*/

using System;

namespace Tactics_CoreGameEngine
{
	/// <summary>
	/// A class representing the interface between a game and Player
	/// Allows the Player to control their turn
	/// </summary>
	public class CommandInterface
	{

		//----------------------------------------------------------------

		/// <summary>
		/// The Player this interface is representing
		/// </summary>
		public Player P;

		public Target TARGET;

		/// <summary>
		/// Initializes a new instance of the 
		/// <see cref="Tactics_CoreGameEngine.CommandInterface"/> class.
		/// </summary>
		public CommandInterface (){}

		/// <summary>
		/// Gets the Command for the Player's turn
		/// By default this let's the Player type via the terminal
		/// Flushes back to the Tabletop
		/// </summary>
		public virtual void GetTurnCommand(){
			Console.Write (P.Name + " ");
			Console.Write ("*CMD: ");
			String command = Console.ReadLine ();
			//Create a Command by parsing the string
			Command c = Command.Parse (command, P);
			//Trigger the table to move forward
			P.TABLE.Flush(P, c);
		}

		/// <summary>
		/// A function for getting a Target for an ability
		/// By default it let's the user type via the terminal
		/// </summary>
		/// <returns>The target.</returns>
		public virtual void GetTarget(Character c){
			Console.Write("*TARGET: ");
			String t = Console.ReadLine ();
			//Create a Target by parsing the string
			Target TARGET = Target.Parse (t, P);
			this.TARGET = TARGET;
		}
	} // End CommandInterface class

	//----------------------------------------------------------------------

	/// <summary>
	/// An EnemyAI interface that only passes turn
	/// </summary>
	public class EnemyAI_Pass : CommandInterface{
		public EnemyAI_Pass(){}

		public override void GetTurnCommand(){
			Command c = new Command (Command.TYPE.PASS, null, P);
			P.TABLE.Flush (P, c);
		}
	} // End EnemyAI_Pass class

} // End namespace

