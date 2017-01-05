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
* Target.cs
* Author: Nic Wilson
* Last updated: 3/27/2016
*/

using System;

namespace Tactics_CoreGameEngine
{
	public class Target
	{
		public Player PTARGET;
		public Character CTARGET;
		public bool TARGETPLAYER = true;
		public Target (Player PTARGET, Character CTARGET)
		{
			if (PTARGET == null)
				TARGETPLAYER = false;
			this.PTARGET = PTARGET;
			this.CTARGET = CTARGET;
		}

		public static Target Parse(String t, Player P){
			try{
				string[] chunks = t.Split(' ');
				if(chunks.Length == 1){
					if(chunks[0] == "0") return new Target(P, null);
					else if(chunks[0] == "1") return new Target(P.ENEMY, null);
				}else{
					int x = int.Parse(chunks[1]);
					int y = int.Parse(chunks[2]);
					GameBoard gb = (chunks[0] == "0") ? P.GAMEBOARD : P.ENEMY.GAMEBOARD;
					if(gb.Board[x,y] == null) return null;
					return new Target(null, gb.Board[x,y]);
				}
			}catch{}
			return null;
		}
	} // End Target class

} // End namespace

