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
* Last updated: 3/27/2016
*/

using System;
using System.Collections.Generic;

namespace Tactics_CoreGameEngine
{

	/// <summary>
	/// A class object representing a Command
	/// Commands are actions that move the game forward
	/// </summary>
	public class Command
	{

		//----------------------------------------------------------------

		/// <summary>
		/// An enum that categorizes the types of Commands that can be made
		/// </summary>
		public enum TYPE{
			MOVE, QUIT, PASS, MELEE, RECRUIT, DRAW, CLEARCORPSE,
			ACTIVE, TRAP, ONGOING, NULL
		}

		//----------------------------------------------------------------

		/// <summary>
		/// A Trigger Packet that can be executed.
		/// Holds a status on if the command was successful.
		/// </summary>
		private class ExecutableTriggerPacket{

			//----------------------------------------------

			/// <summary>
			/// The Trigger Packet this object is executing
			/// </summary>
			public TriggerPacket TP;

			/// <summary>
			/// Whether this object has been successful
			/// at executing the Trigger Packet
			/// </summary>
			public bool Successful;

			//----------------------------------------------

			/// <summary>
			/// Initializes a new instance of the 
			/// <see cref="PixelTactics.Command+ExecutableTriggerPacket"/>
			///  class.
			/// </summary>
			/// <param name="TP">The Trigger Packet</param>
			/// <param name="b">Base successful value</param>
			public ExecutableTriggerPacket(TriggerPacket TP, bool b){
				this.TP = TP; this.Successful = b;
			}
		}

		//----------------------------------------------------------------
		//Class variables

		/// <summary>
		/// The Command type for this object instance
		/// </summary>
		public TYPE Type;

		/// <summary>
		/// The command parameters for this command
		/// </summary>
		public string[] Parameters;

		/// <summary>
		/// Which player is executing this command
		/// </summary>
		public Player PLAYER;

		//----------------------------------------------------------------

		/// <summary>
		/// Initializes a new instance of the <see cref="PixelTactics.Command"/> class.
		/// Basic constructor that initializes variables
		/// </summary>
		/// <param name="type">The Command type</param>
		/// <param name="param">The Command parameters</param>
		/// <param name="PLAYER">The Player executing the Command</param>
		public Command (TYPE type, string[] param, Player PLAYER)
		{
			this.Type = type;
			this.Parameters = param;
			this.PLAYER = PLAYER;
		}

		//----------------------------------------------------------------

		/// <summary>
		/// Execute the specified Command
		/// </summary>
		/// <param name="c">The Command to execute</param>
		public static bool Execute(Command c){
			if (c == null)
				return false;

			//Create an ETP object by executing the Command
			ExecutableTriggerPacket etp = _Execute (c);

			//If the Command was successful, add its Trigger Packet
			//	to the Table's pipeline
			if(etp.Successful)
				c.PLAYER.TABLE.PIPELINE.Add(etp.TP);

			//Return the success value
			return etp.Successful;
		}

		/// <summary>
		/// Create a Command object from a command line string
		/// </summary>
		/// <param name="s">The string to parse</param>
		/// <param name="PLAYER">The Player that will execute the command</param>
		public static Command Parse(String s, Player PLAYER){
			//Enforce case insensitivities
			s = s.ToLower ();
			//Avoid parsing errors
			try{
				//Split by spaces
				string[] chunks = s.Split(' ');
				//Create the parameters array (without the command name argument)
				string[] param = new List<string>(chunks).GetRange(
					1, chunks.Length-1).ToArray();
				
				//Parse logic
				switch(chunks[0]){
				case "move": return new Command(TYPE.MOVE, param, PLAYER);
				case "quit": return new Command(TYPE.QUIT, param, PLAYER);
				case "pass": return new Command(TYPE.PASS, param, PLAYER);
				case "melee": return new Command(TYPE.MELEE, param, PLAYER);
				case "recruit": return new Command(TYPE.RECRUIT, param, PLAYER);
				case "draw": return new Command(TYPE.DRAW, param, PLAYER);
				case "clear": return new Command(TYPE.CLEARCORPSE, param, PLAYER);
				case "active": return new Command(TYPE.ACTIVE, param, PLAYER);
				case "trap": return new Command(TYPE.TRAP, param, PLAYER);
				case "ongoing": return new Command(TYPE.ONGOING, param, PLAYER);
				case "hand":
					int i = int.Parse(param[0]);
					if(PLAYER.HAND.Get(i).HandAbility.GetType()
						.IsSubclassOf(typeof(Trigger)))
						return new Command(TYPE.TRAP, param, PLAYER);
					if(PLAYER.HAND.Get(i).HandAbility.GetType()
						.IsSubclassOf(typeof(Active)))
						return new Command(TYPE.ACTIVE, param, PLAYER);
					if(PLAYER.HAND.Get(i).HandAbility.GetType()
						.IsSubclassOf(typeof(OnGoing)))
						return new Command(TYPE.ONGOING, param, PLAYER);
					break;
				}

			}catch{}
			//Return null on a parsing error
			return null;
		}

		public override string ToString(){
			string ret = "";
			ret += Type.ToString ();
			return ret;
		}

		//----------------------------------------------------------------

		/// <summary>
		/// Convert a Command Type to a Trigger Type
		/// </summary>
		/// <returns>The Trigger Type related to the Command Type</returns>
		/// <param name="type">The Command Type</param>
		private static Trigger.TYPE CtoT(Command.TYPE type){
			if (type == Command.TYPE.DRAW) return Trigger.TYPE.DRAW;
			if (type == Command.TYPE.RECRUIT) return Trigger.TYPE.RECRUIT;
			if (type == Command.TYPE.PASS) return Trigger.TYPE.ENDTURN;
			if (type == Command.TYPE.ACTIVE) return Trigger.TYPE.ACTIVE;
			return Trigger.TYPE.NULL;
		}

		/// <summary>
		/// The private method for Executing a Command
		/// Will return if the Command was successfully executed
		/// </summary>
		/// <param name="c">The command to execute</param>
		private static ExecutableTriggerPacket _Execute(Command c){
			//Make a dummy ETP with default success value 'false'
			ExecutableTriggerPacket ETP = new ExecutableTriggerPacket (null, false);
			//If the Command is null return the dummy value
			if (c == null)
				return ETP;
			
			//Add the Trigger Packet to the ETP
			ETP.TP = new TriggerPacket (CtoT (c.Type), c.PLAYER, null, c.PLAYER);

			//Avoid parsing errors
			try{
			switch (c.Type)
			{
				//Check if the move is valid first
				//Then make the move if it is a valid move
				case TYPE.MOVE:
					ETP.Successful= c.PLAYER.GAMEBOARD.ValidMove (
						int.Parse (c.Parameters [0]),
						int.Parse (c.Parameters [1]),
						int.Parse (c.Parameters [2]),
						int.Parse (c.Parameters [3]), false, true);
					if(ETP.Successful){
						//c.PLAYER.TABLE.SettleState(c.PLAYER);
						ETP.Successful= c.PLAYER.GAMEBOARD.Move (
							int.Parse (c.Parameters [0]),
							int.Parse (c.Parameters [1]),
							int.Parse (c.Parameters [2]),
							int.Parse (c.Parameters [3]));
					}
					break;
				//Exit the program
				case TYPE.QUIT:
					Environment.Exit (0);
					break;
				//Pass your turn (do nothing)
				case TYPE.PASS:
					ETP.Successful= true;
					break;
				//Perform a melee attack
				case TYPE.MELEE:
					ETP.Successful= c.PLAYER.GAMEBOARD.Melee(
						int.Parse(c.Parameters[0]),
						int.Parse(c.Parameters[1]));
					break;
				//Recruit a card from Hand into play
				case TYPE.RECRUIT:
					ETP.TP.TARGET = c.PLAYER.HAND.Get(
						int.Parse(c.Parameters[2]));
					ETP.Successful = c.PLAYER.GAMEBOARD.Recruit(
						int.Parse(c.Parameters[0]),
						int.Parse(c.Parameters[1]),
						int.Parse(c.Parameters[2]),
						c.PLAYER);
					break;
				//Draw a card
				case TYPE.DRAW:
					ETP.Successful = c.PLAYER.HAND.Draw(c.PLAYER);
					break;
				//Clear a corpse from the playing field
				case TYPE.CLEARCORPSE:
					ETP.TP.TARGET = c.PLAYER.GAMEBOARD.Board[
							int.Parse(c.Parameters[0]),
							int.Parse(c.Parameters[1])];
					ETP.Successful = c.PLAYER.GAMEBOARD.ClearCorpse(
						c.PLAYER.GRAVEYARD,
						int.Parse(c.Parameters[0]),
						int.Parse(c.Parameters[1]));
					break;
				//Perform the active on a card in hand
				case TYPE.ACTIVE:
					ETP.Successful= c.PLAYER.Active(
						int.Parse(c.Parameters[0]),
						int.Parse(c.Parameters[1]));
					break;
				//Lay a trap from the card in hand
				case TYPE.TRAP:
					ETP.TP.TARGET = c.PLAYER.HAND.Get(
						int.Parse(c.Parameters[0]));
					int index = -1;
					if(c.Parameters.Length > 1)
						index = int.Parse(c.Parameters[1]);
					ETP.Successful = c.PLAYER.Trap(
						int.Parse(c.Parameters[0]), index);
					break;
				//Play an ongoing effect from a card in hand
				case TYPE.ONGOING:
					ETP.TP.TARGET = c.PLAYER.HAND.Get(
						int.Parse(c.Parameters[0]));
					index = -1;
					if(c.Parameters.Length > 1)
						index = int.Parse(c.Parameters[1]);
					ETP.Successful = c.PLAYER.OnGoing(
						int.Parse(c.Parameters[0]), index);
					break;
				case TYPE.NULL:
					ETP.Successful = false;
					break;
			} // End switch

			}catch{
				//Default Success to false if there was a parse error
				ETP.Successful = false;
			}

			//Return the ETP
			return ETP;
		}
	
	} // End Command class

} // End Namespace
	